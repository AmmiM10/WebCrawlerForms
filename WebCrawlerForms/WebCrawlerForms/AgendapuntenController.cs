using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace WebCrawlerForms
{
    public class AgendapuntenController : IGenericObject
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

        public List<IGenericObject> GetAgendapuntenItems()
        {
            List<IGenericObject> AgendapuntenItems = new List<IGenericObject>();
            DataTable dt = DAL.Select("SELECT Titel, Bron, Link, Media, Datum FROM Objecten WHERE Categorie = '4' ORDER BY Datum ASC");

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IGenericObject newObject = new NieuwsItemsController();
                    newObject.GetTitel = dt.Rows[i][0].ToString();
                    newObject.GetBron = dt.Rows[i][1].ToString();
                    newObject.GetLink = dt.Rows[i][2].ToString();
                    newObject.GetMedia = dt.Rows[i][3].ToString();
                    newObject.GetDag = dt.Rows[i][4].ToString();
                    AgendapuntenItems.Add(newObject);
                }
            }

            return AgendapuntenItems;
        }
    }
}
