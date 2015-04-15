using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebcrawlerService
{
    public class GenericController: BaseCrawler
    {
        private List<string> allSources = new List<string> {"NOS.xml", "Agendapunten.xml", "Wetsvoorstellen.xml", "Zetels.xml", "BNR.xml" };

        public void Crawl()
        {
            foreach (var item in allSources)
            {
                GenericModel model = new GenericModel();
                model.CrawlBestand(item);
            }
        }
    }
}