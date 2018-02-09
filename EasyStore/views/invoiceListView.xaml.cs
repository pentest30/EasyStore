using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using EasyStore.Entities;
using EasyStore.Infrastructure;
using EasyStore.models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using LinqToDB;

namespace EasyStore.views
{
    /// <summary>
    /// Interaction logic for invoiceListView.xaml
    /// </summary>
    public partial class invoiceListView : UserControl
    {
        public invoiceListView()
        {
            InitializeComponent();
            using (var db = new ObjectContext())
            {
                InvoiceDg.ItemsSource = db.Invoices.LoadWith(x=>x.Customer).ToList();
            }
        }

        private void InvoiceDg_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = InvoiceDg.SelectedItem as Invoice;
            if (selectedItem==null) return;
            using (var db = new ObjectContext())
            {
                InvoiceItemDg.ItemsSource = db.InvoiceItems.LoadWith(x => x.Product).Where(x=>x.Invoice_Id == selectedItem.Id).ToList();
            }
        }

        private void AddOrderBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            if (mainWindow != null) mainWindow.ContentControl.Content = new invoiceItemView();
        }

        private void PrintBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var invoice = InvoiceDg.SelectedItem as Invoice;
            if (invoice == null)
            {
                MessageBox.Show("Séléctionner une facture", "Inforation", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }
            string[] headers = { "Désignation", "Prix unitaire", "Quantité", "THT", "TTC" };
            float[] columnWidths = { 200f, 200, 200, 200f, 140f };
            Store store;
            using (var db = new ObjectContext())
            {
               
                store = db.Stores.FirstOrDefault();
            }
            if (store == null)
            {
                MessageBox.Show("Il faut enregistrer les informations de votre etablissement");
                return;
            }
            Document doc = new Document(PageSize.A4, 15, 15, 15, 15);
            MemoryStream stream = new MemoryStream();
            PdfWriter.GetInstance(doc, stream).CloseStream = false;
            doc.Open();

            PdfPTable tblHeader = new PdfPTable(3);
            tblHeader.WidthPercentage = 100;

            PdfPCell leftCell = new PdfPCell();
            leftCell.Border = 0;

            iTextSharp.text.Paragraph paragraph = new iTextSharp.text.Paragraph($"Facture",
                FontFactory.GetFont("Calibri", 24, BaseColor.BLACK));

            paragraph.Alignment = Element.ALIGN_LEFT;
            leftCell.AddElement(paragraph);
            leftCell.AddElement(new Paragraph("N°: " + invoice.InvoiceNumber));
            leftCell.AddElement(new Paragraph("Date: " + invoice.CreationDate.ToShortDateString()));
            PdfPCell rightCell = new PdfPCell();
            rightCell.Border = 0;
            var prg = new Paragraph(store.StoreName,
                FontFactory.GetFont("Calibri", 12, BaseColor.BLACK));
            rightCell.AddElement(prg);
            prg = new Paragraph("NRC: " + store.NRC,
                FontFactory.GetFont("Calibri", 12, BaseColor.BLACK));
            rightCell.AddElement(prg);
            prg = new Paragraph("NIF: " + store.NIF,
                FontFactory.GetFont("Calibri", 12, BaseColor.BLACK));
            rightCell.AddElement(prg);
            prg = new Paragraph(store.City,
                FontFactory.GetFont("Calibri", 12, BaseColor.BLACK));
            rightCell.AddElement(prg);
            PdfPCell emptyCell = new PdfPCell();
            emptyCell.Border = 0;
            tblHeader.AddCell(leftCell);
            tblHeader.AddCell(emptyCell);
            tblHeader.AddCell(rightCell);
            doc.Add(tblHeader);

            iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            // call the below to add the line when required.
            doc.Add(p);
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
            pTable.WidthPercentage = 100;
            pTable.SetWidths(columnWidths);
            foreach (string t in headers)
            {
                var cell = new PdfPCell(new Phrase(t, FontFactory.GetFont("Calibri", 14, BaseColor.BLACK)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = BaseColor.LIGHT_GRAY
                };


                pTable.AddCell(cell);
            }
            using (var db = new ObjectContext())
            {
                foreach (var invoiceItem in db.InvoiceItems.LoadWith(x=>x.Product).Where(x=>x.Invoice_Id == invoice.Id))
                {
                    pTable.AddCell(ItextSharpHelper.Cell(invoiceItem.Product.Name, BaseColor.BLACK));
                    pTable.AddCell(ItextSharpHelper.Cell(invoiceItem.UnitPrice.ToString("F"), BaseColor.BLACK));
                    pTable.AddCell(ItextSharpHelper.Cell(invoiceItem.Qnt.ToString(), BaseColor.BLACK));
                    pTable.AddCell(ItextSharpHelper.Cell(invoiceItem.THT.ToString(CultureInfo.InvariantCulture), BaseColor.BLACK));
                    pTable.AddCell(ItextSharpHelper.Cell(invoiceItem.TTC.ToString(CultureInfo.InvariantCulture), BaseColor.BLACK));
                }
            }
            doc.Add(p);
            doc.Add(new Chunk(Environment.NewLine));

            doc.Add(pTable);
            doc.Add(new Chunk(Environment.NewLine));

            doc.Add(new Chunk(Environment.NewLine));
            PdfPTable table2 = new PdfPTable(3);
            table2.WidthPercentage = 90;
 
            table2.AddCell(ItextSharpHelper.LeftCell("", BaseColor.DARK_GRAY));
            
            table2.AddCell(ItextSharpHelper.LeftCell("", BaseColor.DARK_GRAY));
            var cell1 = new PdfPCell();
            var tt = new Paragraph("TOTAL: " + invoice.TTC.ToString(CultureInfo.InvariantCulture) + " DA.",
                FontFactory.GetFont("Calibri", 16, BaseColor.BLACK));
            var pp=new Paragraph("Montant payé: " + invoice.PaidAmmount.ToString(CultureInfo.InvariantCulture) + " DA.", FontFactory.GetFont("Calibri", 12, BaseColor.BLACK));                   
            var ppp =new Paragraph("Reste: " + invoice.Left.ToString(CultureInfo.InvariantCulture) + " DA.",FontFactory.GetFont("Calibri", 12, BaseColor.BLACK)) ;
            cell1.Border = 0;
            cell1.AddElement(tt);
            cell1.AddElement(pp);
            cell1.AddElement(ppp);
            table2.AddCell(cell1);
            table2.DefaultCell.Border = 0; ;

            doc.Add(table2);
            doc.Close();
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.DefaultExt = ".pdf";
            dlg.Filter = "pdf documents (.pdf)|*.pdf";
            if (dlg.ShowDialog() != true) return;

            string filename = dlg.FileName;
            FileManager.SavePdfFile(stream, filename);
        }

        private void ExpPdfBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var invoice = InvoiceDg.SelectedItem as Invoice;
            if (invoice == null)
            {
                MessageBox.Show("Séléctionner une facture", "Inforation", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }
            string[] headers = { "Désignation", "Unité de mesure", "Prix unitaire", "Quantité", "THT", "TTC" };
            float[] columnWidths = { 200f, 200, 200, 200, 200f, 140f };
            Store store;
            using (var db = new ObjectContext())
            {

                store = db.Stores.FirstOrDefault();
            }

            if (store == null)
            {
                MessageBox.Show("Il faut enregistrer les informations de votre etablissement");
                return;
            }
            Document doc = new Document(PageSize.A4, 15, 15, 15, 15);
            MemoryStream stream = new MemoryStream();
            PdfWriter.GetInstance(doc, stream).CloseStream = false;
            doc.Open();

            PdfPTable tblHeader = new PdfPTable(3);
            tblHeader.WidthPercentage = 100;

            PdfPCell leftCell = new PdfPCell();
            leftCell.Border = 0;

            iTextSharp.text.Paragraph paragraph = new iTextSharp.text.Paragraph($"Bon de livraison",
                FontFactory.GetFont("Calibri", 24, BaseColor.BLACK));

            paragraph.Alignment = Element.ALIGN_LEFT;
            leftCell.AddElement(paragraph);
            leftCell.AddElement(new Paragraph("N°: " + invoice.InvoiceNumber));
            leftCell.AddElement(new Paragraph("Date: " + invoice.CreationDate.ToShortDateString()));
            PdfPCell rightCell = new PdfPCell();
            rightCell.Border = 0;
            var prg = new Paragraph(store.StoreName,
                FontFactory.GetFont("Calibri", 12, BaseColor.BLACK));
            rightCell.AddElement(prg);
            prg = new Paragraph("NRC: " + store.NRC,
                FontFactory.GetFont("Calibri", 12, BaseColor.BLACK));
            rightCell.AddElement(prg);
            prg = new Paragraph("NIF: " + store.NIF,
                FontFactory.GetFont("Calibri", 12, BaseColor.BLACK));
            rightCell.AddElement(prg);
            prg = new Paragraph(store.City,
                FontFactory.GetFont("Calibri", 12, BaseColor.BLACK));
            rightCell.AddElement(prg);
            PdfPCell emptyCell = new PdfPCell();
            emptyCell.Border = 0;
            tblHeader.AddCell(leftCell);
            tblHeader.AddCell(emptyCell);
            tblHeader.AddCell(rightCell);
            doc.Add(tblHeader);

            iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            // call the below to add the line when required.
            doc.Add(p);
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
            pTable.WidthPercentage = 100;
            pTable.SetWidths(columnWidths);
            foreach (string t in headers)
            {
                var cell = new PdfPCell(new Phrase(t, FontFactory.GetFont("Calibri", 12, BaseColor.BLACK)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = BaseColor.WHITE
                };


                pTable.AddCell(cell);
            }
            using (var db = new ObjectContext())
            {
                foreach (var invoiceItem in db.InvoiceItems.LoadWith(x => x.Product).Where(x => x.Invoice_Id == invoice.Id))
                {
                    pTable.AddCell(ItextSharpHelper.Cell(invoiceItem.Product.Name, BaseColor.BLACK));
                    pTable.AddCell(ItextSharpHelper.Cell(invoiceItem.Product.MesureUnit.ToString(), BaseColor.BLACK));
                    pTable.AddCell(ItextSharpHelper.Cell(invoiceItem.UnitPrice.ToString("F"), BaseColor.BLACK));
                    pTable.AddCell(ItextSharpHelper.Cell(invoiceItem.Qnt.ToString(), BaseColor.BLACK));
                    pTable.AddCell(ItextSharpHelper.Cell(invoiceItem.THT.ToString(CultureInfo.InvariantCulture), BaseColor.BLACK));
                    pTable.AddCell(ItextSharpHelper.Cell(invoiceItem.TTC.ToString(CultureInfo.InvariantCulture), BaseColor.BLACK));
                }
            }
            doc.Add(new Chunk(Environment.NewLine));

            doc.Add(p);
            doc.Add(new Chunk(Environment.NewLine));
            doc.Add(pTable);
          
            doc.Close();
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.DefaultExt = ".pdf";
            dlg.Filter = "pdf documents (.pdf)|*.pdf";
            if (dlg.ShowDialog() != true) return;

            string filename = dlg.FileName;
            try
            {

                FileManager.SavePdfFile(stream, filename);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message , "Attention!" , MessageBoxButton.OK , MessageBoxImage.Error);
//                throw;
            }
        }

        private void TextBoxBase_OnSelectionChanged(object sender, RoutedEventArgs e)
        {
            var term = ((TextBox) sender).Text;
            using (var db = new ObjectContext())
            {
                InvoiceDg.ItemsSource = db.Invoices.LoadWith(x => x.Customer).Where(x=>x.Customer.FirstName.Contains(term) ||x.Left.ToString(CultureInfo.InvariantCulture).Contains(term)||x.InvoiceNumber.Contains(term)).ToList();
            }
        }

        private void RptBtn_OnClick(object sender, RoutedEventArgs e)
        {
           var invoice = InvoiceDg.SelectedItem as Invoice;
            if (invoice == null)
            {
                MessageBox.Show("Séléctionner une facture", "Inforation", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }
            var  list = new List<InvoiceReportVm>();
            using (var db =  new ObjectContext())
            {
                var invoiceItems = db.InvoiceItems.LoadWith(x => x.Product).Where(x => x.Invoice_Id == invoice.Id);
                var store = db.Stores.FirstOrDefault();
                foreach (
                    var invoiceItem in invoiceItems)
                {
                   
                    var reportVm = new InvoiceReportVm();
                    reportVm.InvoiceNumber = invoice.InvoiceNumber;
                    reportVm.Date = invoice.CreationDate;
                    reportVm.TotalAmmount = invoice.TTC;
                    reportVm.PaidAmmount = invoice.PaidAmmount;
                    reportVm.Left = invoice.Left;
                    if (store != null) reportVm.City = store.City;
                    if (store != null) reportVm.Store = store.StoreName;
                    if (store != null) reportVm.NIF = store.NIF;
                    if (store != null) reportVm.NRC = store.NRC;
                    reportVm.Customer = invoice.Customer.FullName;
                    reportVm.Qnt = invoiceItem.Qnt;
                    reportVm.UnitPrice = invoiceItem.UnitPrice;
                    reportVm.Product = invoiceItem.Product.Name;
                    reportVm.LineTotal = invoiceItem.TTC;
                    list.Add(reportVm);
                }
            }
            var  rptView = new InvoiceReportView(list);
            rptView.ShowDialog();

        }

        private void BlRptBtn_OnClick(object sender, RoutedEventArgs e)
        {
              var invoice = InvoiceDg.SelectedItem as Invoice;
            if (invoice == null)
            {
                MessageBox.Show("Séléctionner une facture", "Inforation", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }
            var  list = new List<InvoiceReportVm>();
            using (var db =  new ObjectContext())
            {
                var invoiceItems = db.InvoiceItems.LoadWith(x => x.Product).Where(x => x.Invoice_Id == invoice.Id);
                var store = db.Stores.FirstOrDefault();
                foreach (
                    var invoiceItem in invoiceItems)
                {
                   
                    var reportVm = new InvoiceReportVm();
                    reportVm.InvoiceNumber = invoice.InvoiceNumber;
                    reportVm.Date = invoice.CreationDate;
                    reportVm.TotalAmmount = invoice.TTC;
                    reportVm.PaidAmmount = invoice.PaidAmmount;
                    reportVm.Left = invoice.Left;
                    if (store != null) reportVm.City = store.City;
                    if (store != null) reportVm.Store = store.StoreName;
                    if (store != null) reportVm.NIF = store.NIF;
                    if (store != null) reportVm.NRC = store.NRC;
                    reportVm.Customer = invoice.Customer.FullName;
                    reportVm.Qnt = invoiceItem.Qnt;
                    reportVm.UnitPrice = invoiceItem.UnitPrice;
                    reportVm.Product = invoiceItem.Product.Name;
                    reportVm.LineTotal = invoiceItem.TTC;
                    reportVm.MeasureUnit = invoiceItem.Product.MesureUnit.ToString();
                    list.Add(reportVm);
                }
            }
            var  rptView = new BlReportView(list);
            rptView.ShowDialog();

        }
    }
}
