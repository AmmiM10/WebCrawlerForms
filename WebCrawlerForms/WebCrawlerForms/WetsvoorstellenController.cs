using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace WebCrawlerForms
{
    public class WetsvoorstellenController : IGenericObject
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

        public List<IGenericObject> GetWetsvoorstellenItems()
        {
            List<IGenericObject> WetsvoorstellenItems = new List<IGenericObject>();
            DataTable dt = DAL.Select("SELECT Titel, Bron, Link FROM Objecten WHERE Categorie = '3' ORDER BY Datum DESC, Tijd DESC");

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IGenericObject newObject = new NieuwsItemsController();
                    newObject.GetTitel = dt.Rows[i][0].ToString();
                    newObject.GetBron = dt.Rows[i][1].ToString();
                    newObject.GetLink = dt.Rows[i][2].ToString();
                    WetsvoorstellenItems.Add(newObject);
                }
            }

            return WetsvoorstellenItems;
        }
    }
}
