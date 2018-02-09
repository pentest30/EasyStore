using System;
using LinqToDB.Mapping;

namespace EasyStore.Entities
{
    [Table("Stocks")]
    public class Stock
    {
        [PrimaryKey, NotNull]
        [Column(IsIdentity = true, SkipOnInsert = false)]
        public Int64 Id { get; set; }

        [Column, Nullable]
        public decimal UnitPrice { get; set; }

        [Column, NotNull]
        public int Qnt { get; set; }

        [Association(ThisKey = "Product_Id", OtherKey = "Id")]
        public Product Product { get; set; }

        [Association(ThisKey = "Store_Id", OtherKey = "Id")]
        public Store Store { get; set; }
        [Association(ThisKey = "Supplier_Id", OtherKey = "Id")]
        public Supplier Supplier { get; set; }
        [Column, NotNull]
        public DateTime CreationDate { get; set; }

        [Column, Nullable]
        public DateTime? FabricationDate { get; set; }

        [Column, Nullable]
        public DateTime? LapsingDate { get; set; }
        [Column, NotNull]
        public Int64 Product_Id { get; set; }
        [Column, Nullable]
        public int? Store_Id { get; set; }
        [Column, Nullable]
        public int? Supplier_Id { get; set; }

    }
}
