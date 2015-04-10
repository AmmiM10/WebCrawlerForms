using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace WebCrawlerForms
{
    public class Converter
    {
        public List<IGenericObject> ConvertGenericObjects(Categorie Object)
        {
            List<IGenericObject> items = new List<IGenericObject>();
            DataTable dt = DAL.Select(Object);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IGenericObject newObject = new NieuwsItemsController();
                    //newObject.GetCategorie = dt.Rows[i][1].ToString();
                    newObject.GetTitel = dt.Rows[i][1].ToString();
                    newObject.GetBeschrijving = dt.Rows[i][2].ToString();
                    newObject.GetLink = dt.Rows[i][3].ToString();
                    newObject.GetMedia = dt.Rows[i][4].ToString();
                    newObject.GetBron = dt.Rows[i][5].ToString();
                    newObject.GetTijd = dt.Rows[i][8].ToString();
                    items.Add(newObject);
                }
            }

            return items;
        }
    }
}
