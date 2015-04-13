using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebcrawlerService
{
    public class WetsvoorstellenController: BaseCrawler
    {
        public void Crawl()
        {
            WetsvoorstellenModel wet = new WetsvoorstellenModel();
            wet.GetAllSources();
        }
    }
}