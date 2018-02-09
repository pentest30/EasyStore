using System;
using LinqToDB.Mapping;

namespace EasyStore.Entities
{
    [Table("Customers")]
    public class Customer
    {
        [PrimaryKey, NotNull]
        [Column(IsIdentity = true, SkipOnInsert = true)]
        public Int64 Id { get; set; }
        [Column, NotNull]
        public string FirstName { get; set; }
        [Column, NotNull]
        public string LastName { get; set; }
        [Column]
        public string Tel { get; set; }
        [Column]
        public string Email { get; set; }
        [Association(ThisKey = "Store_Id", OtherKey = "Id")]
        public Store Store { get; set; }
        [Column]
        public string City { get; set; }
        [Column]
        public string Street1 { get; set; }
        [Column]
        public string Street2 { get; set; }
        [Column]
        public DateTime CreationDate { get; set; }
        [Column, NotNull]
        public decimal Debt { get; set; }

        
        public String FullName => FirstName + " " + LastName;
    }
}
