using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using EasyStore.Entities;
using LinqToDB;

namespace EasyStore.views
{
    /// <summary>
    /// Interaction logic for StockHistoricView.xaml
    /// </summary>
    public partial class StockHistoricView : UserControl
    {
        public StockHistoricView()
        {
            InitializeComponent();
            using (var db = new ObjectContext())
            {
                StockDg.ItemsSource = db.StocksHistoric.LoadWith(x=>x.Product).ToList();
            }
            
        }

        private void RptBtn_OnClick(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            
            using (var db = new ObjectContext())
            {
                if (MetroSearchBox.Text == "")
                {
                    StockDg.ItemsSource = db.StocksHistoric.LoadWith(x => x.Product).ToList();
                    return;
                }
                if (StartPeriod.SelectedDate == null || EndPeriod.SelectedDate == null)
                {
                    StockDg.ItemsSource = db.StocksHistoric.LoadWith(x => x.Product).Where(x=>x.Product.Name.Contains(MetroSearchBox.Text)).ToList();
                    return;
                }

                var startPeriod = StartPeriod.SelectedDate.Value;
                var endPeriod = EndPeriod.SelectedDate.Value;

                StockDg.ItemsSource = db.StocksHistoric.LoadWith(x => x.Product)
                    .Where(x => x.Date >= startPeriod && x.Date < endPeriod&& x.Product.Name.Contains(MetroSearchBox.Text)).ToList();

            }
        }

        private void AddBtn_OnClick(object sender, RoutedEventArgs e)
        {
            using (var db = new ObjectContext())
            {
                if (StartPeriod.SelectedDate == null || EndPeriod.SelectedDate == null)
                {
                    StockDg.ItemsSource = db.StocksHistoric.LoadWith(x => x.Product).ToList();
                    return;
                }

                var startPeriod = StartPeriod.SelectedDate.Value;
                var endPeriod = EndPeriod.SelectedDate.Value;

                StockDg.ItemsSource = db.StocksHistoric.LoadWith(x => x.Product)
                    .Where(x => x.Date >= startPeriod && x.Date < endPeriod).ToList();

            }
        }
    }
    
}
