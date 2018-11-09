using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseClient
{

    public class ChandgeTableVM : ChandgeTableBaseVM<Column>
    {
 
        public ChandgeTableVM(bool isNew = false, string name = "")
        {
            table = new ChandgeTableModel(isNew, name);
            // SchemeCollection = new ReadOnlyObservableCollection<SchemeDB>(schemeCollection);
            AddCommand = new DelegateCommand<object>(obj => {
                (Table as ChandgeTableModel)?.AddValue();
                RaisePropertyChanged("ListColumnNotEmpty");
            });
            RemoveCommand = new DelegateCommand<int?>(i => {
                if (i.HasValue) (Table as ChandgeTableModel)?.RemoveValue(i.Value);
                RaisePropertyChanged("ListColumnNotEmpty");
            });

        }

        public DelegateCommand<object> AddCommand { get; }
        public DelegateCommand<int?> RemoveCommand { get; }
        public bool ListColumnNotEmpty => Table?.ColumnInTable?.Count > 0;

    }
}
