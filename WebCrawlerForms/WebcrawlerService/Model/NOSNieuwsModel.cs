﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebcrawlerService
{
    public class NOSNieuwsModel: GenericObject
    {
        private string _naam;
        public string Naam { get { return _naam; } set { _naam = value; } }
        private string _link;
        public string PropLink { get { return _link; } set { _link = value; } }

        private List<string> GetHeadlines()
        {
            List<string> Headlines = new CrawlContent().getItems("http://nos.nl/nieuws/politiek/archief/", "class=\"list-time__title link-hover\">(.+?)</div></a></li><li class=\"list-time__item\">");
            return Headlines;
        }

        private List<string> GetHeadlineLinks()
        {
            return new CrawlContent().getItems("http://nos.nl/nieuws/politiek/archief/", "<li class=\"list-time__item\"><a href=\"(.+?)\" class=\"link-block\">");
        }

        private List<string> GetTime()
        {
            return new CrawlContent().getItems("http://nos.nl/nieuws/politiek/archief/", "<time datetime=\"(.+?)\">");
        }

        private List<string> GetVideos()
        {
            return new CrawlContent().getItems("http://www.nos.nl" + _link, "<div class=\"video-play\"><a href=\"(.+?)\" class=\"video-play__link js-ajax\" ");
        }

        private string GetVideo(string url)
        {
            return new CrawlContent().getVideo("http://www.nos.nl" + url, new List<string> { "data-label=\"Laag - 360p\"  /><source\\s*src=\"(.+?)\" type=\"480p\" data-label=\"", "data-label=\"Normaal - 480p\"  /><source src=\"(.+?)\" type=\"360p\" data-label=\"" });
        }

        private string GetTekst()
        {
            return new CrawlContent().GetTekst("http://www.nos.nl" + _link, "<div class=\"article_textwrap\"><p>\\s*(.+?)</p></div>");
        }

        public void GetAllSources()
        {
            List<IGenericObject> ListAllObjecten = new List<IGenericObject>();

            List<string> ListHeadlines = GetHeadlines();
            List<string> ListHeadlinesLink = GetHeadlineLinks();
            List<string> ListTime = GetTime();
            List<string> Tijd = new List<string>();

            for (int i = 0; i < ListHeadlines.Count; i++)
            {
                GenericObject go = new NOSNieuwsModel();
                go.GetTitel = ListHeadlines[i];
                PropLink = ListHeadlinesLink[i];
                List<string> ListVideo = GetVideos();
                go.GetBeschrijving = GetTekst();
                if (go.GetBeschrijving == null)
                {
                    go.GetBeschrijving = "";
                }

                go.GetBron = "NOS";
                Tijd.Add(ListTime[i].Split('T')[1]);
                Tijd[i] = Tijd[i].Split('+')[0];
                ListTime[i] = ListTime[i].Split('T')[0] + " " + Tijd[i];
                go.GetTijd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(Convert.ToDateTime(ListTime[i]), "W. Europe Standard Time");
                go.GetLink = ListHeadlinesLink[i];

                for (int j = 0; j < ListVideo.Count; j++)
                {
                    if (j == 5)
                    {
                        j = ListVideo.Count;
                    }
                    go.GetMedia += GetVideo(ListVideo[j]) + ";";
                }
                go.GetCategorie = Categorie.Nieuws;
                ListAllObjecten.Add(go);
            }

            foreach (var item in ListAllObjecten)
            {
                DAL.Insert(item);
            }
        }
    }
}