using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class TagModel
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Nazwa tagu jest wymagana")]
    [RegularExpression("^[a-z0-9]{1,10}$", 
    ErrorMessage = "Nazwa tagu musi składać się z małych liter i cyfr oraz mieć od 1 do 10 znaków")]
    public string Title { get; set; }

    public ICollection<ProductModel> Products { get; set; } = new List<ProductModel>();
}
