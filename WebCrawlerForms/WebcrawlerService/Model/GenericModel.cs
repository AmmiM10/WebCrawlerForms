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
        private string videoExtra;
        private string videoLinks;
        private string videoLinksExtra;
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
            videoExtra = Regex.Split(sb.ToString(), "<videoExtra>(.+?)</videoExtra>")[1];
            videoLinks = Regex.Split(sb.ToString(), "<videoLinks>(.+?)</videoLinks>")[1];
            videoLinksExtra = Regex.Split(sb.ToString(), "<videoLinksExtra>(.+?)</videoLinksExtra>")[1];
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

        private List<string> GetTime(int aantal)
        {
            if (time == " ")
            {
                List<string> tijden = new List<string>();
                for (int i = 0; i < aantal; i++)
                {
                    tijden.Add(DateTime.Now.Date.ToString());
                }
                return tijden;
            }
            else
            {
                return new CrawlContent().getItems(link, time);
            }
        }

        private List<string> GetVideos()
        {
            return new CrawlContent().getItems(siteLink + _link, videoLinks);
        }

        private string GetVideo(string url)
        {
            return new CrawlContent().getVideo(videoLinksExtra + url, new List<string> { video });;
        }

        private string GetTekst()
        {
            return new CrawlContent().GetTekst(siteLink + _link, tekst);
        }

        private void GetAllSources()
        {
            List<IGenericObject> ListAllObjecten = new List<IGenericObject>();

            List<string> ListHeadlines = GetHeadlines();
            List<string> ListHeadlinesLink = GetHeadlineLinks();
            List<string> ListTime = GetTime(ListHeadlines.Count);
            if (ListTime.Count == 1)
            {
                for (int j = 0; j < ListHeadlines.Count; j++)
                {
                    ListTime.Add(ListTime[0]);
                }
            }
            List<string> Tijd = new List<string>();
            for (int i = 0; i < ListHeadlines.Count; i++)
            {
                GenericObject go = new GenericModel();
                go.GetTitel = ListHeadlines[i];
                PropLink = ListHeadlinesLink[i];
                if (video != " ")
                {
                    List<string> ListVideo = GetVideos();
                    for (int j = 0; j < ListVideo.Count; j++)
                    {
                        if (ListVideo[j].Length > 100)
                        {
                            ListVideo.Remove(ListVideo[j]);
                            j--;
                        }
                        else
                        {
                            if(titel == "BNR") go.GetMedia += videoExtra + GetVideo(ListVideo[j]) + ";";
                            else go.GetMedia += GetVideo(videoExtra + ListVideo[j]) + ";";
                        }
                    }
                }

                if (titel == " ") { go.GetBeschrijving = GetTime(ListHeadlines.Count)[0]; go.GetBron = "Zetels"; }
                else { go.GetBeschrijving = GetTekst(); go.GetBron = titel; }
                if (go.GetBeschrijving == null)
                {
                    go.GetBeschrijving = "";
                }

                go.GetTijd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(Convert.ToDateTime(ListTime[i]), "W. Europe Standard Time");
                go.GetLink = ListHeadlinesLink[i];                
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