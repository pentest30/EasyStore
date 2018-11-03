using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using EasyStore.Entities;
using EasyStore.Infrastructure;
using iTextSharp.text;
using iTextSharp.text.pdf;
using LinqToDB;

namespace EasyStore.views
{
    /// <summary>
    /// Interaction logic for invoiceItemView.xaml
    /// </summary>
    public partial class invoiceItemView
    {
        List<InvoiceItem> invoiceItems = new List<InvoiceItem>();
        public invoiceItemView()
        {
            InitializeComponent();
          
            using (var db = new ObjectContext())
            {
                ProductDg.ItemsSource = db.Products.Where(x=>x.Qnt>0).ToList();
                CustomersCb.ItemsSource = db.Customers.ToList();
            }
        }

        private void AddBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(ProductDg.SelectedItem is Product product))
                return;
            
            if (invoiceItems.All(x => x.Product_Id !=product.Id))
            {
                var invoiceItem = new InvoiceItem();
                invoiceItem.Product_Id = product.Id;
                invoiceItem.Product = product;
                if (TxtPrice.Value.HasValue) invoiceItem.UnitPrice = Convert.ToDecimal(TxtPrice.Value);
                if (TxtQnt.Value.HasValue)
                    invoiceItem.Qnt += (int)TxtQnt.Value;
                else invoiceItem.Qnt += 1;

                invoiceItem.THT = invoiceItem.Qnt * invoiceItem.UnitPrice;
                invoiceItem.TTC = invoiceItem.Qnt * invoiceItem.UnitPrice + invoiceItem.THT * invoiceItem.Tax;
                invoiceItems.Add(invoiceItem);
            }
            else
            {
                InvoiceItem first = invoiceItems.FirstOrDefault(x=>x.Product_Id == product.Id);
               
                if (first != null)
                {
                    if (TxtQnt.Value != null)
                        first.Qnt += (int) TxtQnt.Value;
                    else first.Qnt += 1;
                    first.THT = first.Qnt * first.UnitPrice;
                    first.TTC = first.Qnt * first.UnitPrice + first.THT * first.Tax;
                }
               
            }
            InvoiceItemDg.ItemsSource = null;
            InvoiceItemDg.ItemsSource = invoiceItems;
            if (invoiceItems.Any())
            {
                TotalTxt.Text = invoiceItems.Sum(x => x.TTC).ToString(CultureInfo.InvariantCulture);
                TotalNetTxt.Text = invoiceItems.Sum(x => x.THT).ToString(CultureInfo.InvariantCulture);
            }
        }

        private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (CustomersCb.SelectedIndex == -1)
            {
                MessageBox.Show("Il faut séléctionner un client", "Attention", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }
            if (invoiceItems.Count == 0)
            {
                MessageBox.Show("Il faut ajouter aux moins une ligne à la facture ", "Attention", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }
            using (var db = new ObjectContext())
            {
                var invoice = new Invoice();
                invoice.CreationDate = DateTime.Now;
                invoice.InvoiceNumber = $"{DateTime.Now.Year}-{(db.Invoices.Count() + 1).ToString().PadLeft(4,'0')}";
                invoice.Customer_Id = ((Customer) CustomersCb.SelectedItem).Id;
                invoice.InvoiceType = InvoiceType.Facture;
                invoice.IsValid = true;
                if (AmmoutpaidXTxt.Value.HasValue)
                    invoice.PaidAmmount = Convert.ToDecimal(AmmoutpaidXTxt.Value);
                invoice.THT =(decimal) invoiceItems.Sum(x=>x.THT);
                invoice.TTC = invoiceItems.Sum(x=>x.TTC);
                invoice.Left = decimal.Subtract(invoice.TTC , invoice.PaidAmmount);
                if (invoice.Left > 0)
                {
                    var customer = db.Customers.FirstOrDefault(x => x.Id == invoice.Customer_Id);
                    if (customer != null)
                    {
                        customer.Debt += invoice.Left;
                        if (customer.Debt <= 0)
                        {
                            invoice.Status = InvoiceStatus.Reglée;
                            invoice.Left = 0;
                        }
                        else
                        {
                            invoice.Left = customer.Debt;
                            invoice.Status = invoice.Left < invoice.PaidAmmount ? InvoiceStatus.Reglée : InvoiceStatus.Non_Reglée;
                            
                        }
                        db.InsertWithInt64Identity(invoice);
                    }
                    db.Update(customer);
                }
                var newInvoice = db.Invoices.FirstOrDefault(x =>
                    x.CreationDate == invoice.CreationDate && x.Customer_Id == invoice.Customer_Id &&
                    x.InvoiceNumber == invoice.InvoiceNumber);
                foreach (var invoiceItem in invoiceItems)
                {
                    if (newInvoice == null) continue;
                    invoiceItem.Invoice_Id = newInvoice.Id;
                    db.InsertWithInt64Identity(invoiceItem);
                    var stock = db.Products.FirstOrDefault(x => x.Id == invoiceItem.Product_Id);
                    if (stock != null && stock.Qnt >= invoiceItem.Qnt)
                    {
                        stock.Qnt -= invoiceItem.Qnt;
                        db.Update(stock);
                    }
                    else if (stock != null)
                    {
                        stock.Qnt = 0;
                        db.Update(stock);
                    }
                    var stock2 = new StockHistoric();
                    stock2.Product_Id = stock.Id;
                    stock2.Qnt = invoiceItem.Qnt;
                    stock2.Date = DateTime.Now;
                    stock2.Movement = MovementStock.Sortie;
                    db.InsertWithIdentity(stock2);
                }
                MessageBox.Show("Enregistrement terminé avec succcés !", "Enregistrement", MessageBoxButton.OK,
                    MessageBoxImage.Information);


            }

        }

        private void PrintBtn_OnClick(object sender, RoutedEventArgs e)
        {
           // throw new NotImplementedException();
        }

        private void ProductDg_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(ProductDg.SelectedItem is Product product))
                return;
            TxtPrice.Value =Convert.ToDouble( product.UnitPrice);
        }

        private void DeleteBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var s = InvoiceItemDg.SelectedItem as InvoiceItem;
            if (s==null) return;
            invoiceItems.Remove(s);
            InvoiceItemDg.ItemsSource = null;
            InvoiceItemDg.ItemsSource = invoiceItems;

        }

        private void ExpPdfBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (invoiceItems.Count ==0)
            {
                MessageBox.Show("Enregistrer la facture avant l'impréssion", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;

            }
            Invoice invoice;
            Store store;
            using (var db = new ObjectContext())
            {
                var i  = invoiceItems.FirstOrDefault().Invoice_Id;
                invoice = db.Invoices.LoadWith(x=>x.Customer)
                    .FirstOrDefault(x => x.Id ==i );
                store = db.Stores.FirstOrDefault();
            }
            if (store == null)
            {
                MessageBox.Show("Il faut enregistrer les informations de votre etablissement");
                return;
            }
            if (invoice == null )
            {
                MessageBox.Show("Enregistrer la facture avant l'impréssion", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;

            }
            
            string[] headers = { "Désignation", "Unité de mesure","Prix unitaire", "Quantité", "THT", "TTC" };
            float[] columnWidths = { 200f, 200, 200,200, 200f, 140f };

            Document doc = new Document(PageSize.A4, 15, 15, 15, 15);
            MemoryStream stream = new MemoryStream();
            PdfWriter.GetInstance(doc, stream).CloseStream = false;
            doc.Open();
            PdfPTable tblHeader = new PdfPTable(3);
            tblHeader.WidthPercentage = 100;

            PdfPCell leftCell = new PdfPCell();
            leftCell.Border = 0;

            iTextSharp.text.Paragraph paragraph = new iTextSharp.text.Paragraph($"Bon de livraison",
                FontFactory.GetFont("Calibri", 18, BaseColor.BLACK));
        
            paragraph.Alignment = Element.ALIGN_LEFT;
            leftCell.AddElement(paragraph);
            leftCell.AddElement(null);
            leftCell.AddElement(null);
            leftCell.AddElement(null);
            PdfPCell rightCell = new PdfPCell();
            rightCell.Border = 0;
            var prg = new Paragraph(store.StoreName);
            rightCell.AddElement(prg);
            prg = new Paragraph("NRC: "+store.NRC);
            rightCell.AddElement(prg);
            prg = new Paragraph("NIF: "+store.NIF);
            rightCell.AddElement(prg);
            prg = new Paragraph(store.City);
            rightCell.AddElement(prg);

            PdfPCell emptyCell = new PdfPCell();
            emptyCell.Border = 0;

            tblHeader.AddCell(leftCell);
            tblHeader.AddCell(emptyCell);
            tblHeader.AddCell(rightCell);
            doc.Add(tblHeader);
           
            doc.Add(new Chunk(Environment.NewLine));
            iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            // call the below to add the line when required.
            doc.Add(p);
            doc.Add(new Chunk(Environment.NewLine));
            doc.Add(new Chunk(Environment.NewLine));
            PdfPTable table = new PdfPTable(2);
            table.WidthPercentage = 90;
            var bodyLeftCell = new PdfPCell();
            bodyLeftCell.Border = 0;
            bodyLeftCell.AddElement(new Paragraph(invoice.Customer.FullName));
            bodyLeftCell.AddElement(new Paragraph(invoice.Customer.Street1));
            bodyLeftCell.AddElement(new Paragraph(invoice.Customer.City));

            var bodyRightCell = new PdfPCell()
                ;
            bodyRightCell.Border = 0;
            table.AddCell(bodyLeftCell);
            // table.AddCell(emptyCell);
            table.AddCell(bodyRightCell);
            table.DefaultCell.Border = 0; ;

            doc.Add(table);
            doc.Add(new Chunk(Environment.NewLine));
            p.Alignment = Element.ALIGN_CENTER;


            doc.Add(new Chunk(Environment.NewLine));
            doc.Add(new Chunk(Environment.NewLine));

            PdfPTable pTable = new PdfPTable(headers.Length);
            pTable.WidthPercentage = 90;
            pTable.SetWidths(columnWidths);
            foreach (string t in headers)
            {
                var cell = new PdfPCell(new Phrase(t, FontFactory.GetFont("Calibri", 12, BaseColor.BLACK)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = new BaseColor(230, 230, 250)
                };


                pTable.AddCell(cell);
            }
            foreach (var invoiceItem in invoiceItems)
            {
                pTable.AddCell(ItextSharpHelper.Cell(invoiceItem.Product.Name, BaseColor.BLACK));
                pTable.AddCell(ItextSharpHelper.Cell(invoiceItem.Product.MesureUnit.ToString(), BaseColor.BLACK));
                pTable.AddCell(ItextSharpHelper.Cell(invoiceItem.UnitPrice.ToString("F"), BaseColor.BLACK));
                pTable.AddCell(ItextSharpHelper.Cell(invoiceItem.Qnt.ToString(), BaseColor.BLACK));
                pTable.AddCell(ItextSharpHelper.Cell(invoiceItem.THT.ToString(CultureInfo.InvariantCulture), BaseColor.BLACK));
                pTable.AddCell(ItextSharpHelper.Cell(invoiceItem.TTC.ToString(CultureInfo.InvariantCulture), BaseColor.BLACK));
            }
            doc.Add(p);
            doc.Add(pTable);
            doc.Close();
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.DefaultExt = ".pdf";
            dlg.Filter = "pdf documents (.pdf)|*.pdf";
            if (dlg.ShowDialog() != true) return;

            string filename = dlg.FileName;
            FileManager.SavePdfFile(stream, filename);
        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var term = ((TextBox)sender).Text;
            using (var db = new ObjectContext())
            {
                ProductDg.ItemsSource = db.Products.Where(x => x.Name.Contains(term)||x.UnitPrice.ToString(CultureInfo.InvariantCulture).Contains(term)).ToList();
            }
        }
    }
}
