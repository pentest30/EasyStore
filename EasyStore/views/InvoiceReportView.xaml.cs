using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using EasyStore.models;
using Microsoft.Reporting.WinForms;

namespace EasyStore.views
{
    /// <summary>
    /// Interaction logic for InvoiceReportView.xaml
    /// </summary>
    public partial class InvoiceReportView : Window
    {
        public InvoiceReportView(List<InvoiceReportVm> table )
        {
            InitializeComponent();
            ReportViewer.LocalReport.ReportPath =
              ReportViewer.LocalReport.ReportPath = Environment.CurrentDirectory + @"\Reporting\InvoiceRpt.rdlc";
            ReportViewer.LocalReport.DataSources.Clear();
            ReportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1",
                table.AsQueryable()));
            ReportViewer.RefreshReport();
            ReportViewer.Show();
        }
    }
}
