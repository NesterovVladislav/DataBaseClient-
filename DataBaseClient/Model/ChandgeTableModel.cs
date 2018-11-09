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

    public class ChandgeTableModel : ChandgeTableBaseModel<Column>
    {

        public ChandgeTableModel(bool isNew = false, string name = "") : base(name)
        {
            //TableInDB = new ReadOnlyObservableCollection<TableDB>(tableInDB);
            IsNew = isNew;
        }
        public void AddValue()
        {
            AddColumn(new Column());
            SelectedColumnInTable = ColumnInTable.LastOrDefault();
        }
        //проверка на валидность, удаление из коллекции и уведомление об изменении суммы
        public void RemoveValue(int index)
        {
            //проверка на валидность удаления из коллекции - обязанность модели
            if (index >= 0 && index < ColumnInTable.Count)
            {
                RemoveColumn(index);
                selectedColumnInTable = null;
                RaisePropertyChanged("EnableDelete");
            }

        }
        public override string GenerateSQLQuery()
        {
            StringBuilder query = new StringBuilder();
            if (IsNew)
            {
                query.Append($"create table {ConnectionSettings.getInstance().NameScheme}.{NameTable} ( ");
            }
            else
            {
                query.Append($"alter table {ConnectionSettings.getInstance().NameScheme}.{NameTable}  add  ");
            }
            foreach (var col in ColumnInTable)
            {
                query.Append($"{col.NameColumn} {col.NameType} {col.SizeTypeQuery}, ");
            }
            if (IsNew)
            {
                query.Replace(", ", ") ", query.Length - 2, 2);
            }
            else
            {
                query.Replace(", ", " ", query.Length - 2, 2);
            }

            return IsNew ? GenerateQueryWorker.CurentGenerate.GetCreateTableQuery($"{ConnectionSettings.getInstance().NameScheme}.{NameTable}",
                                ColumnInTable.ToList().Select(col => $"{col.NameColumn} {col.NameType} {col.SizeTypeQuery}").ToList()) :
                                GenerateQueryWorker.CurentGenerate.GetAddColumnsQuery($"{ConnectionSettings.getInstance().NameScheme}.{NameTable}",
                                ColumnInTable.ToList().Select(col => $"{col.NameColumn} {col.NameType} {col.SizeTypeQuery}").ToList());
        }
        public override  bool IsValid()
        {

                if (!String.IsNullOrEmpty(NameTable) && (ColumnInTable.Count(c => c.isValid)== ColumnInTable.Count))
                {
                    return true;
                }
                return false;
        }
        public bool EnableDelete => selectedColumnInTable != null;
        private Column selectedColumnInTable;
        public Column SelectedColumnInTable
        {
            get { return selectedColumnInTable; }
            set
            {
                if (selectedColumnInTable == null || !selectedColumnInTable.Equals(value))
                {
                    selectedColumnInTable = value;
                    RaisePropertyChanged("SelectedColumnInTable");
                    RaisePropertyChanged("EnableDelete");
                }
            }
        }
        public bool IsNew { get; set; }


    }
}
