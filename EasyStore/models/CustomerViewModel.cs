using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using EasyStore.Annotations;
using EasyStore.Entities;

namespace EasyStore.models
{
    public class CustomerViewModel: IDataErrorInfo, INotifyPropertyChanged
    {
      
      

        public Int64 Id { get; set; }
        private string _firstName;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value; 
                OnPropertyChanged("_firstName");
            }
        }

        private string _lastName;
        private string _tel;
        private string _email;
        private string _city;
        private string _street1;

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value; 
                OnPropertyChanged("_lastName");
            }
        }


        public string Tel
        {
            get { return _tel; }
            set
            {
                if (value == _tel) return;
                _tel = value;
                OnPropertyChanged(_tel);
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                if (value == _email) return;
                _email = value;
                OnPropertyChanged(_email);
            }
        }

        public Store Store { get; set; }

        public string City
        {
            get { return _city; }
            set
            {
                if (value == _city) return;
                _city = value;
                OnPropertyChanged(_city);
            }
        }

        public string Street1
        {
            get { return _street1; }
            set
            {
                if (value == _street1) return;
                _street1 = value;
                OnPropertyChanged(_street1);
            }
        }

        public string Street2 { get; set; }


        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "FirstName")
                {
                    if (string.IsNullOrEmpty(FirstName))
                        result = "Le champ nom est requis";
                }
                if (columnName == "LastName")
                {
                    if (string.IsNullOrEmpty(LastName))
                        result = "Le champ prénom est requis";
                }
               
                return result;
            }
        }

        public string Error { get; }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
