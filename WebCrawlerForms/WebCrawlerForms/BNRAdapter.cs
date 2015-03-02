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
            return new Tekst().getItems(_link, "mb6\" onclick=\"window.open('(.+?)',");
        }

        public List<string> GetTime(List<string> Links)
        {
            return new Tekst().getItems("http://www.bnr.nl/nieuws/politiek/?widget=rssfeed&view=feed&contentId=1227456", "<pubDate>(.+?)</pubDate>");
        }

        public string GetVideo()
        {
            string link = new Tekst().getLinkVideo(_link, "mb6\" onclick=\"window.open('(.+?)'");
            return new Video().getVideo(link, new List<string> { "data-label=\"Laag - 360p\"  /><source src=\"(.+?)\" type=\"480p\" data-label=\"", "data-label=\"Normaal - 480p\"  /><source src=\"(.+?)\" type=\"360p\" data-label=\"" });
        }

        public string GetTekst()
        {
            return new Tekst().GetTekst(_link, "itemprop=\"articleBody\">\\s*(.+?)</span");
        }
    }
}
