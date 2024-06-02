using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class CatalogModel
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Nazwa katalogu jest wymagana")]
    [RegularExpression("[A-Z]([a-zA-Z ]){4,14}", 
    ErrorMessage = "Nazwa katalogu musi zaczynać się z dużej litery i mieć od 5 do 15 znaków")]
    public string? Title { get; set; }

    public ICollection<ProductModel> Products { get; set; } = new List<ProductModel>();
}
