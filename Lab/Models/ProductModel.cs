using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class ProductModel
{
    [Key]
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }

    public CatalogModel Catalog { get; set; }

    public ICollection<TagModel> Tags { get; set; } = new List<TagModel>();
}
