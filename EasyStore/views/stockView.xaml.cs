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
    /// Interaction logic for stockView.xaml
    /// </summary>
    public partial class stockView : UserControl
    {
        private MapperConfiguration _cfg;
        public stockView()
        {
            InitializeComponent();
            using (var db = new ObjectContext())
            {

                InvoiceDg.ItemsSource = db.Stocks.LoadWith(x => x.Product).LoadWith(x => x.Supplier).Where(x=>x.Qnt>0)
                    .ToList();
            }
        }

        private void RefreshBtn_OnClick(object sender, RoutedEventArgs e)
        {
            CustomerInfo.DataContext = new StockViewModel();
        }

        private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var stockViewModel = CustomerInfo.DataContext as StockViewModel;
            if (stockViewModel != null && (stockViewModel.Product_Id==0 || stockViewModel.Qnt ==0))
            {
                MessageBox.Show("Désignation produit ou quntité stock est null", "Attention!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _cfg = AutomapperConfig.Config<StockViewModel, Stock>();
            var stock = _cfg.CreateMapper().Map<Stock>(stockViewModel);
            stock.CreationDate = DateTime.Now;
            using (var db = new ObjectContext())
            {
                var product = db.Products.FirstOrDefault(x => x.Id == stock.Product_Id);

                if (stock.Id == 0)
                {
                    var last = db.Stocks.OrderByDescending(x => x.Id).FirstOrDefault();
                    stock.Id = last?.Id + 1 ?? 1;
                    db.InsertWithInt64Identity(stock);

                    if (product != null)
                    {
                        product.Qnt += stock.Qnt;
                        db.Update(product);
                    }
                }
                else
                {
                    var old = db.Stocks.FirstOrDefault(x => x.Id == stock.Id);
                    db.Update(stock);
                    if (product != null)
                    {
                        if (old != null) product.Qnt += (stock.Qnt - old.Qnt);
                        db.Update(product);
                    }
                }  
              
                InvoiceDg.ItemsSource = db.Stocks.LoadWith(x => x.Product).LoadWith(x => x.Supplier)
                    .ToList();
            }
        }

        private void DEletebtn_OnClick(object sender, RoutedEventArgs e)
        {
            var stock = InvoiceDg.SelectedItem as Stock;

            if (stock == null) return;
            var result = MessageBox.Show("Est vous sûr de vouloir supprimer cet enregistrement!", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (!result.ToString().Equals("Yes")) return;

            using (var db = new ObjectContext())
            {
                db.Delete(stock);

                var product = db.Products.FirstOrDefault(x => x.Id == stock.Product_Id);
                if (product != null)
                {
                    if (product.Qnt >= stock.Qnt) product.Qnt -= stock.Qnt;
                    else product.Qnt = 0;
                    db.Update(product);
                }
                InvoiceDg.ItemsSource = db.Stocks.LoadWith(x => x.Product).LoadWith(x => x.Supplier)
                    .ToList();
            }
        }

        private void PrintBtn_OnClick(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void ExpPdfBtn_OnClick(object sender, RoutedEventArgs e)
        {
            string[] headers = { "Désignation", "Prix unitaire", "Quantité", "Date d'entrée" ,"Date de péremption"};
            float[] columnWidths = { 200f, 200, 200 , 200f, 200f };

            Document doc = new Document(PageSize.A4, 5, 5, 5, 5);
            MemoryStream stream = new MemoryStream();
            PdfWriter.GetInstance(doc, stream).CloseStream = false;

            doc.Open();
            Paragraph paragraph = new Paragraph($"Rapprot  de stock détaillé le: {DateTime.Now.Date.ToShortDateString()}",
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
            using (var db = new ObjectContext())
            {

                foreach (var stock in db.Stocks.LoadWith(w => w.Product).Where(x => x.Qnt > 0)
                    .OrderBy(x => x.Product_Id))
                {
                    table.AddCell(ItextSharpHelper.Cell(stock.Product.Name, BaseColor.BLACK));
                    table.AddCell(ItextSharpHelper.Cell(stock.Product.UnitPrice.ToString("F"), BaseColor.BLACK));
                    table.AddCell(ItextSharpHelper.Cell(stock.Qnt.ToString(),
                        (stock.Qnt <= stock.Product.MinQnt) ? BaseColor.RED : BaseColor.BLACK));
                    table.AddCell(ItextSharpHelper.Cell(stock.CreationDate.ToString("d"), BaseColor.BLACK));

                    table.AddCell(stock.LapsingDate != null
                        ? ItextSharpHelper.Cell(stock.LapsingDate.Value.ToString("d"), BaseColor.BLACK)
                        : ItextSharpHelper.Cell("", BaseColor.BLACK));
                }
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
            var term = ((TextBox)sender).Text;
            using (var db = new ObjectContext())
            {
                InvoiceDg.ItemsSource = db.Stocks.LoadWith(x=>x.Product).Where(x => x.Product.Name.Contains(term)).ToList();
            }
        }

        private void InvoiceDg_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(InvoiceDg.SelectedItem is Stock stock)) return;
            _cfg = AutomapperConfig.Config<Stock, StockViewModel>();
            CustomerInfo.DataContext = _cfg.CreateMapper().Map<StockViewModel>(stock);
        }
    }
}
