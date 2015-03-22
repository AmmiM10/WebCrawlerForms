using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebcrawlerService
{
    public class GenericBron
    {
        public List<List<IGenericObject>> getEverything()
        {
            List<List<IGenericObject>> ListObjects = new List<List<IGenericObject>>();
            ListObjects[0].Add(new NOSNieuwsCrawler().GetEverything());
            return ListObjects;
        }
    }
}