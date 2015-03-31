using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebcrawlerService
{
    public class ZetelCrawler: BaseCrawler
    {
        public List<IGenericObject> Crawl()//IGenericObject igo)
        {
            Zetels z = new Zetels();//igo);
            return z.savedObjecten;
        }
    }
}