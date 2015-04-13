using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace WebcrawlerService
{
    public class GenericBron
    {
        public void CrawlAllSources()
        {
            BaseCrawler NOSNieuwsObject = new NOSNieuwsController();
            NOSNieuwsObject.Crawl();

            BaseCrawler BNRNieuwsObject = new BNRNieuwsController();
            BNRNieuwsObject.Crawl();

            BaseCrawler ZetelObject = new ZetelController();
            ZetelObject.Crawl();

            BaseCrawler AgendaObject = new AgendapuntenController();
            AgendaObject.Crawl();

            BaseCrawler WetsvoorstellenObject = new WetsvoorstellenController();
            WetsvoorstellenObject.Crawl();
        }
    }
}