using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DataBaseClient
{
    public class TableDB : BindableBase
    {
        public TableDB(string name,string nameScheme)
        {
            this.nameScheme = nameScheme;
            nameTable = name;
        }
        private string nameScheme;
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
        public void LoadData()
        {
            try
            {
                table = DataBaseWorker.CurentDBWorker.GetDataFromTable(GenerateQueryWorker.CurentGenerate.GetTableDataQuery($"{ConnectionSettings.getInstance().NameScheme}.{nameTable}"));
            }
            catch (DBWorkerException e)
            {

                ShowMessageError.ShowError($"При загрузке таблицы произошла ошибка \n {e.Message}");
            }
        }
        private DataTable table;
        public DataTable Table
        {
            get { return table; }
        }
    }
}
