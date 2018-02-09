using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using EasyStore.Annotations;
using EasyStore.Entities;

namespace EasyStore.models
{
    public class StockViewModel:INotifyPropertyChanged, IDataErrorInfo
    {
        private decimal _unitPrice;
        private int _qnt;
        private DateTime _creationDate;
        private DateTime? _fabricationDate;
        private DateTime? _lapsingDate;
        private long _productId;
        private int? _storeId;
        private int? _supplierId;
        private List<Product> _products;
        public Int64 Id { get; set; }

        public List<Product> Products
        {
            get
            {
                using (var db = new ObjectContext())
                {
                    return _products ?? db.Products.ToList();
                }
               
            }
            set
            {
                if (Equals(value, _products)) return;
                _products = value;
                OnPropertyChanged("_products");
            }
        }
        private List<Supplier> _suppliers;

        public List<Supplier> Suppliers
        {
            get {
                using (var db = new ObjectContext())
                {
                    return _suppliers?? db.Suppliers.ToList();
                }
            }
            set { _suppliers = value; }
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


        public DateTime CreationDate
        {
            get { return _creationDate; }
            set
            {
                if (value.Equals(_creationDate)) return;
                _creationDate = value;
                OnPropertyChanged("_creationDate");
            }
        }

        public DateTime? FabricationDate
        {
            get { return _fabricationDate; }
            set
            {
                if (value.Equals(_fabricationDate)) return;
                _fabricationDate = value;
                OnPropertyChanged("_fabricationDate");
            }
        }

        public DateTime? LapsingDate
        {
            get { return _lapsingDate; }
            set
            {
                if (value.Equals(_lapsingDate)) return;
                _lapsingDate = value;
                OnPropertyChanged("_lapsingDate");
            }
        }

        public Int64 Product_Id
        {
            get { return _productId; }
            set
            {
                if (value == _productId) return;
                _productId = value;
                OnPropertyChanged("_productId");
            }
        }

        public int? Store_Id
        {
            get { return _storeId; }
            set
            {
                if (value == _storeId) return;
                _storeId = value;
                OnPropertyChanged("_storeId");
            }
        }

        public int? Supplier_Id
        {
            get { return _supplierId; }
            set
            {
                if (value == _supplierId) return;
                _supplierId = value;
                OnPropertyChanged("_supplierId");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string this[string columnName]
        {
            get {
                string result = null;
                if (columnName == "Product_Id")
                {
                    if (Product_Id==0)
                        result = "Le champ article est requis";

                }
                if (columnName == "Qnt")
                {
                    if (Qnt <= 0)
                        result = "La quantité doit etre superieur a zéro";
                }


                return result;
            }
        }

        public string Error { get; }
    }
}
