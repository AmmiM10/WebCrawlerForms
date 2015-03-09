using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Web;
using System.Net;
using System.Text.RegularExpressions;
using System.Data;

namespace WebCrawlerForms
{
    public class Zetels
    {
        public Graphics paper;

        public Zetels(Graphics paper)
        {
            this.paper = paper;
        }

        public Zetels()
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

        public void TekenZetels(List<string> zetels)
        {
            for (int i = 0; i < zetels.Count; i++)
            {
                int cijfer = Convert.ToInt32(zetels[i]);
                paper.DrawLine(new Pen(new SolidBrush(Color.White),2), (i * 40) + 45, 195, (i * 40) + 45, 195 - (cijfer*5));
                paper.DrawString(zetels[i], new Font(FontFamily.GenericSansSerif, 10, FontStyle.Regular), new SolidBrush(Color.White), (i * 40) + 40, 10);
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

        public string DbDatum()
        {
            string datum = null;
            DataTable dt = SQL.Select("SELECT PeilingDatum FROM Zetels WHERE Id = '2'");
            datum = dt.Rows[0][0].ToString();
            return datum;
        }

        public List<string> DbPartijen()
        {
            List<string> partijen = new List<string>();
            DataTable dt = SQL.Select("SELECT Partij, Aantal FROM Zetels ORDER BY Aantal DESC");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                partijen.Add(dt.Rows[i][0].ToString());
            }
            return partijen;
        }

        public List<string> DbAantalZetels()
        {
            List<string> aantal = new List<string>();
            DataTable dt = SQL.Select("SELECT Aantal FROM Zetels WHERE Id != '2' ORDER BY Aantal DESC");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                aantal.Add(dt.Rows[i][0].ToString());
            }
            return aantal;
        }
    }
}
