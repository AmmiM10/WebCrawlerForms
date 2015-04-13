using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebcrawlerService
{
    public class NOSNieuwsController: BaseCrawler
    {
        public void Crawl()
        {
            NOSNieuwsModel nos = new NOSNieuwsModel();
            nos.GetAllSources();
        }
    }
}