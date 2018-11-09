using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseClient
{
    public static class GenerateQueryWorker
    {
        private static SQLGenerateQuery sqlGenerate = new SQLGenerateQuery();
        private static OracleGenerateQuery oracleGenerate = new OracleGenerateQuery();
        private static IGeneraterQuery curentGenerate;
        public  static void IdentifyCurentGenerate()
        {
            switch (ConnectionSettings.getInstance().CurentTypeDB)
            {
                case TypeDB.SQL:
                    curentGenerate = sqlGenerate;
                    break;
                case TypeDB.Oracle:
                    curentGenerate = oracleGenerate;
                    break;
                default:
                    throw new Exception();
            }
        }
        public static IGeneraterQuery CurentGenerate
        {
            get
            {
                if (curentGenerate == null)
                {
                    IdentifyCurentGenerate();
                }
                return curentGenerate;
            }
        }

    }
}
