using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AutoMapper;
using EasyStore.Entities;
using EasyStore.Infrastructure;
using EasyStore.models;
using LinqToDB;

namespace EasyStore.views
{
    /// <summary>
    /// Interaction logic for supplierView.xaml
    /// </summary>
    public partial class supplierView
    {
        private MapperConfiguration _cfg;
        public supplierView()
        {
            InitializeComponent();
            using (var db = new ObjectContext())
            {
                InvoiceDg.ItemsSource = db.Suppliers.ToList();
            }
        }

        private void InvoiceDg_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(InvoiceDg.SelectedItem is Supplier supplier)) return;
            _cfg = AutomapperConfig.Config<Supplier, SupplierViewModel>();
            CustomerInfo.DataContext = _cfg.CreateMapper().Map<SupplierViewModel>(supplier);
        }

        private void RefreshBtn_OnClick(object sender, RoutedEventArgs e)
        {
            CustomerInfo.DataContext = new SupplierViewModel();
        }

        private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var supplierViewModel = CustomerInfo.DataContext as SupplierViewModel;
            if (supplierViewModel != null && (string.IsNullOrEmpty(
                                            supplierViewModel.FirstName) || string.IsNullOrWhiteSpace(supplierViewModel.FirstName) ||
                                        string.IsNullOrEmpty(supplierViewModel.LastName) || string.IsNullOrWhiteSpace(supplierViewModel.LastName)))
            {
                MessageBox.Show("Nom ou Prénom est null", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _cfg = AutomapperConfig.Config<SupplierViewModel, Supplier>();
            var supplier = _cfg.CreateMapper().Map<Supplier>(supplierViewModel);
            using (var db = new ObjectContext())
            {
                if (supplier.Id == 0)
                    db.InsertWithIdentity(supplier);
                else
                    db.Update(supplier);
                InvoiceDg.ItemsSource = db.Suppliers.ToList();
            }
        }

        private void DEletebtn_OnClick(object sender, RoutedEventArgs e)
        {
            var supplier = InvoiceDg.SelectedItem as Supplier;

            if (supplier == null) return;
            var result = MessageBox.Show("Est vous sûr de vouloir supprimer cet enregistrement!", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (!result.ToString().Equals("Yes")) return;

            using (var db = new ObjectContext())
            {
                db.Delete(supplier);
                InvoiceDg.ItemsSource = db.Suppliers.ToList();
            }
        }

        private void PrintBtn_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ExpPdfBtn_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
