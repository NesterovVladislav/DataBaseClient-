using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseClient
{
    public static class TypeColumnDB
    {
        private static List<string> TypeColumnSQL = new List<string>() { "bit", "tinyint", "smallint", "int", "bigint", "decimal", "varchar", "nvarchar", "date", "datetime", "datetime2", "binary" };
        private static List<string> TypeColumnSQLWithSize = new List<string>() { "decimal", "varchar", "nvarchar", "binary" };


        private static List<string> TypeColumnOracle = new List<string>() { "char", "nchar", "nvarchar2", "varchar2", "long", "raw", "number", "decimal", "numeric", "PLS_INTEGER", "date", "BOOLEAN" };
        private static List<string> TypeColumnOracleWithSize = new List<string>() { "char", "nchar", "nvarchar2", "varchar2", "number", "decimal", "numeric" };

        public static List<String> GetTypeColumn()
        {
            switch (ConnectionSettings.getInstance().CurentTypeDB)
            {
                case TypeDB.SQL:
                    return TypeColumnSQL;
                case TypeDB.Oracle:
                    return TypeColumnOracle;
                default:
                    return new List<string>();
            }
        }

        public static List<String> GetTypeColumnWithSize()
        {
            switch (ConnectionSettings.getInstance().CurentTypeDB)
            {
                case TypeDB.SQL:
                    return TypeColumnSQLWithSize;
                case TypeDB.Oracle:
                    return TypeColumnOracleWithSize;
                default:
                    return new List<string>();
            }
        }
    }
}
