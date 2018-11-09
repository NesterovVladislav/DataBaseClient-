using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseClient
{

    public class ChandgeTableDeleteColumnsVM : ChandgeTableBaseVM<ColumnForDelete>
    {

        public ChandgeTableDeleteColumnsVM(List<string> listColumns, string name = "")
        {
            table = new ChandgeTableDeleteColumnsModel(listColumns, name);
        }

    }
}
