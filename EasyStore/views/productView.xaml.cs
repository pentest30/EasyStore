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
    /// Interaction logic for productView.xaml
    /// </summary>
    public partial class productView
    {
        private MapperConfiguration _cfg;
        public productView()
        {
            InitializeComponent();
            using (var db = new ObjectContext())
            {
               
                InvoiceDg.ItemsSource = db.Products.ToList();
            }
        }

        private void InvoiceDg_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(InvoiceDg.SelectedItem is Product product)) return;
            _cfg = AutomapperConfig.Config<Product, ProductViewModel>();
            CustomerInfo.DataContext = _cfg.CreateMapper().Map<ProductViewModel>(product);

        }

        private void RefreshBtn_OnClick(object sender, RoutedEventArgs e)
        {
            CustomerInfo.DataContext = new ProductViewModel();
            InvoiceDg.SelectedIndex = -1;
        }

        private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var productViewModel = CustomerInfo.DataContext as ProductViewModel;
            if (productViewModel != null && (string.IsNullOrEmpty(
                                            productViewModel.Name) || string.IsNullOrWhiteSpace(productViewModel.Name) ))
            {
                MessageBox.Show("Désignation produit est null", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _cfg = AutomapperConfig.Config<ProductViewModel, Product>();
            var product = _cfg.CreateMapper().Map<Product>(productViewModel);
            product.CreationDate = DateTime.Now;
            switch (CbMeasureUnit.Text)
            {
                case "Kilogramme":
                    product.MesureUnit = MesureUnit.Kilogramme;
                    break;
                case "Carton":
                    product.MesureUnit = MesureUnit.Carton;
                    break;
                case "Litre":
                    product.MesureUnit = MesureUnit.Litre;
                    break;
                case "Paquet":
                    product.MesureUnit = MesureUnit.Paquet;
                    break;
                case "Boite":
                    product.MesureUnit = MesureUnit.Boite;
                    break;
                default: product.MesureUnit = MesureUnit.Autre;
                    break;
            }

            using (var db = new ObjectContext())
            {
                if (db.Products.Count() > 5)
                {
                    MessageBox.Show("Evaluation","Vous pouvez pas ajouter plus de 5 produits dans cette version");
                    return;
                }
                if (product.Id == 0)
                    db.InsertWithIdentity(product);
                else
                    db.Update(product);
                InvoiceDg.ItemsSource = db.Products.ToList();
            }
        }

        private void DEletebtn_OnClick(object sender, RoutedEventArgs e)
        {
            var product = InvoiceDg.SelectedItem as Product;

            if (product == null) return;
            var result = MessageBox.Show("Est vous sûr de vouloir supprimer cet enregistrement!", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (!result.ToString().Equals("Yes")) return;

            using (var db = new ObjectContext())
            {
                db.Delete(product);
                InvoiceDg.ItemsSource = db.Products.ToList();
            }
        }

        private void PrintBtn_OnClick(object sender, RoutedEventArgs e)
        {
           // throw new NotImplementedException();
        }

        private void ExpPdfBtn_OnClick(object sender, RoutedEventArgs e)
        {
            string[] headers = { "Désignation", "Prix unitaire", "Unité de mesure", "Quantité stock", "Quantité.min" };
            float[] columnWidths = { 200f, 200, 200, 200f, 140f };

            Document doc = new Document(PageSize.A4, 5, 5, 5, 5);
            MemoryStream stream = new MemoryStream();
            PdfWriter.GetInstance(doc, stream).CloseStream = false;

            doc.Open();
            Paragraph paragraph = new Paragraph($"Rapport de stock le: {DateTime.Now.Date.ToShortDateString()}",
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
                var cell = new PdfPCell(new Phrase(t, FontFactory.GetFont("Calibri", 12, BaseColor.BLACK)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = new BaseColor(230, 230, 250)
                };


                table.AddCell(cell);
            }
            foreach (var customer in (List<Product>)InvoiceDg.ItemsSource)
            {
                table.AddCell(ItextSharpHelper.Cell(customer.Name , BaseColor.BLACK));
                table.AddCell(ItextSharpHelper.Cell(customer.UnitPrice.ToString("F"), BaseColor.BLACK));
                table.AddCell(ItextSharpHelper.Cell(customer.MesureUnit.ToString(), BaseColor.BLACK));
                table.AddCell(ItextSharpHelper.Cell( customer.Qnt.ToString(), (customer.Qnt <= customer.MinQnt) ? BaseColor.RED : BaseColor.BLACK));
                table.AddCell(ItextSharpHelper.Cell(customer.MinQnt.ToString(), BaseColor.BLACK));
                
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
       
        private void TextBoxBase_OnSelectionChanged(object sender, RoutedEventArgs e)
        {
            var term = ((TextBox) sender).Text;
            using (var db = new ObjectContext())
            {
                InvoiceDg.ItemsSource = db.Products.Where(x=>x.Name.Contains(term)).ToList();
            }
        }

        private void AddBtn_OnClicktn_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(InvoiceDg.SelectedItem is Product product) || !TxtQnt.Value.HasValue)
                return;
            product.Qnt +=Convert.ToInt32(TxtQnt.Value);
            using (var db = new ObjectContext())
            {
                db.Update(product);
                InvoiceDg.ItemsSource = db.Products.ToList();
                var stock = new StockHistoric();
                stock.Product_Id = product.Id;
                stock.Qnt = Convert.ToInt32(TxtQnt.Value);
                stock.Date = DateTime.Now;
                stock.Movement = MovementStock.Entrée;
                db.InsertWithIdentity(stock);
            }
        }
    }
}
