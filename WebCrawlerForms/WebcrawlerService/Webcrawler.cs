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

            //List<List<IGenericObject>> content = new GenericBron().CrawlAllSources();
            //for (int i = 0; i < content.Count; i++)
            //{
            //    for (int j = 0; j < content[i].Count; j++)
            //    {
            //        DAL.Insert(content[i][j]);
            //    }
            //}
        }
    }
}