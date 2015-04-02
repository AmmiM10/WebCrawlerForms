using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebcrawlerService
{
    public class Zetels: GenericObject
    {
        private List<string> HaalZetelsOp()
        {
            List<string> peilingen = new CrawlContent().getItems("http://frontbencher.nl/peilingen/", "<li class=\"dehond\"><div class=\"bar\" style=\"height:(.+?)</div>");
            for (int i = 0; i < peilingen.Count; i++)
            {
                peilingen[i] = peilingen[i].Split('>')[1];
            }
            List<string> titels = new CrawlContent().getItems("http://frontbencher.nl/peilingen/", "title=\"Peilingen (.+?)\" alt");
            return peilingen;
        }

        private List<string> HaalPartijenOp()
        {
            List<string> titels = new CrawlContent().getItems("http://frontbencher.nl/peilingen/", "title=\"Peilingen (.+?)\" alt");

            return titels;
        }

        private string HaalDatumOp()
        {
            string datum = new CrawlContent().GetTekst("http://frontbencher.nl/peilingen/", "Maurice de Hond<span>(.+?)</span>");

            return datum;
        }

        public void GetAllSources()
        {
            List<IGenericObject> ListObjecten = new List<IGenericObject>();

            List<string> ListInhoud = HaalZetelsOp();
            List<string> ListHeadlines = HaalPartijenOp();
            string ListTime = HaalDatumOp().Replace(".", "");
            ListTime = "1 apr 2015";

            for (int i = 0; i < ListHeadlines.Count; i++)
            {
                GenericObject newObject = new Zetels();
                newObject.GetTitel = ListHeadlines[i];
                newObject.GetBeschrijving = ListInhoud[i];
                newObject.GetBron = "Maurice de Hond";
                newObject.GetCategorie = Categorie.Zetels;
                newObject.GetTijd = Convert.ToDateTime(ListTime);
                ListObjecten.Add(newObject);
            }

            foreach (var item in ListObjecten)
            {
                DAL.Insert(item);
            }
        }
    }
}