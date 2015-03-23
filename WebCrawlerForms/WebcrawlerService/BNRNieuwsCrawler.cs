using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebcrawlerService
{
    public class BNRNieuwsCrawler: IGenericObject
    {
        private string _link;
        private string _naam;
        public string Naam { get { return _naam; } set { _naam = value; } }
        public string PropLink { get { return _link; } set { _link = value; } }

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
        private string datum;
        public string GetDatum { get { return datum; } set { datum = value; } }
        private Categorie categorie;
        public Categorie GetCategorie { get { return categorie; } set { categorie = value; } }

        public List<string> GetHeadlines()
        {
            return new CrawlContent().getItems("http://www.bnr.nl/nieuws/politiek/?widget=rssfeed&view=feed&contentId=1227456", "<item>\\s*<title>(.+?)</title>");
        }

        public List<string> GetHeadlineLinks()
        {
            List<string> links = new CrawlContent().getItems("http://www.bnr.nl/nieuws/politiek/?widget=rssfeed&view=feed&contentId=1227456", "</title>\\s*<link>(.+?)</link>");
            return links;
        }

        public List<string> GetVideos()
        {
            List<string> videoUrls = new CrawlContent().getItems(_link, "MyNewService=player&type=fragment&articleId=(.+?)',");
            for (int i = 0; i < videoUrls.Count; i++)
            {
                if (videoUrls[i].Length > 23)
                {
                    videoUrls.RemoveAt(i);
                    i--;
                }
                else
                    videoUrls[i] = "http://www.bnr.nl/?MyNewService=player&type=fragment&articleId=" + videoUrls[i];
            }
            return videoUrls;
        }

        public List<string> GetTime()
        {
            return new CrawlContent().getItems("http://www.bnr.nl/nieuws/politiek/?widget=rssfeed&view=feed&contentId=1227456", "<pubDate>(.+?)</pubDate>");
        }

        public string GetVideo(string url)
        {
            string returnurl = new CrawlContent().getVideo(url, new List<string> { "http://www.bnr.nl/feeds/audio/(.+?).mp3" });

            returnurl = "http://www.bnr.nl/feeds/audio/" + returnurl + ".mp3";
            return returnurl;
        }

        public string GetTekst()
        {
            return new CrawlContent().GetTekst(_link, "itemprop=\"articleBody\">\\s*(.+?)</span");
        }

        public List<IGenericObject> GetAllSources()
        {
            List<IGenericObject> ListAllObjecten = new List<IGenericObject>();

            List<string> ListHeadlines = GetHeadlines();
            List<string> ListHeadlinesLink = GetHeadlineLinks();

            for (int i = 0; i < ListHeadlines.Count; i++)
            {
                IGenericObject go = new BNRNieuwsCrawler();
                go.GetTitel = ListHeadlines[i];
                PropLink = ListHeadlinesLink[i];
                List<string> ListVideo = GetVideos();
                List<string> ListTime = GetTime();
                go.GetBeschrijving = GetTekst();
                if (go.GetBeschrijving == null)
                {
                    go.GetBeschrijving = "";
                }
                go.GetBron = "BNR";
                go.GetDatum = ListTime[i];
                go.GetLink = ListHeadlinesLink[i];

                //for (int j = 0; j < ListVideo.Count; j++)
                //{
                //    go.GetMedia += ListVideo[j] + ";";
                //}
                go.GetCategorie = Categorie.Nieuws;
                ListAllObjecten.Add(go);
            }

            return ListAllObjecten;
        }
    }
}