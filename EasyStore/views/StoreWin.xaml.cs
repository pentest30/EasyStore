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
using EasyStore.Entities;
using LinqToDB;

namespace EasyStore.views
{
    /// <summary>
    /// Logique d'interaction pour StoreWin.xaml
    /// </summary>
    public partial class StoreWin 
    {
        public StoreWin()
        {
            InitializeComponent();
            using (var db= new ObjectContext())
            {
                StoreGrid.DataContext = db.Stores.FirstOrDefault() ?? new Store();
            }

        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            using (var db = new ObjectContext())
            {
                var store = StoreGrid.DataContext as Store;
                if (store != null)
                {
                    if (store.Id == 0)
                        db.InsertWithInt32Identity(store);
                    else db.Update(store);
                    MessageBox.Show("Enregistrement est teminé avec succés", "Info", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
            }

        }
    }
}
