using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebcrawlerService
{
    public class GenericBron
    {
        public List<List<IGenericObject>> CrawlAllSources()
        {
            var Objecten = new List<List<IGenericObject>>();
            Objecten.Add(CrawlNOS());
            Objecten.Add(CrawlBNR());
            //Objecten.Add(CrawlWetsvoorstellen());
            //Objecten.Add(CrawlZetels());

            return Objecten;
        }

        private List<IGenericObject> CrawlNOS()
        {
            return new NOSNieuwsCrawler().GetAllSources();
        }

        private List<IGenericObject> CrawlBNR()
        {
            return new BNRNieuwsCrawler().GetAllSources();
        }

        private List<IGenericObject> CrawlWetsvoorstellen()
        {
            return new Wetsvoorstellen().GetAllSources();
        }

        private List<IGenericObject> CrawlZetels()
        {
            return new Zetels().GetAllSources();
        }
    }
}