using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebcrawlerService
{
    public class Wetsvoorstellen: GenericObject
    {
        private List<string> HaalTitelOp()
        {
            List<string> titels = new CrawlContent().getItems("http://www.tweedekamer.nl/kamerstukken/wetsvoorstellen?qry=%2A&fld_tk_categorie=Kamerstukken&fld_tk_subcategorie=Wetsvoorstellen&Type=Wetsvoorstellen&srt=date%3Adesc%3Adate&fld_prl_status=Aanhangig&fld_tk_subsubcategorie=Wetsvoorstellen+regering&dpp=15&clusterName=Wetsvoorstellen+regering", "<div class=\"search-result-content\">\\s*<h3><a href=\"(.+?)</a>");
            for (int i = 0; i < titels.Count; i++)
            {
                titels[i] = titels[i].Split('>')[1];
            }
            return titels;
        }

        public List<DateTime> HaalDatumOp()
        {
            List<DateTime> dt = new List<DateTime>();
            List<string> tijden = new CrawlContent().getItems("http://www.tweedekamer.nl/kamerstukken/wetsvoorstellen?qry=%2A&fld_tk_categorie=Kamerstukken&fld_tk_subcategorie=Wetsvoorstellen&Type=Wetsvoorstellen&srt=date%3Adesc%3Adate&fld_prl_status=Aanhangig&fld_tk_subsubcategorie=Wetsvoorstellen+regering&dpp=15&clusterName=Wetsvoorstellen+regering", "<p class=\"date\">(.+?)</p>");

            foreach (var item in tijden)
            {
                dt.Add(Convert.ToDateTime(item));
            }
            return dt;
        }

        private List<string> HaalLinkOp()
        {
            List<string> links = new CrawlContent().getItems("http://www.tweedekamer.nl/kamerstukken/wetsvoorstellen?qry=%2A&fld_tk_categorie=Kamerstukken&fld_tk_subcategorie=Wetsvoorstellen&Type=Wetsvoorstellen&srt=date%3Adesc%3Adate&fld_prl_status=Aanhangig&fld_tk_subsubcategorie=Wetsvoorstellen+regering&dpp=15&clusterName=Wetsvoorstellen+regering", "<div class=\"search-result-content\">\\s*<h3><a href=\"(.+?)\"");

            return links;
        }

        public void GetAllSources()
        {
            List<IGenericObject> ListObjecten = new List<IGenericObject>();

            List<string> ListHeadlines = HaalTitelOp();
            List<string> ListHeadlinesLink = HaalLinkOp();
            List<DateTime> ListTijden = HaalDatumOp();
            //List<string> ListTime = GetTime();

            for (int i = 0; i < ListHeadlines.Count; i++)
            {
                GenericObject newObject = new Wetsvoorstellen();
                newObject.GetTitel = ListHeadlines[i];
                newObject.GetBron = "2e kamer";
                newObject.GetLink = ListHeadlinesLink[i];
                newObject.GetBeschrijving = "";
                newObject.GetTijd = Convert.ToDateTime("01-01-2015");
                newObject.GetCategorie = Categorie.Wetsvoorstellen;
                ListObjecten.Add(newObject);
            }

            foreach (var item in ListObjecten)
            {
                DAL.Insert(item);
            }
        }
    }
}