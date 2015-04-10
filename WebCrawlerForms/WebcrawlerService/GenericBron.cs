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
            /*BaseCrawler NOSNieuwsObject = new NOSNieuwsCrawler();
            NOSNieuwsObject.Crawl();*/

            BaseCrawler BNRNieuwsObject = new BNRNieuwsCrawler();
            BNRNieuwsObject.Crawl();

            BaseCrawler ZetelObject = new ZetelCrawler();
            ZetelObject.Crawl();

            BaseCrawler AgendaObject = new AgendapuntenCrawler();
            AgendaObject.Crawl();

            BaseCrawler WetsvoorstellenObject = new WetsvoorstellenCrawler();
            WetsvoorstellenObject.Crawl();
        }
    }
}