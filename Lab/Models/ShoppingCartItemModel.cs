using System.ComponentModel.DataAnnotations;

namespace Lab.Models
{
    public class ShoppingCartItemModel
    {
        [Key]
        public int Id { get; set; }
        public ProductModel Product { get; set; }

        public ShoppingCartModel shoppingCart { get; set; }
    }
}