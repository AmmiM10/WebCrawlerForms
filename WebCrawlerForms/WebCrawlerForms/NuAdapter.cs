using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebCrawlerForms
{
    public class NuAdapter: BronInterface
    {
        private string _link;
        public string PropLink
        {
            get
            {
                return _link;
            }
            set
            {
                _link = value;
            }
        }

        public List<string> GetHeadlines()
        {
            return new Tekst().getItems("http://www.nu.nl/rss/politiek", "<description>(.+?)</description>");
        }

        public List<string> GetHeadlineLinks()
        {
            return new Tekst().getItems("http://www.nu.nl/rss/politiek", "</title><link>(.+?)</link>");
        }

        public List<string> GetVideos()
        {
            //throw new NotImplementedException();
            return null;
        }

        public string GetVideo()
        {
            //throw new NotImplementedException();

            return null;
        }

        public string GetTekst()
        {
            return new Tekst().GetTekst(_link, "<div class=\"block-content\">\\s*(.+?)<p><a href");
        }
    }
}
