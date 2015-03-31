using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebcrawlerService
{
    public class WetsvoorstellenCrawler: BaseCrawler
    {
        public void Crawl()
        {
            Wetsvoorstellen wet = new Wetsvoorstellen();
            wet.GetAllSources();
        }
    }
}