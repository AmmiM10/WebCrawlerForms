using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebcrawlerService
{
    public abstract class GenericObject: IGenericObject
    {
        private string titel;
        public string GetTitel { get { return titel; } set { titel = value; } }

        private string beschrijving;
        public string GetBeschrijving { get { return beschrijving; } set { beschrijving = value; } }

        private string bron;
        public string GetBron { get { return bron; } set { bron = value; } }

        private string media;
        public string GetMedia { get { return media; } set { media = value; } }

        private string link;
        public string GetLink { get { return link; } set { link = value; } }

        private string dag;
        public string GetDag { get { return dag; } set { dag = value; } }

        private DateTime datum;
        public DateTime GetTijd { get { return datum; } set { datum = value; } }

        private Categorie categorie;
        public Categorie GetCategorie { get { return categorie; } set { categorie = value; } }
    }
}