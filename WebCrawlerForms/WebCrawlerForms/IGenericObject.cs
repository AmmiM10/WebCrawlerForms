using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebCrawlerForms
{
    public enum Categorie
    {
        Nieuws, Zetels, Wetsvoorstellen, Agendapunten
    }

    public interface IGenericObject
    {
        string GetTitel { get; set; }
        Categorie GetCategorie { get; set; }
        string GetBeschrijving { get; set; }
        string GetBron { get; set; }
        string GetMedia { get; set; }
        string GetLink { get; set; }
        string GetDag { get; set; }
        DateTime GetTijd { get; set; }
    }
}
