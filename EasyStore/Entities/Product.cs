using System;
using LinqToDB.Mapping;

namespace EasyStore.Entities
{
    [Table("Products")]
    public class Product
    {
        public Product()
        {
            MesureUnit = MesureUnit.Kilogramme;
        }
        [PrimaryKey, NotNull]
        [Column(IsIdentity = true, SkipOnInsert = true)]
        public Int64 Id { get; set; }
        [Column, NotNull]
        public string Name { get; set; }
        [Column, Nullable]
        public string FullDescription { get; set; }
        [Column]
        public int Qnt { get; set; }
        [Column]
        public int MinQnt { get; set; }
        [Column, Nullable]
        public DateTime CreationDate { get; set; }
        [Column, Nullable]
        public DateTime LastChange { get; set; }
        [Association(ThisKey = "Category_Id", OtherKey = "Id")]
        public Category Category { get; set; }
        [Association(ThisKey = "Supplier_Id", OtherKey = "Id")]
        public Supplier Supplier { get; set; }
        [Column, Nullable] public MesureUnit MesureUnit { get; set; }
        [Column, Nullable] public int? Category_Id { get; set; }
        [Column, Nullable] public int? Supplier_Id { get; set; }
        [Column, NotNull]
        public decimal UnitPrice { get; set; }
    }
    

}
