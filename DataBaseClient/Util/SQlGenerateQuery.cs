using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseClient
{
    public class SQLGenerateQuery : IGeneraterQuery
    {
        public string GetAddColumnsQuery(string nameTable, List<string> columns)
        {
            StringBuilder query = new StringBuilder();
            query.Append($"alter table {nameTable}  add  ");
            columns.ForEach(c => { query.Append($"{c}, "); });
            query.Replace(", ", " ", query.Length - 2, 2);
            return query.ToString();
        }

        public string GetCreateTableQuery(string nameTable, List<string> columns)
        {
            StringBuilder query = new StringBuilder();
            query.Append($"create table {nameTable} ( ");
            columns.ForEach(c => { query.Append($"{c}, "); });
            query.Replace(", ", ") ", query.Length - 2, 2);
            return query.ToString();
        }

        public string GetDeleteColumnsQuery(string nameTable, List<string> columns)
        {
            StringBuilder query = new StringBuilder();
            query.Append($"alter table {nameTable}  drop column  ");
            columns.ForEach(c => { query.Append($"{c}, "); });
            query.Replace(", ", " ", query.Length - 2, 2);

            return query.ToString();
        }

        public string GetDropTableQuery(string nameTable)
        {
            return $"drop table {nameTable}";
        }

        public string GetListSchemeQuery()
        {
            return "select TABLE_SCHEMA from information_schema.tables group by TABLE_SCHEMA"; 
        }

        public string GetListTableQuery(string nameScheme)
        {
            return $"select TABLE_NAME from information_schema.tables where TABLE_SCHEMA = '{nameScheme}' and TABLE_TYPE = 'BASE TABLE'";
        }

        public string GetTableDataQuery(string nameTable)
        {
            return $"select top (10) * from {nameTable}";
        }
    }
}
