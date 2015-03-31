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
            //List<string> ListHeadlinesLink = GetHeadlineLinks();
            string ListTime = HaalDatumOp().Replace(".", "");

            //Maikel, kijk hier nog ff naar
            //DateTime dtDatum = DateTime.ParseExact(ListTime, "dd MM yyyy", System.Globalization.CultureInfo.InvariantCulture);

            //List<string> ListVideo = GetVideos();

            for (int i = 0; i < ListHeadlines.Count; i++)
            {
                GenericObject newObject = new Zetels();
                newObject.GetTitel = ListHeadlines[i];
                newObject.GetBeschrijving = ListInhoud[i];
                newObject.GetBron = "Maurice de Hond";
                //newObject.GetTijd = dtDatum;

                //for (int j = 0; j < ListVideo.Count; j++)
                //{
                //    this.GetMedia += ListVideo[i] + ";";
                //}
                newObject.GetCategorie = Categorie.Zetels;
                ListObjecten.Add(newObject);
            }

            foreach (var item in ListObjecten)
            {
                DAL.Insert(item);
            }
        }
    }
}