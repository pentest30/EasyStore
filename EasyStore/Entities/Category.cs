using LinqToDB.Mapping;

namespace EasyStore.Entities
{
    [Table("Categories")]
    public class Category
    {
        [PrimaryKey, NotNull]
        [Column(IsIdentity = true, SkipOnInsert = true)]
        public int Id { get; set; }
        [Column, NotNull]
        public string Name { get; set; }
        [Association(ThisKey = "Category_Id", OtherKey = "Id")]
        public Category UpCategory{ get; set; }
    }
}
