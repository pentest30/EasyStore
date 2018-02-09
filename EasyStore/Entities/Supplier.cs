using System;
using LinqToDB.Mapping;

namespace EasyStore.Entities
{
    [Table("Suppliers")]
    public class Supplier
    {
        [PrimaryKey, NotNull]
        [Column(IsIdentity = true, SkipOnInsert = true)]

        public int Id { get; set; }
        [Column, NotNull]
        public string FirstName { get; set; }
        [Column, NotNull]
        public string LastName { get; set; }
        [Column, Nullable]
        public string Tel { get; set; }
        [Column, Nullable]
        public string Email { get; set; }
        [Column, Nullable]
        public string City { get; set; }
        [Column, Nullable]
        public string Street1 { get; set; }
        [Column, Nullable]
        public string Street2 { get; set; }
        [Column, NotNull]
        public DateTime CreationDate { get; set; }

    }
}
