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
        public Video()
        {

        }

        public string getVideo(string url, List<string> tags)
        {
            WebClient wc = new WebClient();
            String html = wc.DownloadString(url);
            MatchCollection m1 = Regex.Matches(html, tags[0], RegexOptions.Singleline);
            string webstring = "";
            foreach (var item in tags)
            {
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
                else
                {
                    m1 = Regex.Matches(html, item, RegexOptions.Singleline);
                    foreach (Match m in m1)
                    {
                        string score = m.Groups[1].Value;
                        score = HttpUtility.HtmlDecode(score);
                        score = Regex.Replace(score, "<.*?>", string.Empty);
                        webstring = score;
                    }
                }
            }

            return webstring;
        }
    }
}
