using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.Sql;
using WebcrawlerService;

namespace WebCrawlerForms
{
    public class NieuwsItemsController
    {
        public List<GenericObject> GetNieuwsItems()
        { 
            List<GenericObject> NieuwsItems = new List<GenericObject>();
            DataTable dt = DAL.Select("SELECT * FROM Objecten WHERE Categorie = '1' ORDER BY Datum DESC, Tijd DESC");

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var test = new GenericObject();
                    test.TitelProp = dt.Rows[i][1].ToString();
                    test.BeschrijvingProp = dt.Rows[i][2].ToString();
                    test.LinkProp = dt.Rows[i][3].ToString();
                    test.MediaProp = dt.Rows[i][4].ToString();
                    test.BronProp = dt.Rows[i][5].ToString();
                    test.DatumProp = dt.Rows[i][6].ToString();
                    NieuwsItems.Add(test);
                }
            }

            return NieuwsItems;
        }

    }
}
