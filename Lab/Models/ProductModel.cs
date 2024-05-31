using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class ProductModel
{
    [Key]
    public int Id { get; set; }

    [RegularExpression("[A-Z]([a-zA-Z0-9 ]){4,25}")]
    public string? Title { get; set; }
    
    [RegularExpression("^.{1,3000}$|<(?!.*\b(on\\w+|style)\\s*=)[^>]*>")]
    public string? Description { get; set; }

    public CatalogModel Catalog { get; set; }

    public ICollection<TagModel> Tags { get; set; } = new List<TagModel>();
}
