using System;
using LinqToDB.Mapping;

namespace EasyStore.Entities
{
    public class Payement
    {
        [PrimaryKey, NotNull]
        [Column(IsIdentity = true, SkipOnInsert = true)]
        public Int64 Id { get; set; }
        [Association(ThisKey = "Customer_Id", OtherKey = "Id")]
        public Customer Customer { get; set; }
        [Column, NotNull]
        public DateTime CreationDate { get; set; }
        public decimal PaidAmmount { get; set; }
        [Column, NotNull]
        public Int64? Customer_Id { get; set; }
    }
}
