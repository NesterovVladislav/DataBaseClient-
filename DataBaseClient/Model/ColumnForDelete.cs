using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseClient
{

    public class ColumnForDelete : ColumnBase
    {
        public ColumnForDelete(string name)
        {
            NameColumn = name;
            isSelect = false;
        }

        private bool isSelect;
        public bool IsSelect
        {
            get { return isSelect; }
            set
            {
                if (isSelect != value)
                {
                    isSelect = value;
                    RaisePropertyChanged("IsSelect");
                }
            }
        }
    }
}
