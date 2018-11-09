using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseClient
{

    public class OracleGenerateQuery : IGeneraterQuery
    {
        public string GetAddColumnsQuery(string nameTable, List<string> columns)
        {
            StringBuilder query = new StringBuilder();
            query.Append($"alter table {nameTable}  add  (");
            columns.ForEach(c => { query.Append($"{c}, "); });
            query.Replace(", ", ") ", query.Length - 2, 2);
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
            query.Append($"alter table {nameTable}  drop (");
            columns.ForEach(c => { query.Append($"{c}, "); });
            query.Replace(", ", ") ", query.Length - 2, 2);

            return query.ToString();
        }

        public string GetDropTableQuery(string nameTable)
        {
            return $"drop table {nameTable}";
        }

        public string GetListSchemeQuery()
        {
            return "select owner from all_tables ao group by ao.owner";
        }

        public string GetListTableQuery(string nameScheme)
        {
            return $"select table_name from all_tables where owner = {nameScheme}";
        }

        public string GetTableDataQuery(string nameTable)
        {
            return $"select * from (select  * from {nameTable}) where rownum < 11";
        }
    }
}
