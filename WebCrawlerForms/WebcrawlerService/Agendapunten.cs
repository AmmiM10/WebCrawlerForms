using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WebcrawlerService
{
    public class Agendapunten : GenericObject
    {
        private List<string> HaalTitelOp()
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

        private List<string> HaalLinkOp()
        {
            List<string> links = new CrawlContent().getItems("http://www.tweedekamer.nl/vergaderingen/commissievergaderingen", "<a class=\"goto-agenda\" href=\"(.+?)\"");

            return links;
        }

        private List<string> HaalTijdOp()
        {
            List<string> tijden = new CrawlContent().getItems("http://www.tweedekamer.nl/vergaderingen/commissievergaderingen", "<p class=\"datetime\">(.+?)</p>");

            return tijden;
        }

        private List<string> HaalTypeOp()
        {
            //<h3><a href="/kamerleden/commissies/
            List<string> Type = new CrawlContent().getItems("http://www.tweedekamer.nl/vergaderingen/commissievergaderingen", "<p class=\"type\">(.+?)</p>");

            return Type;
        }

        public void GetAllSources()
        {
            List<IGenericObject> ListObjecten = new List<IGenericObject>();

            List<string> ListHeadlines = HaalTitelOp();
            List<string> ListLink = HaalLinkOp();
            List<string> ListType = HaalTypeOp();
            List<string> ListTijd = HaalTijdOp();

            for (int i = 0; i < ListHeadlines.Count; i++)
            {
                GenericObject newObject = new Agendapunten();
                newObject.GetTitel = ListHeadlines[i];

                newObject.GetBron = "2e kamer";
                newObject.GetMedia = ListType[i];
                newObject.GetDag = ListTijd[i];
                newObject.GetLink = ListLink[i];
                newObject.GetBeschrijving = ListTijd[i];

                newObject.GetCategorie = Categorie.Agendapunten;
                ListObjecten.Add(newObject);
            }

            foreach (var item in ListObjecten)
            {
                DAL.Insert(item);
            }
        }
    }
}