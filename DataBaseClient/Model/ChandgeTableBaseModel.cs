using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseClient
{
    public abstract class ChandgeTableBaseModel<T> : BindableBase
    {
        private ObservableCollection<T> columnInTable;
        //public  ReadOnlyObservableCollection<TableDB> TableInDB;
        public ObservableCollection<T> ColumnInTable
        {
            get
            {
                return columnInTable;
            }
        }
        public ChandgeTableBaseModel( string name)
        {
            columnInTable = new ObservableCollection<T>();
            nameTable = name;
        }
        public abstract string GenerateSQLQuery();
        public void AddColumn(T col)
        {
            columnInTable.Add(col);
        }
        public void RemoveColumn(int index)
        {
            columnInTable.RemoveAt(index);
        }
        public abstract bool IsValid();
        private string nameTable;
        public string NameTable
        {
            get { return nameTable; }
            set
            {
                if (nameTable == null || !nameTable.Equals(value))
                {
                    nameTable = value;
                    RaisePropertyChanged("NameTable");
                }

            }
        }
    }
}
