using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebcrawlerService
{
    public class WetsvoorstellenCrawler: BaseCrawler
    {
        public List<IGenericObject> Crawl()
        {
            Wetsvoorstellen wet = new Wetsvoorstellen();
            return wet.savedObjecten;
        }
    }
}