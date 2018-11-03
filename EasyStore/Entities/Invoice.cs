using System;
using System.Collections.Generic;
using LinqToDB.Mapping;

namespace EasyStore.Entities
{
    [Table("Invoices")]
    public class Invoice
    {
        public Invoice()
        {
            InvoiceItems = new List<InvoiceItem>();
        }
        [PrimaryKey, NotNull]
        [Column(IsIdentity = true, SkipOnInsert = true)]
        public Int64 Id { get; set; }
        public Store Store { get; set; }
        [Association(ThisKey = "Customer_Id", OtherKey = "Id")]
        public Customer Customer { get; set; }
        [Column, NotNull]
        public DateTime CreationDate { get; set; }
        [Column, NotNull]
        public string InvoiceNumber { get; set; }
        [Column, Nullable]
        public decimal TTC { get; set; }
        [Column, Nullable]
        public decimal THT { get; set; }
        [Column, NotNull]
        public bool IsValid { get; set; }
        [Column, NotNull]
        public InvoiceType InvoiceType { get; set; }
        [Column, NotNull]
        public decimal PaidAmmount { get; set; }
        [Column, NotNull]
        public decimal Left { get; set; }
        [Column, NotNull]
        public Int64?  Customer_Id { get; set; }
        [Column, NotNull]
        public InvoiceStatus Status { get; set; }

        public List<InvoiceItem> InvoiceItems { get; set; }
    }
}
