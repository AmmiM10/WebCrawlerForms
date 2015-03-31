using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebcrawlerService
{
    public class BNRNieuws: GenericObject
    {
        private string _link;
        private string _naam;
        public string Naam { get { return _naam; } set { _naam = value; } }
        public string PropLink { get { return _link; } set { _link = value; } }

        private List<string> GetHeadlines()
        {
            return new CrawlContent().getItems("http://www.bnr.nl/nieuws/politiek/?widget=rssfeed&view=feed&contentId=1227456", "<item>\\s*<title>(.+?)</title>");
        }

        private List<string> GetHeadlineLinks()
        {
            List<string> links = new CrawlContent().getItems("http://www.bnr.nl/nieuws/politiek/?widget=rssfeed&view=feed&contentId=1227456", "</title>\\s*<link>(.+?)</link>");
            return links;
        }

        private List<string> GetVideos()
        {
            List<string> videoUrls = new CrawlContent().getItems(_link, "articleId=(.+?)'");
            for (int i = 0; i < videoUrls.Count; i++)
            {
                if (videoUrls[i].Length > 23)
                {
                    videoUrls.RemoveAt(i);
                    i--;
                }
                else
                {
                    videoUrls[i] = "http://www.bnr.nl/?service=player&type=fragment&articleId=" + videoUrls[i];
                    videoUrls[i] = GetVideo(videoUrls[i]);
                    if (videoUrls[i].Length < 40)
                    {
                        videoUrls.RemoveAt(i);
                        i--;
                    }
                }

            }
            return videoUrls;
        }

        private List<string> GetTime()
        {
            return new CrawlContent().getItems("http://www.bnr.nl/nieuws/politiek/?widget=rssfeed&view=feed&contentId=1227456", "<pubDate>(.+?)</pubDate>");
        }

        private string GetVideo(string url)
        {
            string returnurl = new CrawlContent().getVideo(url, new List<string> { "http://www.bnr.nl/feeds/audio/(.+?).mp3" });

            returnurl = "http://www.bnr.nl/feeds/audio/" + returnurl + ".mp3";
            return returnurl;
        }

        private string GetTekst()
        {
            return new CrawlContent().GetTekst(_link, "itemprop=\"articleBody\">\\s*(.+?)</span");
        }

        public void GetAllSources()
        {
            List<IGenericObject> ListAllObjecten = new List<IGenericObject>();

            List<string> ListHeadlines = GetHeadlines();
            List<string> ListHeadlinesLink = GetHeadlineLinks();
            List<string> ListTime = GetTime();
            List<string> tijd_split = new List<string>();
            List<string> dag_split = new List<string>();

            for (int i = 0; i < ListHeadlines.Count; i++)
            {
                GenericObject go = new BNRNieuws();
                go.GetTitel = ListHeadlines[i];
                PropLink = ListHeadlinesLink[i];
                List<string> ListVideo = GetVideos();
                go.GetBeschrijving = GetTekst();
                if (go.GetBeschrijving == null)
                {
                    go.GetBeschrijving = "";
                }
                go.GetBron = "BNR";
                string maand = "01";
                if (ListTime[i + 1].Split(' ')[2] == "Feb")
                {
                    maand = "02";
                }
                else if (ListTime[i + 1].Split(' ')[2] == "Mar")
                {
                    maand = "03";
                }
                else if (ListTime[i + 1].Split(' ')[2] == "Apr")
                {
                    maand = "04";
                }
                else if (ListTime[i + 1].Split(' ')[2] == "Mei")
                {
                    maand = "05";
                }
                dag_split.Add(ListTime[i + 1].Split(' ')[3] + "-" + maand + "-" + ListTime[i + 1].Split(' ')[1]);
                tijd_split.Add(dag_split[i] + " " + ListTime[i + 1].Split(' ')[4] + ",531");


                go.GetDag = dag_split[i];
                go.GetTijd = DateTime.ParseExact(tijd_split[i], "yyyy-MM-dd HH:mm:ss,fff",
                                       System.Globalization.CultureInfo.InvariantCulture);
                go.GetLink = ListHeadlinesLink[i];

                for (int j = 0; j < ListVideo.Count; j++)
                {
                    go.GetMedia += ListVideo[j] + ";";
                }
                go.GetCategorie = Categorie.Nieuws;
                ListAllObjecten.Add(go);
            }

            foreach (var item in ListAllObjecten)
            {
                DAL.Insert(item);
            }
        }
    }
}