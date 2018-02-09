using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using EasyStore.Annotations;
using EasyStore.Entities;

namespace EasyStore.models
{
    public class ProductViewModel: IDataErrorInfo, INotifyPropertyChanged
    {
        private string _name;
        private int _qnt;
        private int _minQnt;
        private string _fullDescription;
        private int? _categoryId;
        private int? _supplierId;
        private MesureUnit _mesureUnit;
        private decimal _unitPrice;
        private long _id;

        public Int64 Id
        {
            get { return _id; }
            set
            {
                if (value == _id) return;
                _id = value;
                OnPropertyChanged("_id");
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged(_name);
            }
        }

        public string FullDescription
        {
            get { return _fullDescription; }
            set
            {
                if (value == _fullDescription) return;
                _fullDescription = value;
                OnPropertyChanged("_fullDescription");
            }
        }

        public int Qnt
        {
            get { return _qnt; }
            set
            {
                if (value == _qnt) return;
                _qnt = value;
                OnPropertyChanged("_qnt");
            }
        }

        public int MinQnt
        {
            get { return _minQnt; }
            set
            {
                if (value == _minQnt) return;
                _minQnt = value;
                OnPropertyChanged("_minQnt");
            }
        }

      
        public int? Category_Id
        {
            get { return _categoryId; }
            set
            {
                if (value == _categoryId) return;
                _categoryId = value;
                OnPropertyChanged("_categoryId");
            }
        }

        public int? Supplier_Id
        {
            get { return _supplierId; }
            set
            {
                if (value == _supplierId) return;
                _supplierId = value;
                OnPropertyChanged("_categoryId");
            }
        }

        public MesureUnit MesureUnit
        {
            get { return _mesureUnit; }
            set
            {
                if (value == _mesureUnit) return;
                _mesureUnit = value;
                OnPropertyChanged("_mesureUnit");
            }
        }

        public decimal UnitPrice
        {
            get { return _unitPrice; }
            set
            {
                if (value == _unitPrice) return;
                _unitPrice = value;
                OnPropertyChanged("_unitPrice");
            }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "Name")
                {
                    if (string.IsNullOrEmpty(Name))
                        result = "Le champ nom est requis";

                }
                if (columnName == "UnitPrice")
                {
                    if (UnitPrice <= 0)
                        result = "Le prix doit etre superieur a zéro";
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
