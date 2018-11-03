using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using EasyStore.Entities;
using LinqToDB;

namespace EasyStore.views
{
    /// <summary>
    /// Logique d'interaction pour payementView.xaml
    /// </summary>
    public partial class payementView : UserControl
    {
        public payementView()
        {
            InitializeComponent();
            using (var db = new ObjectContext())
            {
                PaymentDg.ItemsSource = db.Payement.LoadWith(x=>x.Customer).ToList();
                CustomersCb.ItemsSource = db.Customers.ToList();
            }
        }

        

        private void RefreshBtn_OnClick(object sender, RoutedEventArgs e)
        {
           // throw new NotImplementedException();
        }

        private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var payement = (Payement) PaymentInfo.DataContext;
             if (payement != null)
            {
                using (var db = new ObjectContext())
                {
                    payement.Customer_Id = ((Customer)CustomersCb.SelectedItem).Id;
                    payement.CreationDate = DateTime.Now;
                    db.InsertWithIdentity(payement);
                    PaymentDg.ItemsSource = db.Payement.LoadWith(x => x.Customer).ToList();

                    var customer = db.Customers.FirstOrDefault(x => x.Id == payement.Customer_Id);
                    if (customer != null)
                    {
                        customer.Debt -= payement.PaidAmmount;
                        db.Update(customer);
                        foreach (var invoice in (List<Invoice>) InvoiceDg.ItemsSource)
                        {
                            if (invoice.Left <= payement.PaidAmmount)
                            {
                                payement.PaidAmmount -= invoice.Left;
                                invoice.Left = 0;
                                invoice.Status = InvoiceStatus.Reglée;
                                db.Update(invoice);
                            }
                            else
                            {
                                invoice.Left -= payement.PaidAmmount;
                                payement.PaidAmmount = 0;
                                db.Update(invoice);
                                break;
                                
                            }

                        }
                       

                    }
                }
            }
        }

        private void DEletebtn_OnClick(object sender, RoutedEventArgs e)
        {
           // throw new NotImplementedException();
        }

        private void PrintBtn_OnClick(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void ExpPdfBtn_OnClick(object sender, RoutedEventArgs e)
        {
          //  throw new NotImplementedException();
        }

        private void CustomersCb_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var customer = CustomersCb.SelectedItem as Customer;
            if (customer == null) return;
            using (var db = new ObjectContext())
            {
                var invoices = db.Invoices.Where(x => x.Status == InvoiceStatus.Non_Reglée && x.Customer_Id == customer.Id).ToList();
                InvoiceDg.ItemsSource = invoices;
                TotalAmmount.Text = " "+invoices.Sum(x => x.Left).ToString(CultureInfo.InvariantCulture) +")";
            }
        }
    }
}
