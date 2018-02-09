using System.Windows;
using EasyStore.views;

namespace EasyStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CustomerBtn_OnClick(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = new customerview();
        }

        private void ProductBtn_OnClick(object sender, RoutedEventArgs e)
        {
            ContentControl.Content =  new productView();
        }

        private void SuppBtn_OnClick(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = new supplierView();
        }

        private void StockBtn_OnClick(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = new stockView();
        }

        private void Invoicebtn_OnClick(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = new invoiceListView();
        }

        private void NewOrderBtn_OnClick(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = new invoiceItemView();

        }

        private void StoreBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var win = new StoreWin();
            win.ShowDialog();
        }
    }
}
