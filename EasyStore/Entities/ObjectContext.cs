using LinqToDB;
using LinqToDB.Data;

namespace EasyStore.Entities
{
    public class ObjectContext:DataConnection
    {
        public ObjectContext(string connection) :base(connection)
        {
            
        }

        public ObjectContext()
        {
            
        }
        public ITable<Customer> Customers => GetTable<Customer>();
        public ITable<Category> Categories  => GetTable<Category>();
        public ITable<Invoice> Invoices => GetTable<Invoice>();
        public ITable<InvoiceItem> InvoiceItems => GetTable<InvoiceItem>();
        public ITable<Product> Products => GetTable<Product>();
        public ITable<Stock> Stocks => GetTable<Stock>();
        public ITable<Store> Stores => GetTable<Store>();
        public ITable<Supplier> Suppliers => GetTable<Supplier>();
    }
}
