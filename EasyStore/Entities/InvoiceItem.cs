using System;
using LinqToDB.Mapping;

namespace EasyStore.Entities
{
    [Table("InvoiceItems")]
    public class InvoiceItem
    {
        [PrimaryKey, NotNull]
        [Column(IsIdentity = true, SkipOnInsert = true)]
        public Int64 Id { get; set; }
        [Association(ThisKey = "Product_Id", OtherKey = "Id")]
        public Product Product { get; set; }
        [Association(ThisKey = "Invoice_Id", OtherKey = "Id")]
        public Invoice Invoice { get; set; }
        [Column, NotNull]
        public decimal UnitPrice { get; set; }
        [Column, NotNull]
        public int Qnt { get; set; }
        [Column, NotNull]
        public decimal TTC { get; set; }
        [Column, NotNull]
        public decimal THT { get; set; }
        [Column, NotNull]
        public decimal Tax { get; set; }
        [Column, NotNull]
        public Int64 Product_Id { get; set; }
        [Column, NotNull]
        public Int64 Invoice_Id { get; set; }
    }
}
