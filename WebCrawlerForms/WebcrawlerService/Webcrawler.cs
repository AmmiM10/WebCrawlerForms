using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }
}