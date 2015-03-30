using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebcrawlerService
{
    public class Zetels: IGenericObject
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
        private DateTime tijd;
        public DateTime GetTijd { get { return tijd; } set { tijd = value; } }
        private Categorie categorie;
        public Categorie GetCategorie { get { return categorie; } set { categorie = value; } }

        public List<string> HaalZetelsOp()
        {
            List<string> peilingen = new CrawlContent().getItems("http://frontbencher.nl/peilingen/", "<li class=\"dehond\"><div class=\"bar\" style=\"height:(.+?)</div>");
            for (int i = 0; i < peilingen.Count; i++)
            {
                peilingen[i] = peilingen[i].Split('>')[1];
            }
            List<string> titels = new CrawlContent().getItems("http://frontbencher.nl/peilingen/", "title=\"Peilingen (.+?)\" alt");
            return peilingen;
        }

        public List<string> HaalPartijenOp()
        {
            List<string> titels = new CrawlContent().getItems("http://frontbencher.nl/peilingen/", "title=\"Peilingen (.+?)\" alt");

            return titels;
        }

        public string HaalDatumOp()
        {
            string datum = new CrawlContent().GetTekst("http://frontbencher.nl/peilingen/", "Maurice de Hond<span>(.+?)</span>");

            return datum;
        }

        public List<IGenericObject> GetAllSources()
        {
            List<IGenericObject> ListObjecten = new List<IGenericObject>();

            List<string> ListInhoud = HaalZetelsOp();
            List<string> ListHeadlines = HaalPartijenOp();
            //List<string> ListHeadlinesLink = GetHeadlineLinks();
            string ListTime = HaalDatumOp().Replace(".", "");
            DateTime dtDatum = DateTime.ParseExact(ListTime, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //List<string> ListVideo = GetVideos();

            for (int i = 0; i < ListHeadlines.Count; i++)
            {
                IGenericObject newObject = new Zetels();
                newObject.GetTitel = ListHeadlines[i];
                newObject.GetBeschrijving = ListInhoud[i];
                newObject.GetBron = "Maurice de Hond";
                newObject.GetTijd = dtDatum;

                //for (int j = 0; j < ListVideo.Count; j++)
                //{
                //    this.GetMedia += ListVideo[i] + ";";
                //}
                newObject.GetCategorie = Categorie.Zetels;
                ListObjecten.Add(newObject);
            }

            return ListObjecten;
        }
    }
}