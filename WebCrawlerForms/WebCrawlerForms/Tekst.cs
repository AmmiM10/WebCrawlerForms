using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;

namespace WebCrawlerForms
{
    public class Tekst
    {
        private string _url;
        private string _tags;

        public Tekst()
        {
            
        }

        public string GetTekst(string url, string tags)
        {
            this._url = url;
            this._tags = tags;
            WebClient wc = new WebClient();
            // url = http://nos.nl/nieuws/politiek/
            String html = wc.DownloadString(_url);
            // voorbeeld = "</li>\\s*<li>\\s*<a href=\"(.+?)\">\\s*<article id="
            MatchCollection m1 = Regex.Matches(html, _tags, RegexOptions.Singleline);
            string webtekst = null;
            if (m1.Count > 0)
            {
                foreach (Match m in m1)
                {
                    string score = m.Groups[1].Value;
                    //Encoding ddd = UTF8Encoding.GetEncoding(score);
                    score = HttpUtility.HtmlDecode(score);
                    score = Regex.Replace(score, "<.*?>", string.Empty);
                    webtekst += score;
                }
            }

            return webtekst;
        }

        public List<string> getItems(string url, string tags)
        {
            this._url = url;
            this._tags = tags;
            WebClient wc = new WebClient();
            // url = http://nos.nl/nieuws/politiek/
            String html = wc.DownloadString(_url);
            // voorbeeld = "</li>\\s*<li>\\s*<a href=\"(.+?)\">\\s*<article id="
            MatchCollection m1 = Regex.Matches(html, _tags, RegexOptions.Singleline);
            List<string> webitems = new List<string>();
            if (m1.Count > 0)
            {
                foreach (Match m in m1)
                {
                    string score = m.Groups[1].Value;
                    score = HttpUtility.HtmlDecode(score);
                    score = Regex.Replace(score, "<.*?>", string.Empty);
                    webitems.Add(score);
                }
            }

            return webitems;
        }
    }
}