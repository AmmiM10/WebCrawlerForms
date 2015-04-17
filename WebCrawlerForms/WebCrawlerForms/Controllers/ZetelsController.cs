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
        private Graphics paper;
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
        private DateTime tijd;
        public DateTime GetTijd { get { return tijd; } set { tijd = value; } }
        private Categorie categorie;
        public Categorie GetCategorie { get { return categorie; } set { categorie = value; } }

        public List<IGenericObject> GetZetelsItems()
        {
            return new Converter().ConvertGenericObjects(Categorie.Zetels);
        }

        public ZetelsController(Graphics paper)
        {
            this.paper = paper;
        }

        public void TekenLabels(List<IGenericObject> partijen)
        {
            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
            drawFormat.FormatFlags = StringFormatFlags.DirectionVertical;
            for (int i = 0; i < partijen.Count; i++)
            {
                paper.DrawString(partijen[i].GetLink, new Font(FontFamily.GenericSansSerif, 10, FontStyle.Regular), new SolidBrush(Color.White), (i * 40) + 40, 200, drawFormat);
            }
        }

        public void TekenZetels(List<IGenericObject> zetels)
        {
            for (int i = 0; i < zetels.Count; i++)
            {
                int cijfer = Convert.ToInt32(zetels[i].GetTitel);
                paper.DrawLine(new Pen(new SolidBrush(Color.White), 2), (i * 40) + 45, 195, (i * 40) + 45, 195 - (cijfer * 5));
                paper.DrawString(zetels[i].GetTitel, new Font(FontFamily.GenericSansSerif, 10, FontStyle.Regular), new SolidBrush(Color.White), (i * 40) + 40, 10);
            }
        }

        public void TekenAs()
        {
            paper.DrawLine(new Pen(new SolidBrush(Color.SeaGreen), 3), 30, 10, 30, 195);
            paper.DrawLine(new Pen(new SolidBrush(Color.SeaGreen), 3), 30, 195, 500, 195);
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
    }
}
