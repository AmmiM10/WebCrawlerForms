using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebcrawlerMyNewService
{
    public class AbstractBron
    {
        public List<List<GenericClassObject>> geteverything()
        {
            List<List<GenericClassObject>> Objects = new List<List<GenericClassObject>>();
            Objects[0].Add(new NOSNieuwsCrawler().getEverything());
            return Objects;
        }
    }
}