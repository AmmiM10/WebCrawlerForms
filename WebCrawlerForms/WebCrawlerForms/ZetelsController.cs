using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using WebcrawlerService;

namespace WebCrawlerForms
{
    public class ZetelsController
    {
        public Graphics paper;

        public ZetelsController(Graphics paper)
        {
            this.paper = paper;
        }

        public ZetelsController()
        {

        }

        public void TekenLabels(List<string> partijen)
        {
            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
            drawFormat.FormatFlags = StringFormatFlags.DirectionVertical;
            for (int i = 0; i < partijen.Count; i++)
            {
                paper.DrawString(partijen[i], new Font(FontFamily.GenericSansSerif, 10, FontStyle.Regular), new SolidBrush(Color.White), (i*40)+ 40, 200, drawFormat);
            }
        }

        public void TekenZetels(List<string> zetels, List<string> zetels2012)
        {
            for (int i = 0; i < zetels.Count; i++)
            {
                int cijfer = Convert.ToInt32(zetels[i]);
                int cijfer2012 = Convert.ToInt32(zetels2012[i]);
                paper.DrawLine(new Pen(new SolidBrush(Color.White),2), (i * 40) + 45, 195, (i * 40) + 45, 195 - (cijfer*5));
                paper.DrawString(zetels[i], new Font(FontFamily.GenericSansSerif, 10, FontStyle.Regular), new SolidBrush(Color.White), (i * 40) + 40, 10);

                int verschil = cijfer - cijfer2012;
                if (cijfer > cijfer2012)
                {
                    paper.DrawString("+"+Convert.ToString(verschil), new Font(FontFamily.GenericSansSerif, 10, FontStyle.Regular), new SolidBrush(Color.Green), (i * 40) + 52, 10);
                }
                else if (cijfer < cijfer2012)
                {
                    paper.DrawString(Convert.ToString(verschil), new Font(FontFamily.GenericSansSerif, 10, FontStyle.Regular), new SolidBrush(Color.Red), (i * 40) + 52, 10);
                }
                else
                {
                    paper.DrawString(Convert.ToString(verschil), new Font(FontFamily.GenericSansSerif, 10, FontStyle.Regular), new SolidBrush(Color.White), (i * 40) + 52, 10);    
                }
            }
        }

        public void TekenZetels2012(List<string> zetels)
        {
            for (int i = 0; i < zetels.Count; i++)
            {
                int cijfer = Convert.ToInt32(zetels[i]);
                paper.DrawLine(new Pen(new SolidBrush(Color.Red), 2), (i * 40) + 50, 195, (i * 40) + 50, 195 - (cijfer * 5));
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

        public List<string> HaalZetelsOp()
        {
            List<string> peilingen = new Tekst().getItems("http://frontbencher.nl/peilingen/", "<li class=\"dehond\"><div class=\"bar\" style=\"height:(.+?)</div>");
            for (int i = 0; i < peilingen.Count; i++)
            {
                peilingen[i] = peilingen[i].Split('>')[1];
            }
            List<string> titels = new Tekst().getItems("http://frontbencher.nl/peilingen/", "title=\"Peilingen (.+?)\" alt");
            return peilingen;
        }

        public List<string> HaalPartijenOp()
        {
            List<string> titels = new Tekst().getItems("http://frontbencher.nl/peilingen/", "title=\"Peilingen (.+?)\" alt");

            return titels;
        }

        public string HaalDatumOp()
        {
            string datum = new Tekst().GetTekst("http://frontbencher.nl/peilingen/", "Maurice de Hond<span> (.+?)</span>");

            return datum;
        }

        public List<WebcrawlerService.IGenericObject> GetNieuwsItems()
        { 
            List<WebcrawlerService.IGenericObject> NieuwsItems = new List<WebcrawlerService.IGenericObject>();
            DataTable dt = DAL.Select("SELECT * FROM Objecten WHERE Categorie = '2' ORDER BY Id");

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var test = new IGenericObject;
                    test.GetTitel = dt.Rows[i][1].ToString();
                    test.GetBeschrijving = dt.Rows[i][2].ToString();
                    test.GetBron = dt.Rows[i][3].ToString();
                    test.GetDag = dt.Rows[i][4].ToString();
                    NieuwsItems.Add(test);
                }
            }

            return NieuwsItems;
        }
    }
}
