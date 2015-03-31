using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebcrawlerService
{
    public interface BaseCrawler
    {
        List<IGenericObject> Crawl();//IGenericObject igo);
    }
}