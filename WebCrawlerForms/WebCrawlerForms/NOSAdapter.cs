using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebCrawlerForms
{
    public class NOSAdapter
    {
        private string _link;
        public string PropLink { get { return _link; } set { _link = value; } }

        public List<string> GetHeadlines()
        {
            return new Tekst().getItems("http://nos.nl/nieuws/politiek/archief/", "class=\"list-time__title link-hover\">(.+?)</div></a></li><li class=\"list-time__item\">");
        }

        public List<string> GetHeadlineLinks()
        {
            return new Tekst().getItems("http://nos.nl/nieuws/politiek/archief/", "<li class=\"list-time__item\"><a href=\"(.+?)\" class=\"link-block\">");
        }

        public List<string> GetVideos()
        {
            return new Tekst().getItems(_link, "<div class=\"video-play\"><a href=(.+?) class=\"video-play__link js-ajax\" ");
        }
        
        public string GetVideo()
        {
            string link = new Tekst().getLinkVideo(_link, "<div class=\"video-play\"><a href=\"(.+?)\" class=\"video-play__link js-ajax\" ");
            return new Video().getVideo(link, "data-label=\"Laag - 360p\"  /><source src=\"(.+?)\" type=\"480p\" data-label=\"");
        }

        public string GetTekst()
        {
            //return new Tekst().GetTekst(_link, "<div class=\"article_textwrap\"><p>\\s*(.+?)</p></div></div></section><footer class=\"container\">");
            //return new Tekst().GetTekst(_link, "<div class=\"article_textwrap\"><p>\\s*(.+?)</p></div></div><div class=\"article_block\">");
            return new Tekst().GetTekst(_link, "<div class=\"article_textwrap\"><p>\\s*(.+?)</p></div></div>");
        }
    }
}
