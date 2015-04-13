using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebcrawlerService
{
    public class ZetelController: BaseCrawler
    {
        public void Crawl()
        {
            ZetelsModel z = new ZetelsModel();
            z.GetAllSources();
        }
    }
}