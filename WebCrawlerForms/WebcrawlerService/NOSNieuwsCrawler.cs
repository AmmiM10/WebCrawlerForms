using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebcrawlerService
{
    public class NOSNieuwsCrawler: IGenericObject
    {
        private string _naam;
        public string Naam { get { return _naam; } set { _naam = value; } }
        private string _link;
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
            List<string> Headlines = new CrawlContent().getItems("http://nos.nl/nieuws/politiek/archief/", "class=\"list-time__title link-hover\">(.+?)</div></a></li><li class=\"list-time__item\">");
            //Dit is voor laatste headline maar lukt niet want is het zelfde begin maar ander einde </ul>
            //Headlines.Add(new Tekst().getItems("http://nos.nl/nieuws/politiek/archief/", "class=\"list-time__title link-hover\">(.+?)</div></a></li><li class=\"list-time__item\">"));
            return Headlines;
        }

        public List<string> GetHeadlineLinks()
        {
            return new CrawlContent().getItems("http://nos.nl/nieuws/politiek/archief/", "<li class=\"list-time__item\"><a href=\"(.+?)\" class=\"link-block\">");
        }

        public List<string> GetTime()
        {
            return new CrawlContent().getItems("http://nos.nl/nieuws/politiek/archief/", "<time datetime=\"(.+?)\">");
        }

        public List<string> GetVideos()
        {
            return new CrawlContent().getItems("http://www.nos.nl" + _link, "<div class=\"video-play\"><a href=\"(.+?)\" class=\"video-play__link js-ajax\" ");
        }

        public string GetVideo(string url)
        {
            return new CrawlContent().getVideo("http://www.nos.nl" + url, new List<string> { "data-label=\"Laag - 360p\"  /><source\\s*src=\"(.+?)\" type=\"480p\" data-label=\"", "data-label=\"Normaal - 480p\"  /><source src=\"(.+?)\" type=\"360p\" data-label=\"" });
        }

        public string GetTekst()
        {
            return new CrawlContent().GetTekst("http://www.nos.nl" + _link, "<div class=\"article_textwrap\"><p>\\s*(.+?)</p></div>");
        }

        public IGenericObject GetEverything()
        {
            List<string> ListHeadlines = GetHeadlines();
            List<string> ListHeadlinesLink = GetHeadlineLinks();
            List<string> ListTime = GetTime();
            List<string> ListVideo = GetVideos();

            for (int i = 0; i < ListHeadlines.Count; i++)
            {
                this.GetTitel = ListHeadlines[i];
                PropLink = ListHeadlinesLink[i];
                this.GetBeschrijving = GetTekst();
                this.GetBron = "NOS";
                this.GetDatum = ListTime[i];
                this.GetLink = ListHeadlinesLink[i];

                for (int j = 0; j < ListVideo.Count; j++)
                {
                    this.GetMedia += ListVideo[i] + ";";
                }
                this.GetCategorie = Categorie.Nieuws;
            }

            return this;
        }
    }
}