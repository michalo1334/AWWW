using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class TagModel
{
    [Key]
    public int Id { get; set; }

    [RegularExpression("^[a-z0-9]{1,10}$")]
    public string? Title { get; set; }

    public ICollection<ProductModel> Products { get; set; } = new List<ProductModel>();
}
