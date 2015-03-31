using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebcrawlerService
{
    public class BNRNieuwsCrawler: BaseCrawler
    {
        public List<IGenericObject> Crawl()
        {
            BNRNieuws bnr = new BNRNieuws();
            return bnr.savedObjecten;
        }
    }
}