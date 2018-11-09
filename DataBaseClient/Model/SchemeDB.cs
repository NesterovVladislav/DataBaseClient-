using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseClient
{
    public class SchemeDB : BindableBase
    {
        private readonly ObservableCollection<TableDB> tableInDB= new ObservableCollection<TableDB>();
        //public  ReadOnlyObservableCollection<TableDB> TableInDB;
        public ObservableCollection<TableDB> TableInDB
        {
            get
            {
                return tableInDB;
            }
        }
        public SchemeDB(string name)
        {
            //TableInDB = new ReadOnlyObservableCollection<TableDB>(tableInDB);
            IsVisibleListTable = false;
            IsSelect = false;
            nameScheme = name;
            ChandgeVisibleListTableCommand = new DelegateCommand<object>(ChandgeVisibleListTable);

        }
        public void UpdateListTableAfterModify()
        {
            tableInDB.Clear();
            UpdateLisrTable();
            chandgeTable();
        }

        public void UpdateTableAfterChandgeColumns()
        {
            SelectedTableDB.LoadData();
            chandgeTable();
        }

        public void UpdateLisrTable()
        {
            try
            {
                chandgeScheme(NameScheme);

                if (tableInDB.Count() == 0)
                {
                    List<string> tableDB = DataBaseWorker.CurentDBWorker.GetListDataFromQuery(GenerateQueryWorker.CurentGenerate.GetListTableQuery(NameScheme));
                    tableDB.ForEach(t => {
                        var newTable = new TableDB(t, nameScheme);
                        tableInDB.Add(newTable);
                    });
                }
            }
            catch (DBWorkerException e)
            {

                ShowMessageError.ShowError($"При загрузке списка таблиц произошла ошибка \n {e.Message}");
            }

        }
        public delegate void ChandgeSeceltedScheme(string nameScheme);
        public event ChandgeSeceltedScheme chandgeScheme;

        public delegate void ChandgeSeceltedTable();
        public event ChandgeSeceltedTable chandgeTable;

        private TableDB selectedTableDB;
        public TableDB SelectedTableDB
        {
            get { return selectedTableDB; }
            set
            {
                if (selectedTableDB == null || !selectedTableDB.Equals(value))
                {
                    chandgeScheme(NameScheme);
                    selectedTableDB = value;
                    selectedTableDB.LoadData();
                    chandgeTable();
                    RaisePropertyChanged("SelectedTableDB");
                }
            }
        }
        public bool IsVisibleListTable { get; set; }
        public bool IsSelect { get; set; }
        public void UpdateIsSelect()
        {
            IsSelect = !IsSelect;
            if (!IsSelect)
            {
                selectedTableDB = null;
                RaisePropertyChanged("SelectedTableDB");

            }
            RaisePropertyChanged("IsSelect");
        }
        public void UpdateIsVisibleListTable()
        {
            IsVisibleListTable = !IsVisibleListTable;
            RaisePropertyChanged("IsVisibleListTable");
        }
        public DelegateCommand<object> ChandgeVisibleListTableCommand { get; }
        private void ChandgeVisibleListTable(object obj)
        {
            UpdateLisrTable();
            UpdateIsVisibleListTable();
        }
        private string nameScheme;
        public string NameScheme
        {
            get { return nameScheme; }
            set
            {
                if (nameScheme == null || !nameScheme.Equals(value))
                {
                    nameScheme = value;
                    RaisePropertyChanged("NameScheme");
                }

            }
        }
    }
}
