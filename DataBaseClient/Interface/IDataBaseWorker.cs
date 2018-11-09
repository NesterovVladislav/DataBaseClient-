using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseClient
{
    public interface IDataBaseWorker
    {
        List<string> GetListDataFromQuery(string sqlExpression);
        void ExecuteQuery(string query);
        DataTable GetDataFromTable(string sqlExpression);
    }
}
