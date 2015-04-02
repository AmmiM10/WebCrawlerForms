using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace WebcrawlerService
{
    public class GenericBron
    {
        public void CrawlAllSources()
        {
            //BaseCrawler NOSNieuwsObject = new NOSNieuwsCrawler();
            //NOSNieuwsObject.Crawl();

            //BaseCrawler BNRNieuwsObject = new BNRNieuwsCrawler();
            //BNRNieuwsObject.Crawl();

            //BaseCrawler ZetelObject = new ZetelCrawler();
            //ZetelObject.Crawl();

            BaseCrawler AgendaObject = new AgendapuntenCrawler();
            AgendaObject.Crawl();

            //BaseCrawler WetsvoorstellenObject = new WetsvoorstellenCrawler();
            //WetsvoorstellenObject.Crawl();

            StringBuilder sb = new StringBuilder();
            using (StreamReader sr = new StreamReader("C:/Users/Gebruiker/Desktop/WebCrawlerForms.git/trunk/WebCrawlerForms/Properties.txt"))
            {
                while (sr.Peek() >= 0)
                {
                    sb.Append(sr.ReadLine());
                }
            }
            int duur = Convert.ToInt32(sb.ToString().Split(':')[1].Split(';')[0]) / -1;
            DateTime duurdatum = DateTime.Today.AddDays(duur);
            DAL.Delete(duurdatum);
        }
    }
}