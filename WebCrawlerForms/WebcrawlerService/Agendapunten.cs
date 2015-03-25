using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WebcrawlerService
{
    public class Agendapunten : IGenericObject
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
            List<string> titels = new CrawlContent().getItems("http://www.tweedekamer.nl/vergaderingen/commissievergaderingen", "<p class=\"subject\">(.+?)\\s*</p>");
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
            List<string> links = new CrawlContent().getItems("http://www.tweedekamer.nl/vergaderingen/commissievergaderingen", "<a class=\"goto-agenda\" href=\"(.+?)\"");

            return links;
        }

        public List<string> HaalTijdOp()
        {
            List<string> tijden = new CrawlContent().getItems("http://www.tweedekamer.nl/vergaderingen/commissievergaderingen", "<p class=\"datetime\">(.+?)</p>");

            return tijden;
        }

        public List<string> HaalTypeOp()
        {
            //<h3><a href="/kamerleden/commissies/
            List<string> Type = new CrawlContent().getItems("http://www.tweedekamer.nl/vergaderingen/commissievergaderingen", "<p class=\"type\">(.+?)</p>");

            return Type;
        }

        public List<IGenericObject> GetAllSources()
        {
            List<IGenericObject> ListObjecten = new List<IGenericObject>();

            List<string> ListHeadlines = HaalTitelOp();
            List<string> ListLink = HaalLinkOp();
            List<string> ListType = HaalTypeOp();
            List<string> ListTijd = HaalTijdOp();

            for (int i = 0; i < ListHeadlines.Count; i++)
            {
                IGenericObject newObject = new Agendapunten();
                newObject.GetTitel = ListHeadlines[i];

                newObject.GetBron = "2e kamer";
                newObject.GetMedia = ListType[i];
                newObject.GetDag = ListTijd[i];
                newObject.GetLink = ListLink[i];
                newObject.GetBeschrijving = "";

                newObject.GetCategorie = Categorie.Agendapunten;
                ListObjecten.Add(newObject);
            }


            return ListObjecten;
        }
    }
}