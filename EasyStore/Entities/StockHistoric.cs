using System;
using LinqToDB.Mapping;

namespace EasyStore.Entities
{
    public class StockHistoric
    {
        [PrimaryKey, NotNull]
        [Column(IsIdentity = true, SkipOnInsert = true)]
        public Int64 Id { get; set; }
        [Column, NotNull]
        public int Qnt { get; set; }

        [Association(ThisKey = "Product_Id", OtherKey = "Id")]
        public Product Product { get; set; }
        [Column, NotNull]
        public DateTime Date { get; set; }
        [Column, NotNull]
        public MovementStock Movement { get; set; }
        [Column, NotNull]
        public Int64 Product_Id { get; set; }

    }

    public enum MovementStock
    {
        Sortie,
        Entrée
    }
}
