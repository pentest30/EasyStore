using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using EasyStore.models;
using Microsoft.Reporting.WinForms;

namespace EasyStore.views
{
    /// <summary>
    /// Interaction logic for BlReportView.xaml
    /// </summary>
    public partial class BlReportView : Window
    {
        public BlReportView(List<InvoiceReportVm> table)
        {
            InitializeComponent();
            ReportViewer.LocalReport.ReportPath =
           ReportViewer.LocalReport.ReportPath = Environment.CurrentDirectory + @"\Reporting\BlRpt.rdlc";
            ReportViewer.LocalReport.DataSources.Clear();
            ReportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1",
                table.AsQueryable()));
            ReportViewer.RefreshReport();
            ReportViewer.Show();
        }
    }
}
