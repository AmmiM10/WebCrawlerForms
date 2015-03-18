using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebcrawlerMyNewService
{
    public enum Categorie
    {
        Nieuws,
        Zetels,
        Wetsvoorstellen,
        Agenda
    }

    public class GenericClassObject
    {
        private string titel;
        private string beschrijving;
        private string bron;
        private string media;
        private string link;
        private string datum;
        private Categorie categorie;

        public string TitelProp { get { return titel; } set { titel = value; } }
        public string BeschrijvingProp { get { return beschrijving; } set { beschrijving = value; } }
        public string BronProp { get { return bron; } set { bron = value; } }
        public string MediaProp { get { return media; } set { media = value; } }
        public string LinkProp { get { return link; } set { link = value; } }
        public string DatumProp { get { return datum; } set { datum = value; } }
        public Categorie CategorieProp { get { return categorie; } set { categorie = value; } }
    }
}