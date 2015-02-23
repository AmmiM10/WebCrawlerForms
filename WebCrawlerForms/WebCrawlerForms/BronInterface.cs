using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebCrawlerForms
{
    public interface BronInterface
    {
        string PropLink { get; set; }
        List<string> GetHeadlines();
        List<string> GetHeadlineLinks();
        List<string> GetVideos();
        string GetVideo();
        string GetTekst();
    }
}
