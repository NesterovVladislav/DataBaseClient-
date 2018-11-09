using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseClient
{

    public class OracleDataBaseWorker : IDataBaseWorker
    {
        private string connectionString
        {
            get
            {
                return $"Data Source={ConnectionSettings.getInstance().HostName}; User ID={ConnectionSettings.getInstance().User};Password={ConnectionSettings.getInstance().Password};";
            }
        }
        public DataTable GetDataFromTable(string sqlExpression)
        {
            try
            {
                DataTable table = new DataTable("table");
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    OracleCommand command = new OracleCommand(sqlExpression, connection);
                    OracleDataAdapter da = new OracleDataAdapter(command);
                    da.Fill(table);
                    da.Dispose();
                }
                return table;
            }
            catch (Exception ex)
            {
                throw new DBWorkerException(ex.Message, ex);
            }
        }

        public void ExecuteQuery(string query)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    OracleCommand command = new OracleCommand(query, connection);
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
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    OracleCommand command = new OracleCommand(sqlExpression, connection);
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
