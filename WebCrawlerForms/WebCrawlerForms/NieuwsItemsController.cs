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
