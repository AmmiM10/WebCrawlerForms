using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebcrawlerService
{
    public class Wetsvoorstellen: IGenericObject
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
        private Categorie categorie;
        public Categorie GetCategorie { get { return categorie; } set { categorie = value; } }

        public List<string> HaalTitelOp()
        {
            List<string> titels = new CrawlContent().getItems("http://www.tweedekamer.nl/kamerstukken/wetsvoorstellen?qry=%2A&fld_tk_categorie=Kamerstukken&fld_tk_subcategorie=Wetsvoorstellen&Type=Wetsvoorstellen&srt=date%3Adesc%3Adate&fld_prl_status=Aanhangig&fld_tk_subsubcategorie=Wetsvoorstellen+regering&dpp=15&clusterName=Wetsvoorstellen+regering", "<div class=\"search-result-content\">\\s*<h3><a href=\"(.+?)</a>");
            for (int i = 0; i < titels.Count; i++)
            {
                titels[i] = titels[i].Split('>')[1];
            }
            return titels;
        }

        public List<string> HaalLinkOp()
        {
            List<string> links = new CrawlContent().getItems("http://www.tweedekamer.nl/kamerstukken/wetsvoorstellen?qry=%2A&fld_tk_categorie=Kamerstukken&fld_tk_subcategorie=Wetsvoorstellen&Type=Wetsvoorstellen&srt=date%3Adesc%3Adate&fld_prl_status=Aanhangig&fld_tk_subsubcategorie=Wetsvoorstellen+regering&dpp=15&clusterName=Wetsvoorstellen+regering", "<div class=\"search-result-content\">\\s*<h3><a href=\"(.+?)\"");

            return links;
        }

        public List<IGenericObject> GetAllSources()
        {
            List<IGenericObject> ListObjecten = new List<IGenericObject>();

            List<string> ListHeadlines = HaalTitelOp();
            List<string> ListHeadlinesLink = HaalLinkOp();
            //List<string> ListTime = GetTime();    //Maikel moet dit ook?

            for (int i = 0; i < ListHeadlines.Count; i++)
            {
                IGenericObject newObject = new Wetsvoorstellen();
                newObject.GetTitel = ListHeadlines[i];
                newObject.GetBron = "2e kamer";
                newObject.GetLink = ListHeadlinesLink[i];
                newObject.GetBeschrijving = "";
                newObject.GetCategorie = Categorie.Wetsvoorstellen;
                ListObjecten.Add(newObject);
            }


            return ListObjecten;
        }
    }
}