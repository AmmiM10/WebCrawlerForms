using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace WebCrawlerForms
{
    public class Wetsvoorstellen
    {
        public Wetsvoorstellen()
        {
        }

        public List<string> HaalTitelOp()
        {
            List<string> titels = new Tekst().getItems("http://www.tweedekamer.nl/kamerstukken/wetsvoorstellen?qry=%2A&fld_tk_categorie=Kamerstukken&fld_tk_subcategorie=Wetsvoorstellen&Type=Wetsvoorstellen&srt=date%3Adesc%3Adate&fld_prl_status=Aanhangig&fld_tk_subsubcategorie=Wetsvoorstellen+regering&dpp=15&clusterName=Wetsvoorstellen+regering", "<div class=\"search-result-content\">\\s*<h3><a href=\"(.+?)</a>");
            for (int i = 0; i < titels.Count; i++)
            {
                titels[i] = titels[i].Split('>')[1];
            }
            return titels;
        }

        public List<string> HaalLinkOp()
        {
            List<string> links = new Tekst().getItems("http://www.tweedekamer.nl/kamerstukken/wetsvoorstellen?qry=%2A&fld_tk_categorie=Kamerstukken&fld_tk_subcategorie=Wetsvoorstellen&Type=Wetsvoorstellen&srt=date%3Adesc%3Adate&fld_prl_status=Aanhangig&fld_tk_subsubcategorie=Wetsvoorstellen+regering&dpp=15&clusterName=Wetsvoorstellen+regering", "<div class=\"search-result-content\">\\s*<h3><a href=\"(.+?)\"");

            return links;
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
