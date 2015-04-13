using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Linq;

namespace WebcrawlerService
{
    public class GenericModel: GenericObject
    {
        private string _naam;
        public string Naam { get { return _naam; } set { _naam = value; } }
        private string _link;
        public string PropLink { get { return _link; } set { _link = value; } }

        private string titel;
        private string categorie;
        private string headlines;
        private string headlinesLinks;
        private string video;
        private string videoLinks;
        private string tekst;
        private string time;
        private string link;
        private string siteLink;

        public void CrawlBestand(string xmlFileName)
        {
            StringBuilder sb = new StringBuilder();
            using (StreamReader sr = new StreamReader(xmlFileName))
            {
                while (sr.Peek() >= 0)
                {
                    sb.Append(sr.ReadLine());
                }
            }
            titel = Regex.Split(sb.ToString(), "<titel>(.+?)</titel>")[1];
            categorie = Regex.Split(sb.ToString(), "<categorie>(.+?)</categorie>")[1];
            headlines = Regex.Split(sb.ToString(), "<headlines>(.+?)</headlines>")[1];
            headlinesLinks = Regex.Split(sb.ToString(), "<headlinesLinks>(.+?)</headlinesLinks>")[1];
            video = Regex.Split(sb.ToString(), "<video>(.+?)</video>")[1];
            videoLinks = Regex.Split(sb.ToString(), "<videoLinks>(.+?)</videoLinks>")[1];
            tekst = Regex.Split(sb.ToString(), "<tekst>(.+?)</tekst>")[1];
            time = Regex.Split(sb.ToString(), "<time>(.+?)</time>")[1];
            link = Regex.Split(sb.ToString(), "<eigenLink>(.+?)</eigenLink>")[1];
            siteLink = Regex.Split(sb.ToString(), "<siteLink>(.+?)</siteLink>")[1];

            GetAllSources();
        }

        private List<string> GetHeadlines()
        {
            List<string> Headlines = new CrawlContent().getItems(link, headlines);
            return Headlines;
        }

        private List<string> GetHeadlineLinks()
        {
            return new CrawlContent().getItems(link, headlinesLinks);
        }

        private List<string> GetTime()
        {
            return new CrawlContent().getItems(link, time);
        }

        private List<string> GetVideos()
        {
            return new CrawlContent().getItems(siteLink + _link, videoLinks);
        }

        private string GetVideo(string url)
        {
            return new CrawlContent().getVideo(siteLink + url, new List<string> { video });
        }

        private string GetTekst()
        {
            return new CrawlContent().GetTekst(siteLink + _link, tekst);
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
                GenericObject go = new GenericModel();
                go.GetTitel = ListHeadlines[i];
                PropLink = ListHeadlinesLink[i];
                List<string> ListVideo = GetVideos();
                go.GetBeschrijving = GetTekst();
                if (go.GetBeschrijving == null)
                {
                    go.GetBeschrijving = "";
                }

                go.GetBron = titel;

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
                go.GetCategorie = ParseEnum<Categorie>(categorie);
                ListAllObjecten.Add(go);
            }

            foreach (var item in ListAllObjecten)
            {
                DAL.Insert(item);
            }
        }

        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}