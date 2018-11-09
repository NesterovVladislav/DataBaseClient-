using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseClient
{

    public class ChandgeTableDeleteColumnsModel : ChandgeTableBaseModel<ColumnForDelete>
    {

        public ChandgeTableDeleteColumnsModel(List<string> listColumns, string name = "") : base(name)
        {
            listColumns.ForEach(c => { AddColumn(new ColumnForDelete(c)); });
        }
        public override string GenerateSQLQuery()
        {
            return GenerateQueryWorker.CurentGenerate.GetDeleteColumnsQuery($"{ConnectionSettings.getInstance().NameScheme}.{NameTable}", 
                                    ColumnInTable.ToList().FindAll(c => c.IsSelect).Select(c => c.NameColumn).ToList());
        }
        public override  bool IsValid()
        {

                return ColumnInTable.Count(c => c.IsSelect) > 0 ? true : false;
        }
        private ColumnForDelete selectedColumnInTable;
        public ColumnForDelete SelectedColumnInTable
        {
            get { return selectedColumnInTable; }
            set
            {
                if (selectedColumnInTable == null || !selectedColumnInTable.Equals(value))
                {
                    selectedColumnInTable = value;
                    RaisePropertyChanged("SelectedColumnInTable");
                    RaisePropertyChanged("IsValid");
                }
            }
        }
    }
}
