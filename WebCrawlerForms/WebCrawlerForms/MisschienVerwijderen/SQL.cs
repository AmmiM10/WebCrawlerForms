using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.Sql;

namespace WebCrawlerForms
{
    public static class SQL
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
        /// Select methode
        /// </summary>
        /// <param name="insert_string">Select query</param>
        public static DataTable SelectAgendapunten(DateTime dt)
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
                adapter = new OleDbDataAdapter("", connectionstring);
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
        public static void Insert(string insert_string)
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
