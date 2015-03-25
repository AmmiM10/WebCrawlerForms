using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebcrawlerService
{
    public static class DAL
    {
        /// <summary>
        /// Select methode
        /// </summary>
        /// <param name="insert_string">Select query</param>
        public static DataTable Select(string select_string)
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
                adapter = new OleDbDataAdapter(select_string, connectionstring);
                adapter.Fill(table);
            }
            catch
            { }
            return table;
        }

        /// <summary>
        /// Insert methode
        /// </summary>
        /// <param name="insert_string">Insert query</param>
        public static void Insert(IGenericObject classObject)
        {
            string connectionstring =
            @"provider=microsoft.sqlserver.ce.oledb.4.0;" +
            @"data source=../../Database1.sdf";

            OleDbConnection connection = new OleDbConnection(connectionstring);
            OleDbCommand command = new OleDbCommand();
            OleDbDataAdapter adapter = new OleDbDataAdapter();

            DataTable dt = Select("SELECT Id FROM Categorieen WHERE Naam = '" + classObject.GetCategorie + "'");

            string insert_string = ("INSERT INTO Objecten (Categorie, Titel, Beschrijving, Link, Media, Bron, Datum, Tijd) VALUES ('" + Convert.ToInt32(dt.Rows[0][0]) + "', '" + classObject.GetTitel.Replace("'", "`").Replace('"', '`') + "', '" + classObject.GetBeschrijving.Replace("'", "`") + "', '" + classObject.GetLink + "', '" + classObject.GetMedia + "', '" + classObject.GetBron + "', '" + classObject.GetDag + "', '"+ classObject.GetTijd +"')");

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = insert_string;
                adapter.InsertCommand = command;
                adapter.InsertCommand.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Update methode
        /// </summary>
        /// <param name="update_string">Update query</param>
        public static void Update(string update_string)
        {
            string connectionstring =
            @"provider=microsoft.sqlserver.ce.oledb.4.0;" +
            @"data source=../../Database1.sdf";

            OleDbConnection connection = new OleDbConnection(connectionstring);
            OleDbCommand command = new OleDbCommand();
            OleDbDataAdapter adapter = new OleDbDataAdapter();



            try
            {
                connection.Open();
                adapter = new OleDbDataAdapter();
                command.Connection = connection;
                command.CommandText = update_string;
                adapter.UpdateCommand = command;
                adapter.UpdateCommand.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }
    }
}