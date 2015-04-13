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
            if (!CheckIfAlreadyExist(classObject.GetTitel))
            {
                string connectionstring =
                @"provider=microsoft.sqlserver.ce.oledb.4.0;" +
                @"data source=../../Database1.sdf";

                OleDbConnection connection = new OleDbConnection(connectionstring);
                OleDbCommand command = new OleDbCommand();
                OleDbDataAdapter adapter = new OleDbDataAdapter();

                DataTable dt = Select("SELECT Id FROM Categorieen WHERE Naam = '" + classObject.GetCategorie + "'");

                string insert_string = ("INSERT INTO Objecten (Categorie, Titel, Beschrijving, Link, Bron, Media) VALUES ('" + Convert.ToInt32(dt.Rows[0][0]) + "', '" + classObject.GetTitel.Replace("'", "`").Replace('"', '`') + "', '" + classObject.GetBeschrijving.Replace("'", "`") + "', '" + classObject.GetLink + "', '" + classObject.GetBron + "', '"+ classObject.GetMedia +"')");

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
        }

        /// <summary>
        /// Delete methode
        /// </summary>
        /// <param name="insert_string">Duur</param>
        public static void Delete(DateTime duur)
        {
            string connectionstring =
            @"provider=microsoft.sqlserver.ce.oledb.4.0;" +
            @"data source=../../Database1.sdf";

            OleDbConnection connection = new OleDbConnection(connectionstring);
            OleDbCommand command = new OleDbCommand();
            OleDbDataAdapter adapter = new OleDbDataAdapter();

            string delete_string = string.Format("DELETE FROM Objecten WHERE Tijd < '{0}'", duur);

            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = delete_string;
                adapter.DeleteCommand = command;
                adapter.DeleteCommand.ExecuteNonQuery();
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

        public static bool CheckIfAlreadyExist(string text)
        {
            bool BestaatAl = false;

            DataTable dt = Select("SELECT * FROM Objecten WHERE Categorie = '1' AND Titel="+ text +"");
            if (dt.Rows.Count > 0)
                BestaatAl = true;

            return BestaatAl;
        }
    }
}