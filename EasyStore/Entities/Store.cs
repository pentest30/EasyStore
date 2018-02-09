using LinqToDB.Mapping;

namespace EasyStore.Entities
{
    [Table("Stores")]
    public class Store
    {
        [PrimaryKey, NotNull]
        [Column(IsIdentity = true, SkipOnInsert = true)]

        public int Id { get; set; }
        [Column, NotNull]
        public string StoreName { get; set; }
        [Column, Nullable]
        public string Tel { get; set; }
        [Column, Nullable]
        public string Address { get; set; }
        [Column, Nullable]
        public string Email { get; set; }
        [Column, Nullable]
        public string NRC { get; set; }
        [Column, Nullable]
        public string NIF { get; set; }
        [Column, Nullable]
        public string City { get; set; }

    }
}
