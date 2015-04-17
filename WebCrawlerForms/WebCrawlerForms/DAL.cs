using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.Sql;

namespace WebCrawlerForms
{
    public static class DAL
    {
        /// <summary>
        /// Select methode
        /// </summary>
        /// <param name="insert_string">Select query</param>
        public static DataTable Select(Categorie categorie)
        {
            string connectionstring =
            @"provider=microsoft.sqlserver.ce.oledb.4.0;" +
            @"data source=../../Database1.sdf";

            OleDbConnection connection = new OleDbConnection(connectionstring);
            OleDbCommand command = new OleDbCommand();
            OleDbDataAdapter adapter = new OleDbDataAdapter();

            DataTable table = new DataTable();
            try
            {
                int x = Convert.ToInt32(categorie)+1;
                adapter = new OleDbDataAdapter("SELECT * FROM Objecten WHERE Categorie = '"+ x +"' ORDER BY Tijd DESC", connectionstring);
                adapter.Fill(table);
            }
            catch
            { }
            return table;
        }
    }
}
