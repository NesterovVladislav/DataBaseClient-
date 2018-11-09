using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseClient
{
    public enum TypeDB
    {
        SQL = 0,
        Oracle =1
    }
    public class ConnectionSettings : BindableBase
    {
        private static ConnectionSettings instance;

        private ConnectionSettings()
        {
            isConnect = false;
            curentTypeDB = TypeDB.SQL;
        }

        public static ConnectionSettings getInstance()
        {
            if (instance == null)
                instance = new ConnectionSettings();
            return instance;
        }

        private string hostName;
        public string HostName
        {
            get { return hostName; }
            set
            {
                if (hostName==null || !hostName.Equals(value))
                {
                    hostName = value;
                    RaisePropertyChanged("HostName");
                    RaisePropertyChanged("ValidConnect");
                }
            }
        }
        private string user;
        public string User
        {
            get { return user; }
            set
            {
                if (user==null ||!user.Equals(value))
                {
                    user = value;
                    RaisePropertyChanged("User");
                    RaisePropertyChanged("ValidConnect");
                }
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                if (password == null || !password.Equals(value))
                {
                    password = value;
                    RaisePropertyChanged("Password");
                    RaisePropertyChanged("ValidConnect");
                }
            }
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
                }
            }
        }
        private TypeDB curentTypeDB;
        public TypeDB CurentTypeDB
        {
            get { return curentTypeDB; }
            set
            {
                if (curentTypeDB!= value)
                {
                    curentTypeDB = value;
                    GenerateQueryWorker.IdentifyCurentGenerate();
                    DataBaseWorker.IdentifyCurentGenerate();
                    RaisePropertyChanged("CurentTypeDB");
                    RaisePropertyChanged("ValidConnect");
                }
            }
        }
        private string nameDB;
        public string NameDB
        {
            get { return nameDB; }
            set
            {
                if (nameDB == null || !nameDB.Equals(value))
                {
                    nameDB = value;
                    RaisePropertyChanged("NameDB");
                    RaisePropertyChanged("ValidConnect");
                }
            }
        }
        public bool ValidConnect
        {
            get
            {
                return !IsConnect && (!String.IsNullOrEmpty(NameDB) || (CurentTypeDB == TypeDB.Oracle)) && (!String.IsNullOrEmpty(Password)) && 
                    (!String.IsNullOrEmpty(User)) && (!String.IsNullOrEmpty(HostName));
            }
        }
        private bool isConnect;
        public bool IsConnect
        {
            get { return isConnect; }
            set
            {
                if (isConnect != value)
                {
                    isConnect = value;
                    RaisePropertyChanged("IsConnect");
                    RaisePropertyChanged("ValidConnect");
                }
            }
        }
    }
}
