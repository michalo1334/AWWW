using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class ProductModel
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Nazwa produktu jest wymagana")]
    [RegularExpression("[A-Z]([a-zA-Z0-9 ]){4,25}", 
    ErrorMessage = "Nazwa produktu musi zaczynać się z dużej litery i mieć od 5 do 25 znaków")]
    public string? Title { get; set; }
    
    [RegularExpression("^.{0,3000}$|<(?!.*\b(on\\w+|style)\\s*=)[^>]*>", 
    ErrorMessage = "Opis produktu nie może zawierać więcej niż 3000 znaków")]
    public string? Description { get; set; }
    
    public CatalogModel? Catalog { get; set; }

    public ICollection<TagModel> Tags { get; set; } = new List<TagModel>();
}
