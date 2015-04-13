using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebcrawlerService
{
    public class AgendapuntenController: BaseCrawler
    {
        public void Crawl()
        {
            AgendapuntenModel agenda = new AgendapuntenModel();
            agenda.GetAllSources();
        }
    }
}