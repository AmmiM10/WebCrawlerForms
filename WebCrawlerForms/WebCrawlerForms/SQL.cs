using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.Sql;

namespace WebCrawlerForms
{
    public class SQL
    {
        private OleDbConnection connection;
        private OleDbCommand command;
        private OleDbDataAdapter adapter;
        private string connectionstring =
            @"provider=microsoft.sqlserver.ce.oledb.4.0;" +
            @"data source=../../Database1.sdf";

        /// <summary>
        /// Select methode
        /// </summary>
        /// <param name="insert_string">Select query</param>
        public DataTable Select(string select_string)
        {
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
        public void Insert(string insert_string)
        {
            connection = new OleDbConnection(connectionstring);
            command = new OleDbCommand();
            adapter = new OleDbDataAdapter();
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
        public void Update(string update_string)
        {
            connection = new OleDbConnection(connectionstring);
            command = new OleDbCommand();
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
