using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebcrawlerMyNewService
{
    public class Webcrawler
    {
        public void GetEverything()
        {
            List<List<GenericClassObject>> everything = new AbstractBron().geteverything();
            for (int i = 0; i < everything.Count; i++)
            {
                for (int j = 0; j < everything[i].Count; j++)
                {
                    DAL.Insert(everything[i][j]);
                }
            }
        }
    }
}