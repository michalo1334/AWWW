using System.ComponentModel.DataAnnotations;

namespace Lab.ViewModels;

public class ProductViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Product name is required")]
    [RegularExpression("[A-Z]([a-zA-Z0-9 ]){4,25}", 
    ErrorMessage = "Product name must start with a capital letter and contain 5-25 characters")]
    public string? Title { get; set; }

    [RegularExpression("^.{0,3000}$|<(?!.*\b(on\\w+|style)\\s*=)[^>]*>", 
    ErrorMessage = "Description must be less than 3000 characters and not contain HTML tags")]
    public string? Description { get; set; }

    public string? CatalogName { get; set; }

    public IList<int> TagIds { get; set; } = new List<int>();
    public IList<TagModel> Tags { get; set; } = new List<TagModel>();
}