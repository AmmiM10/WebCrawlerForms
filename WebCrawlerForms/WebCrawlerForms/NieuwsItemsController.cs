using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.Sql;

namespace WebCrawlerForms
{
    public class NieuwsItemsController: IGenericObject
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

        public List<IGenericObject> GetNieuwsItems()
        { 
            List<IGenericObject> NieuwsItems = new List<IGenericObject>();
            DataTable dt = DAL.Select("SELECT * FROM Objecten WHERE Categorie = '1' ORDER BY Datum DESC, Tijd DESC");

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IGenericObject newObject = new NieuwsItemsController();
                    newObject.GetTitel = dt.Rows[i][1].ToString();
                    newObject.GetBeschrijving = dt.Rows[i][2].ToString();
                    newObject.GetLink = dt.Rows[i][3].ToString();
                    newObject.GetMedia = dt.Rows[i][4].ToString();
                    newObject.GetBron = dt.Rows[i][5].ToString();
                    newObject.GetDag = dt.Rows[i][6].ToString();
                    NieuwsItems.Add(newObject);
                }
            }

            return NieuwsItems;
        }
    }
}
