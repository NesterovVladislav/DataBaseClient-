using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseClient
{
    public class SQLDataBaseWorker : IDataBaseWorker
    {
        private string connectionString
        {
            get
            {
                return $"Data Source={ConnectionSettings.getInstance().HostName};Initial Catalog={ConnectionSettings.getInstance().NameDB};Integrated Security=false; User ID={ConnectionSettings.getInstance().User};Password={ConnectionSettings.getInstance().Password};";
            }
        }
        public DataTable GetDataFromTable(string sqlExpression)
        {
            try
            {
                DataTable table = new DataTable("table");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(table);
                    da.Dispose();
                }
                return table;
            }
            catch (Exception ex)
            {

                throw new DBWorkerException(ex.Message,ex);
            }
        }

        public void ExecuteQuery(string query)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    int res = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new DBWorkerException(ex.Message, ex);
            }
        }

        public List<string> GetListDataFromQuery(string sqlExpression)
        {
            try
            {
                List<string> listDB = new List<string>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    var result = command.ExecuteReader();

                    while (result.Read())
                    {
                        listDB.Add(result.GetString(0).ToString());
                    }
                }
                return listDB;
            }
            catch (Exception ex)
            {
                throw new DBWorkerException(ex.Message, ex);
            }
        }

    }
}
