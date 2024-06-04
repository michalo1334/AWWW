using System.ComponentModel.DataAnnotations;

namespace Lab.Models;

public class ShoppingCartItemModel
{
    [Key]
    public int Id { get; set; }
    
    public ProductModel Product { get; set; }
    public int Quantity { get; set; }
    public ShoppingCartModel ShoppingCart { get; set; }
}