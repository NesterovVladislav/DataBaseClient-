using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseClient
{
 
    public static class DataBaseWorker
    {
        private static SQLDataBaseWorker sqlDBWorker = new SQLDataBaseWorker();
        private static OracleDataBaseWorker oracleDBWorker = new OracleDataBaseWorker();
        private static IDataBaseWorker curentDBWorker;
        public static void IdentifyCurentGenerate()
        {
            switch (ConnectionSettings.getInstance().CurentTypeDB)
            {
                case TypeDB.SQL:
                    curentDBWorker = sqlDBWorker;
                    break;
                case TypeDB.Oracle:
                    curentDBWorker = oracleDBWorker;
                    break;
                default:
                    throw new Exception();
            }
        }
        public static IDataBaseWorker CurentDBWorker
        {
            get
            {
                if (curentDBWorker == null)
                {
                    IdentifyCurentGenerate();
                }
                return curentDBWorker;
            }
        }

    }
}
