using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebCrawlerForms
{
    public class BNRAdapter : BronInterface
    {
        private string _link;
        private string _naam;
        public string Naam { get { return _naam; } set { _naam = value; } }
        public string PropLink { get { return _link; } set { _link = value; } }

        public List<string> GetHeadlines()
        {
            return new Tekst().getItems("http://www.bnr.nl/nieuws/politiek/?widget=rssfeed&view=feed&contentId=1227456", "<item>\\s*<title>(.+?)</title>");
        }

        public List<string> GetHeadlineLinks()
        {
            List<string> links = new Tekst().getItems("http://www.bnr.nl/nieuws/politiek/?widget=rssfeed&view=feed&contentId=1227456", "</title>\\s*<link>(.+?)</link>");
            return links;
        }
         
        public List<string> GetVideos()
        {
            List<string> videoUrls = new Tekst().getItems(_link, "service=player&type=fragment&articleId=(.+?)',");
            for (int i = 0; i < videoUrls.Count; i++)
            {
                if (videoUrls[i].Length > 23)
                {
                    videoUrls.RemoveAt(i);
                    i--;
                }
                else
                    videoUrls[i] = "http://www.bnr.nl/?service=player&type=fragment&articleId=" + videoUrls[i];
            }
            return videoUrls;
        }

        public List<string> GetTime(List<string> Links)
        {
            return new Tekst().getItems("http://www.bnr.nl/nieuws/politiek/?widget=rssfeed&view=feed&contentId=1227456", "<pubDate>(.+?)</pubDate>");
        }

        public string GetVideo(string url)
        {
            string returnurl = new Video().getVideo(url, new List<string> { "http://www.bnr.nl/feeds/audio/(.+?).mp3" });

            returnurl = "http://www.bnr.nl/feeds/audio/" + returnurl + ".mp3";
            return returnurl;
        }

        public string GetTekst()
        {
            return new Tekst().GetTekst(_link, "itemprop=\"articleBody\">\\s*(.+?)</span");
        }
    }
}
