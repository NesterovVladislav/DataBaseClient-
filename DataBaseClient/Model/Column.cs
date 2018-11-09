using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseClient
{


    public class Column : ColumnBase
    {
        public Column()
        {
            nameType = TypeColumnDB.GetTypeColumn().FirstOrDefault();
        }

        private string nameType;
        public string NameType
        {
            get { return nameType; }
            set
            {
                if (nameType == null || !nameType.Equals(value))
                {
                    nameType = value;
                    RaisePropertyChanged("NameType");
                    RaisePropertyChanged("TypeWithSize");
                }
            }
        }
        public bool TypeWithSize
        {
            get
            {
                return TypeColumnDB.GetTypeColumnWithSize().Contains(nameType);
            }
        }
        public List<string> TypeColumn => TypeColumnDB.GetTypeColumn();

        public bool isValid
        {
            get
            {
                if ((!String.IsNullOrEmpty(NameColumn)) && ((TypeWithSize && !String.IsNullOrEmpty(SizeType)) || !TypeWithSize))
                {
                    return true;
                }
                return false;
            }
        }
        public string SizeTypeQuery
        {
            get { return TypeWithSize ?  $"( {SizeType} )" : " "; }
        }
        private string sizeType;
        public string SizeType
        {
            get { return sizeType; }
            set
            {
                if (sizeType == null || !sizeType.Equals(value))
                {
                    sizeType = value;
                    RaisePropertyChanged("SizeType");
                }
            }
        }
    }
}
