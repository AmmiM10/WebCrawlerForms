using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebcrawlerService
{
    public class GenericController: BaseCrawler
    {
        public void Crawl()
        {
            GenericModel model = new GenericModel();
            model.CrawlBestand();
        }
    }
}