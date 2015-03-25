using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using WebcrawlerService;

namespace WebCrawlerForms
{
    public class ZetelsController: IGenericObject
    {
        private string titel;
        public string GetTitel { get { return titel; } set { titel = value; } }
        private string beschrijving;
        public string GetBeschrijving { get { return beschrijving; } set { beschrijving = value; } }
        private string bron;
        public string GetBron { get { return bron; } set { bron = value; } }
        private string media;
        public string GetMedia { get { return media; } set { media = value; } }
        private string link;
        public string GetLink { get { return link; } set { link = value; } }
        private string dag;
        public string GetDag { get { return dag; } set { dag = value; } }
        private string tijd;
        public string GetTijd { get { return tijd; } set { tijd = value; } }

        public Graphics paper;

        public ZetelsController(Graphics paper)
        {
            this.paper = paper;
        }

        public ZetelsController()
        {

        }

        public void TekenLabels(List<IGenericObject> partijen)
        {
            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
            drawFormat.FormatFlags = StringFormatFlags.DirectionVertical;
            for (int i = 0; i < partijen.Count; i++)
            {
                paper.DrawString(partijen[i].GetTitel, new Font(FontFamily.GenericSansSerif, 10, FontStyle.Regular), new SolidBrush(Color.White), (i*40)+ 40, 200, drawFormat);
            }
        }

        public void TekenZetels(List<IGenericObject> zetels)
        {
            for (int i = 0; i < zetels.Count; i++)
            {
                int cijfer = Convert.ToInt32(zetels[i].GetBeschrijving);
                paper.DrawLine(new Pen(new SolidBrush(Color.White),2), (i * 40) + 45, 195, (i * 40) + 45, 195 - (cijfer*5));
                paper.DrawString(zetels[i].GetBeschrijving, new Font(FontFamily.GenericSansSerif, 10, FontStyle.Regular), new SolidBrush(Color.White), (i * 40) + 40, 10);
            }
        }

        public void TekenAs()
        {
            paper.DrawLine(new Pen(new SolidBrush(Color.SeaGreen),3), 30, 10, 30, 195);
            paper.DrawLine(new Pen(new SolidBrush(Color.SeaGreen),3), 30, 195, 500, 195);
            Pen pen = new Pen(new SolidBrush(Color.White));
            float[] dashValues = { 5, 2, 15, 4 };
            pen.DashPattern = dashValues;
            paper.DrawLine(pen, 20, 195 - (30 * 5), 500, 195 - (30 * 5));
            paper.DrawLine(pen, 20, 195 - (20 * 5), 500, 195 - (20 * 5));
            paper.DrawLine(pen, 20, 195 - (10 * 5), 500, 195 - (10 * 5));
            paper.DrawString("30", new Font(FontFamily.GenericSansSerif, 10, FontStyle.Regular), new SolidBrush(Color.White), 3, 187 - (30 * 5));
            paper.DrawString("20", new Font(FontFamily.GenericSansSerif, 10, FontStyle.Regular), new SolidBrush(Color.White), 3, 187 - (20 * 5));
            paper.DrawString("10", new Font(FontFamily.GenericSansSerif, 10, FontStyle.Regular), new SolidBrush(Color.White), 3, 187 - (10 * 5));
        }

        public List<IGenericObject> GetZetelsItems()
        {
            List<IGenericObject> ZetelsItems = new List<IGenericObject>();
            DataTable dt = DAL.Select("SELECT Titel, Beschrijving, Datum FROM Objecten WHERE Categorie = '2' ORDER BY Datum DESC, Tijd DESC");

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IGenericObject newObject = new ZetelsController();
                    newObject.GetTitel = dt.Rows[i][0].ToString();
                    newObject.GetBeschrijving = dt.Rows[i][1].ToString();
                    newObject.GetDag = dt.Rows[i][2].ToString();
                    ZetelsItems.Add(newObject);
                }
            }

            return ZetelsItems;
        }
    }
}
