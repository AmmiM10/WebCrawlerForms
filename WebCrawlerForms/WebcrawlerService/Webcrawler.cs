using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace WebcrawlerService
{
    public enum Categorie
    { 
        Nieuws, Zetels, Agendapunten, Wetsvoorstellen
    }

    public class Webcrawler
    {
        public void CrawlContent()
        {
            var content = new GenericBron();
            content.CrawlAllSources();
        }

        public void DeleteContent()
        {
            double dag = 0;

            XDocument doc = XDocument.Load("Properties.xml");
            var duur = doc.Descendants("nieuwsduur");

            foreach (var item in duur)
            {
                dag = Convert.ToDouble(item.Value);
            }
            
            DAL.Delete(DateTime.Now.AddDays(dag));
        }
    }
}