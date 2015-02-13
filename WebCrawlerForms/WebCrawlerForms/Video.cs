using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace WebCrawlerForms
{
    public class Video
    {
        private string _url;
        private string _tags;

        public Video()
        {

        }

        public string getVideo(string url, string tags)
        {
            this._url = url;
            this._tags = tags;
            WebClient wc = new WebClient();
            String html = wc.DownloadString("http://www.nos.nl/video/" + _url);
            MatchCollection m1 = Regex.Matches(html, _tags, RegexOptions.Singleline);
            string webstring = "";
            if (m1.Count > 0)
            {
                foreach (Match m in m1)
                {
                    string score = m.Groups[1].Value;
                    score = HttpUtility.HtmlDecode(score);
                    score = Regex.Replace(score, "<.*?>", string.Empty);
                    webstring = score;
                }
            }

            return webstring;
        }
    }
}
