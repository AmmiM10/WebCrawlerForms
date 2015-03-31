using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebcrawlerService
{
    public class AgendapuntenCrawler: BaseCrawler
    {
        public List<IGenericObject> Crawl()
        {
            Agendapunten agenda = new Agendapunten();
            return agenda.savedObjecten;
        }
    }
}