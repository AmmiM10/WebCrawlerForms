using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebcrawlerService
{
    public class BNRNieuwsController: BaseCrawler
    {
        public void Crawl()
        {
            BNRNieuwsModel bnr = new BNRNieuwsModel();
            bnr.GetAllSources();
        }
    }
}