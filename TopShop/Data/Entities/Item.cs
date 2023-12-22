using System.ComponentModel.DataAnnotations.Schema;

namespace TopShop.Data.Entities;

[Table("items")]
public class Item
{
    [Column("id")]
    public Guid Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("price")]
    public decimal Price { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }
}
