using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebcrawlerService
{
    public class ZetelCrawler: BaseCrawler
    {
        public void Crawl()
        {
            Zetels z = new Zetels();
            z.GetAllSources();
        }
    }
}