using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebcrawlerService
{
    public interface IGenericObject
    {
        string titel { get; set; }
        string beschrijving { get; set; }
        string bron { get; set; }
        string media { get; set; }
        string link { get; set; }
        string datum { get; set; }
        Categorie categorie { get; set; }
    }
}
