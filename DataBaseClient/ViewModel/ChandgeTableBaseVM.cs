using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseClient
{
    public class ChandgeTableBaseVM<T>  : BindableBase
    {
        public void ExecuteChandge()
        {
            DataBaseWorker.CurentDBWorker.ExecuteQuery(Table.GenerateSQLQuery());
        }
        protected ChandgeTableBaseModel<T> table;
        public ChandgeTableBaseModel<T> Table
        {
            get { return table; }
        }
    }
}
