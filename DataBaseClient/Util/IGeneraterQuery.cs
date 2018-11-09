using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseClient
{
    public interface IGeneraterQuery
    {
        string GetCreateTableQuery(string nameTable, List<string> columns);
        string GetAddColumnsQuery(string nameTable, List<string> columns);
        string GetDeleteColumnsQuery(string nameTable, List<string> columns);
        string GetDropTableQuery(string nameTable);
        string GetListTableQuery(string nameScheme);
        string GetTableDataQuery(string nameTable);
        string GetListSchemeQuery();
    }
}
