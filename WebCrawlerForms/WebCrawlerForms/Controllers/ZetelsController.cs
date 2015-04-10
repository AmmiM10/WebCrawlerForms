using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using WebcrawlerService;

namespace WebCrawlerForms
{
    public class ZetelsController: IGenericObject
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
        private string tijd;
        public string GetTijd { get { return tijd; } set { tijd = value; } }
        private Categorie categorie;
        public Categorie GetCategorie { get { return categorie; } set { categorie = value; } }

        public List<IGenericObject> GetZetelsItems()
        {
            return new Converter().ConvertGenericObjects(Categorie.Zetels);
        }
    }
}
