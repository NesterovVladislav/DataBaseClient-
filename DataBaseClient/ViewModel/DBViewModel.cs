using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseClient
{
    public  class DBViewModel : BindableBase
    {
        private readonly ObservableCollection<SchemeDB> schemeCollection = new ObservableCollection<SchemeDB>();
        // public readonly ReadOnlyObservableCollection<SchemeDB> SchemeCollection;
        public  ObservableCollection<SchemeDB> SchemeCollection
        {
            get
            {
                return schemeCollection;
            }
        }
        public DBViewModel()
        {
           // SchemeCollection = new ReadOnlyObservableCollection<SchemeDB>(schemeCollection);
            ConnectDBCommand = new DelegateCommand<object>(ConnectDB);
            NewConnectDBCommand = new DelegateCommand<object>(NewConnectDB);
            AddNewTableCommand = new DelegateCommand<object>(AddNewTable);
            AddNewColumnsCommand = new DelegateCommand<object>(AddNewColumns);
            DropTableCommand = new DelegateCommand<object>(DropTable);
            DeleteColumnsCommand = new DelegateCommand<object>(DeleteColumns);

            ConnectionSettings.getInstance().HostName = "AmonDev01";
            ConnectionSettings.getInstance().Password = "saas";
            ConnectionSettings.getInstance().User = "sa";
            ConnectionSettings.getInstance().NameDB = "ipc";
        }
        public DelegateCommand<object> ConnectDBCommand { get; }
        public DelegateCommand<object> NewConnectDBCommand { get; }
        public DelegateCommand<object> AddNewTableCommand { get; }
        public DelegateCommand<object> AddNewColumnsCommand { get; }
        public DelegateCommand<object> DropTableCommand { get; }
        public DelegateCommand<object> DeleteColumnsCommand { get; }

        private void DeleteColumns(object obj)
        {
            try
            {
                ChandgeTableDeleteColumnsVM chandgeTableVM = new ChandgeTableDeleteColumnsVM(GetColumnsNameForCurentTable(), CurentTableName);

                ChandgeTableDeleteColumnsView chandgeTableDialog = new ChandgeTableDeleteColumnsView()
                {
                    DataContext = chandgeTableVM,
                };
                if (chandgeTableDialog.ShowDialog() == true)
                {
                    chandgeTableVM.ExecuteChandge();
                    selectedSheme.UpdateTableAfterChandgeColumns();
                }
            }
            catch (DBWorkerException e)
            {

                ShowMessageError.ShowError($"При удалении колонок произошла ошибка \n {e.Message}");
            }

        }
        private void DropTable(object obj)
        {
            try
            {
                DataBaseWorker.CurentDBWorker.ExecuteQuery(GenerateQueryWorker.CurentGenerate.GetDropTableQuery($"{CurentSchemeName}.{CurentTableName}"));
                selectedSheme.UpdateListTableAfterModify();
            }
            catch (DBWorkerException e)
            {

                ShowMessageError.ShowError($"При удалении таблицы произошла ошибка \n {e.Message}");
            }


        }
        private void NewConnectDB(object obj)
        {
            schemeCollection.Clear();
            ConnectionSettings.getInstance().IsConnect = false;
            selectedSheme = null;
            UpdateInformationFields();
        }
        private void UpdateInformationFields()
        {
            RaisePropertyChanged("CurentTable");
            RaisePropertyChanged("CurentTableName");
            RaisePropertyChanged("CurentSchemeName");

        }
        private void AddNewColumns(object obj)
        {
            try
            {
                ChandgeTableVM chandgeTableVM = new ChandgeTableVM(false, CurentTableName);

                ChandgeTableView chandgeTableDialog = new ChandgeTableView()
                {
                    DataContext = chandgeTableVM,
                };
                if (chandgeTableDialog.ShowDialog() == true)
                {
                    chandgeTableVM.ExecuteChandge();
                    selectedSheme.UpdateTableAfterChandgeColumns();
                }
            }
            catch (DBWorkerException e)
            {

                ShowMessageError.ShowError($"При добавление колонок произошла ошибка \n {e.Message}");
            }

        }
        private void AddNewTable(object obj)
        {
            try
            {
                ChandgeTableVM chandgeTableVM = new ChandgeTableVM(true);

                ChandgeTableView chandgeTableDialog = new ChandgeTableView()
                {
                    DataContext = chandgeTableVM,
                };
                if (chandgeTableDialog.ShowDialog() == true)
                {
                    chandgeTableVM.ExecuteChandge();
                    selectedSheme.UpdateListTableAfterModify();
                }

            }
            catch (DBWorkerException e)
            {

                ShowMessageError.ShowError($"При создании таблицы произошла ошибка \n {e.Message}");
            }

        }
        private void ConnectDB(object obj)
        {
            try
            {
                schemeCollection.Clear();
                List<string> schemeDB = DataBaseWorker.CurentDBWorker.GetListDataFromQuery(GenerateQueryWorker.CurentGenerate.GetListSchemeQuery());
                schemeDB.ForEach(s => {
                    var newScheme = new SchemeDB(s);
                    newScheme.chandgeScheme += ChandgeScheme;
                    newScheme.chandgeTable += () => { UpdateInformationFields(); };
                    schemeCollection.Add(newScheme);
                });
                ConnectionSettings.getInstance().IsConnect = true;
            }
            catch (DBWorkerException e)
            {

                ShowMessageError.ShowError($"При установлении соединения с Базой Данных произошла ошибка \n {e.Message}");
            }

        }
        private List<string> GetColumnsNameForCurentTable()
        {
            List<string> columns = new List<string>();
            var columsTable = selectedSheme?.SelectedTableDB?.Table.Columns;
            for (int i = 0; i < columsTable.Count; i++)
            {
                var col = columsTable[i];
                columns.Add(col.ColumnName);
            }
            
            return columns;
        }
        public ConnectionSettings SettingsConnectDB
        {
            get { return ConnectionSettings.getInstance(); }
        }
        private void ChandgeFlagSelect()
        {
            if (selectedSheme != null)
            {
                selectedSheme.UpdateIsSelect();
            }
        }
        private void ChandgeScheme(string nameScheme)
        {
            if (selectedSheme == null || selectedSheme.NameScheme != nameScheme )
            {
                ChandgeFlagSelect();
                selectedSheme = schemeCollection.FirstOrDefault(s => s.NameScheme == nameScheme);
                ConnectionSettings.getInstance().NameScheme = nameScheme;
                ChandgeFlagSelect();
                UpdateInformationFields();
            }
        }
        public DataView CurentTable
        {
            get { return selectedSheme?.SelectedTableDB?.Table.DefaultView; }
        }
        public string CurentTableName
        {
            get { return selectedSheme?.SelectedTableDB?.NameTable; }
        }
        public string CurentSchemeName
        {
            get { return selectedSheme?.NameScheme; }
        }
        private SchemeDB selectedSheme;
        public SchemeDB SelectedSheme
        {
            get { return selectedSheme; }
            set
            {
                if (selectedSheme == null || !selectedSheme.Equals(value))
                {
                   // selectedSheme = value;
                  //  selectedSheme.UpdateLisrTable();
                   // RaisePropertyChanged("selectedSheme");
                }
            }
        }
    }
}
