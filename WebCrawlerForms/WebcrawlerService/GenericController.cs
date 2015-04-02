using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebcrawlerService
{
    public class GenericController: BaseCrawler
    {
        public void Crawl(string bron)
        {
            GenericModel model = new GenericModel(bron);
            model.GetAllSources();
        }
    }
}