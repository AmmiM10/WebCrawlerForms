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

        public DataTable Select(string select_string) // Select Methode
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

        public void Insert(string insert_string) // Insert Methode
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

        public void Update(string update_string)// Update Methode
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
