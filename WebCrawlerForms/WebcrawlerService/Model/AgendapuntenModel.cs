using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WebcrawlerService
{
    public class AgendapuntenModel : GenericObject
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
            List<string> Type = new CrawlContent().getItems("http://www.tweedekamer.nl/vergaderingen/commissievergaderingen", "<p class=\"type\">(.+?)</p>");
            return Type;
        }

        private DateTime HaalDatumOp()
        {
            DateTime dt = new DateTime();
            string type = new CrawlContent().GetTekst("http://www.tweedekamer.nl/vergaderingen/commissievergaderingen", "<h2>VANDAAG&nbsp;(.+?)</h2>");
            dt = Convert.ToDateTime(type);
            
            return dt;
        }

        public void GetAllSources()
        {
            List<IGenericObject> ListObjecten = new List<IGenericObject>();

            List<string> ListHeadlines = HaalTitelOp();
            List<string> ListLink = HaalLinkOp();
            List<string> ListType = HaalTypeOp();
            List<string> ListTijd = HaalTijdOp();
            DateTime dt = HaalDatumOp();

            for (int i = 0; i < ListHeadlines.Count; i++)
            {
                GenericObject newObject = new AgendapuntenModel();
                newObject.GetTitel = ListHeadlines[i];

                newObject.GetBron = "2e kamer";
                newObject.GetMedia = ListType[i];
                newObject.GetLink = ListLink[i];
                newObject.GetTijd = dt;
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