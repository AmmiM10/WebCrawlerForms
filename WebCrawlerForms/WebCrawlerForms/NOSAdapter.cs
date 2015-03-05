using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebCrawlerForms
{
    public class NOSAdapter: BronInterface
    {
        private string _naam;
        public string Naam { get { return _naam; } set { _naam = value; } }
        private string _link;
        public string PropLink { get { return _link; } set { _link = value; } }

        public List<string> GetHeadlines()
        {
            List<string> Headlines =  new Tekst().getItems("http://nos.nl/nieuws/politiek/archief/", "class=\"list-time__title link-hover\">(.+?)</div></a></li><li class=\"list-time__item\">");
            //Dit is voor laatste headline maar lukt niet want is het zelfde begin maar ander einde </ul>
            //Headlines.Add(new Tekst().getItems("http://nos.nl/nieuws/politiek/archief/", "class=\"list-time__title link-hover\">(.+?)</div></a></li><li class=\"list-time__item\">"));
            return Headlines;
        }

        public List<string> GetHeadlineLinks()
        {
            return new Tekst().getItems("http://nos.nl/nieuws/politiek/archief/", "<li class=\"list-time__item\"><a href=\"(.+?)\" class=\"link-block\">");
        }

        public List<string> GetTime(List<string> Links)
        {
            return new Tekst().getItems("http://nos.nl/nieuws/politiek/archief/", "<time datetime=\"(.+?)\">");
        }

        public List<string> GetVideos()
        {
            return new Tekst().getItems("http://www.nos.nl" + _link, "<div class=\"video-play\"><a href=\"(.+?)\" class=\"video-play__link js-ajax\" ");
        }
        
        public string GetVideo(string url)
        {
            return new Video().getVideo("http://www.nos.nl" + url, new List<string> { "data-label=\"Laag - 360p\"  /><source\\s*src=\"(.+?)\" type=\"480p\" data-label=\"", "data-label=\"Normaal - 480p\"  /><source src=\"(.+?)\" type=\"360p\" data-label=\"" });
        }

        public string GetTekst()
        {
            //return new Tekst().GetTekst(_link, "<div class=\"article_textwrap\"><p>\\s*(.+?)</p></div></div></section><footer class=\"container\">");
            //return new Tekst().GetTekst(_link, "<div class=\"article_textwrap\"><p>\\s*(.+?)</p></div></div><div class=\"article_block\">");
            return new Tekst().GetTekst("http://www.nos.nl" + _link, "<div class=\"article_textwrap\"><p>\\s*(.+?)</p></div>");
        }
    }
}
