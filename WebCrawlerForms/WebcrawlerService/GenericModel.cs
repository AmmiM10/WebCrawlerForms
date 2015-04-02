using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebcrawlerService
{
    public class GenericModel: GenericObject
    {
        private string bron;
        private string _link = null;
        public GenericModel(string bron)
        {
            this.bron = bron;
        }

        public void GetAllSources()
        {
            List<IGenericObject> listAllObjecten = new List<IGenericObject>();
            bool headlinesChecken = true;//read.bron.nieuws

            if (headlinesChecken)
            {
                var headlines = GetHeadlines();
                var headlineLinks = GetHeadlineLinks();

                for (int i = 0; i < headlineLinks.Count(); i++)
                {
                    _link = headlineLinks[i];
                    var tekst = GetTekst();
                    var listVideos = GetVideos();
                    var media = string.Empty;
                    foreach (var item in listVideos)
                    {
                        media += GetVideo(item) + ";";
                    }
                    var tijd = HaalDatumOp();
                }
            }

            
        }

        private List<string> GetHeadlines()
        {
            return new CrawlContent().getItems("read.bron.url", "read.bron.tags"); ;
        }

        private List<string> GetHeadlineLinks()
        {
            return new CrawlContent().getItems("read.bron.url", "<li class=\"list-time__item\"><a href=\"(.+?)\" class=\"link-block\">");
        }

        private string GetTekst()
        {
            return new CrawlContent().GetTekst("read.bron.websiteUrl" + _link, "<div class=\"article_textwrap\"><p>\\s*(.+?)</p></div>");
        }

        private List<string> GetVideos()
        {
            return new CrawlContent().getItems("read.bron.websiteUrl" + _link, "<div class=\"video-play\"><a href=\"(.+?)\" class=\"video-play__link js-ajax\" ");
        }

        private string GetVideo(string url)
        {
            return new CrawlContent().getVideo("read.bron.websiteUrl" + url, new List<string> { "data-label=\"Laag - 360p\"  /><source\\s*src=\"(.+?)\" type=\"480p\" data-label=\"", "data-label=\"Normaal - 480p\"  /><source src=\"(.+?)\" type=\"360p\" data-label=\"" });
        }

        private string HaalDatumOp()
        {
            string datum = new CrawlContent().GetTekst("http://frontbencher.nl/peilingen/", "Maurice de Hond<span>(.+?)</span>");

            return datum;
        }
    }
}