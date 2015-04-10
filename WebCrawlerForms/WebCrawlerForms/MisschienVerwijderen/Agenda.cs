using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;

namespace WebCrawlerForms
{
    public class Agenda
    {
        public List<string> HaalTitelOp()
        {
            List<string> titels = new Tekst().getItems("http://www.tweedekamer.nl/vergaderingen/commissievergaderingen", "<p class=\"subject\">(.+?)\\s*</p>");
            for (int i = 0; i < titels.Count; i++)
            {
                if (titels[i].Contains("    "))
                {
                    string pattern = "     ";
                    string replacement = " ";
                    Regex rgx = new Regex(pattern);
                    string result = rgx.Replace(titels[i], replacement);
                    //titels[i] = titels[i].Replace('/n',' ');
                }
            }
            return titels;
        }

        public List<string> HaalLinkOp()
        {
            List<string> links = new Tekst().getItems("http://www.tweedekamer.nl/vergaderingen/commissievergaderingen", "<a class=\"goto-agenda\" href=\"(.+?)\"");

            return links;
        }

        public List<string> HaalTijdOp()
        {
            List<string> tijden = new Tekst().getItems("http://www.tweedekamer.nl/vergaderingen/commissievergaderingen", "<p class=\"datetime\">(.+?)</p>");

            return tijden;
        }

        public List<string> HaalTypeOp()
        {
            //<h3><a href="/kamerleden/commissies/
            List<string> Type = new Tekst().getItems("http://www.tweedekamer.nl/vergaderingen/commissievergaderingen", "<p class=\"type\">(.+?)</p>");

            return Type;
        }

        public List<string> DbTitels()
        {
            List<string> titels = new List<string>();
            DataTable dt = SQL.Select("SELECT Titel FROM Wetsvoorstellen ORDER BY Id");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                titels.Add(dt.Rows[i][0].ToString());
            }

            return titels;
        }


        public List<string> DbLinks()
        {
            List<string> links = new List<string>();
            DataTable dt = SQL.Select("SELECT Link FROM Wetsvoorstellen ORDER BY Id ");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                links.Add(dt.Rows[i][0].ToString());
            }

            return links;
        }
    }
}
