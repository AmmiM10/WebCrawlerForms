﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebcrawlerService
{
    public class NOSNieuwsCrawler: BaseCrawler
    {
        public List<IGenericObject> Crawl()
        {
            NOSNieuws nos = new NOSNieuws();
            return nos.savedObjecten;
        }
    }
}