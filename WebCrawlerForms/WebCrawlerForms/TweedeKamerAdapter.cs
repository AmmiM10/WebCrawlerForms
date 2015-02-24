using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebCrawlerForms
{
    public class TweedeKamerAdapter: BronInterface
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
            return new Tekst().getItems("http://www.tweedekamer.nl/nieuws/kamernieuws/", "class=\"list-time__title link-hover\">(.+?)</div></a></li><li class=\"list-time__item\">");
        }

        public List<string> GetHeadlineLinks()
        {
            return new Tekst().getItems("http://www.tweedekamer.nl/nieuws/kamernieuws/", "<h2><a href=\"(.+?)\">");
        }

        public List<string> GetVideos()
        {
            throw new NotImplementedException();
        }

        public string GetVideo()
        {
            throw new NotImplementedException();
        }

        public string GetTekst()
        {
            throw new NotImplementedException();
        }
    }
}
