using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AutoMapper;
using EasyStore.Entities;
using EasyStore.Infrastructure;
using EasyStore.models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using LinqToDB;

namespace EasyStore.views
{
    /// <summary>
    /// Interaction logic for customerview.xaml
    /// </summary>
    public partial class customerview
    {
        private MapperConfiguration _cfg;
        public customerview()
        {
            InitializeComponent();
            using (var db = new ObjectContext())
            {
                InvoiceDg.ItemsSource = db.Customers.ToList();
            }
        }

        private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            
            var customeriew = CustomerInfo.DataContext as CustomerViewModel;
            if (customeriew != null && (string.IsNullOrEmpty(
                customeriew.FirstName) || string.IsNullOrWhiteSpace(customeriew.FirstName) ||
                                        string.IsNullOrEmpty(customeriew.LastName) || string.IsNullOrWhiteSpace(customeriew.LastName)))
            {
                MessageBox.Show("Nom ou Prénom est null", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

             _cfg = AutomapperConfig.Config<CustomerViewModel, Customer>();
            var customer = _cfg.CreateMapper().Map<Customer>(customeriew);
            using (var db = new ObjectContext())
            {
                if (customer.Id == 0)
                    db.InsertWithIdentity(customer);
                else
                    db.Update(customer);
                InvoiceDg.ItemsSource = db.Customers.ToList();
            }
        }

        private void PrintBtn_OnClick(object sender, RoutedEventArgs e)
        {
           
           
        }

        private void InvoiceDg_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(InvoiceDg.SelectedItem is Customer custmer)) return;
            _cfg = AutomapperConfig.Config<Customer, CustomerViewModel>();
            CustomerInfo.DataContext=  _cfg.CreateMapper().Map<CustomerViewModel>(custmer);
        }

        private void DEletebtn_OnClick(object sender, RoutedEventArgs e)
        {
            var custmer = InvoiceDg.SelectedItem as Customer;

            if (custmer == null) return;
            var result = MessageBox.Show("Est vous sûr de vouloir supprimer cet enregistrement", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (!result.ToString().Equals("Yes")) return;

            using (var db = new ObjectContext())
            {
                db.Delete(custmer);
                InvoiceDg.ItemsSource = db.Customers.ToList();
            }
        }

        private void RefreshBtn_OnClick(object sender, RoutedEventArgs e)
        {
           CustomerInfo.DataContext = new CustomerViewModel();
        }

        private void ExpPdfBtn_OnClick(object sender, RoutedEventArgs e)
        {

            string[] headers = {"Nom", "Prénom", "Tél", "Ville", "Adresse", "Créance"};
            float[] columnWidths = {200f, 200, 200f, 140f, 200f, 100f};

            Document doc = new Document(PageSize.A4, 5, 5, 5, 5);
            MemoryStream stream = new MemoryStream();
            PdfWriter.GetInstance(doc, stream).CloseStream = false;

            doc.Open();
            Paragraph paragraph = new Paragraph($"Liste des clients le: {DateTime.Now.Date.ToShortDateString()}",
                FontFactory.GetFont("Calibri", 16, BaseColor.BLACK));
            string imageUrl = Environment.CurrentDirectory + @"\Images\store56px.png";
            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imageUrl);
            img.SpacingAfter = 1f;
            img.Alignment = Element.ALIGN_RIGHT;
            doc.Add(img);
            //  doc.Add(new Chunk(Environment.NewLine));
            paragraph.Alignment = Element.ALIGN_LEFT;
            doc.Add(paragraph);
            doc.Add(new Chunk(Environment.NewLine));
            PdfPTable table = new PdfPTable(headers.Length);
            table.WidthPercentage = 99;
            table.SetWidths(columnWidths);
            foreach (string t in headers)
            {
                var cell = new PdfPCell(new Phrase(t, FontFactory.GetFont("Calibri", 12, BaseColor.BLACK)));

                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.BackgroundColor = new BaseColor(230, 230, 250);

                table.AddCell(cell);
            }
            foreach (var customer in (List<Customer>) InvoiceDg.ItemsSource)
            {
                table.AddCell(Cell(customer.FirstName));
                table.AddCell(Cell(customer.LastName));
                table.AddCell(Cell(customer.Tel));
                table.AddCell(Cell(customer.City));
                table.AddCell(Cell(customer.Street1));
                table.AddCell(Cell(customer.Debt.ToString("F")));
            }
            doc.Add(table);
            doc.Close();
          
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.DefaultExt = ".pdf";
            dlg.Filter = "pdf documents (.pdf)|*.pdf";
            if (dlg.ShowDialog() != true) return;
           
            string filename = dlg.FileName;
            FileManager.SavePdfFile(stream, filename);
        }

        

        private static PdfPCell Cell(string property)
        {
            return new PdfPCell(new Paragraph(property,
                    FontFactory.GetFont("Calibri", 10, BaseColor.BLACK)))
                { HorizontalAlignment = Element.ALIGN_CENTER };
        }
    }
}
