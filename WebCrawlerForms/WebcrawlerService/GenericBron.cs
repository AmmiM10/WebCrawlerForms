using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebcrawlerService
{
    public class GenericBron
    {
        public List<List<IGenericObject>> CrawlAllSources()
        {
            var allObjecten = new List<List<IGenericObject>>();

            BaseCrawler ZetelObject = new ZetelCrawler();
            allObjecten.Add(ZetelObject.Crawl());

            BaseCrawler NOSNieuwsObject = new NOSNieuwsCrawler();
            allObjecten.Add(NOSNieuwsObject.Crawl());

            BaseCrawler BNRNieuwsObject = new BNRNieuwsCrawler();
            allObjecten.Add(BNRNieuwsObject.Crawl());

            BaseCrawler AgendaObject = new AgendapuntenCrawler();
            allObjecten.Add(AgendaObject.Crawl());

            BaseCrawler WetsvoorstellenObject = new WetsvoorstellenCrawler();
            allObjecten.Add(WetsvoorstellenObject.Crawl());

            return allObjecten;
        }
    }
}