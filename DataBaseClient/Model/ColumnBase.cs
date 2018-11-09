using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseClient
{
    public class ColumnBase : BindableBase
    {
        private string nameColumn;
        public string NameColumn
        {
            get { return nameColumn; }
            set
            {
                if (nameColumn == null || !nameColumn.Equals(value))
                {
                    nameColumn = value;
                    RaisePropertyChanged("NameColumn");
                }
            }
        }
    }
}
