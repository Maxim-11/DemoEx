using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    public class DataBase
    {
        public static string Users_ID="null", Password="null", App_Name="Testing";
        public static string connectionString = "Data Source=M\\SQLEXPRESS; Initial Catalog = Test; Persist Security Info = true; User ID = {0}; Password = '{1}'";
        public SqlConnection connection = new SqlConnection(connectionString);
        public SqlCommand command = new SqlCommand();
        public DataTable dataTable = new DataTable();
        public SqlDependency dependency = new SqlDependency();
        public enum act { select, manipulation};
        public void sqlExecute(string quety, act act)
        {
            command.Connection = connection;
            command.CommandText = quety;
            command.Notification = null;
            switch (act)
            {
                case act.select:
                    dependency.AddCommandDependency(command);
                    SqlDependency.Start(connectionString);
                    connection.Open();
                    dataTable.Load(command.ExecuteReader());
                    connection.Close();
                    break;
                case act.manipulation:  
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    break;
            }
        }
    }
}
