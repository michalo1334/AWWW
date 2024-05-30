using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class CatalogModel
{
    [Key]
    public int Id { get; set; }
    public string? Title { get; set; }

    public ICollection<ProductModel> Products { get; set; } = new List<ProductModel>();
}
