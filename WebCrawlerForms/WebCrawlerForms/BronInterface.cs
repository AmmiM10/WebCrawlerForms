using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebCrawlerForms
{
    public interface BronInterface
    {
        string PropLink { get; set; }
        string Naam { get; set; }
        List<string> GetHeadlines();
        List<string> GetHeadlineLinks();
        List<string> GetVideos();
        List<string> GetTime(List<string> Links);
        string GetVideo();
        string GetTekst();
    }
}
