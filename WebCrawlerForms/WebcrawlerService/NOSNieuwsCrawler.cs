using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebcrawlerService
{
    public class NOSNieuwsCrawler: BaseCrawler
    {
        public void Crawl()
        {
            NOSNieuws nos = new NOSNieuws();
            nos.GetAllSources();
        }
    }
}