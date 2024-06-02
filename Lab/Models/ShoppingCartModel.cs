using System.ComponentModel.DataAnnotations;

namespace Lab.Models
{
    public class ShoppingCartModel
    {
        [Key]
        public int Id { get; set; }
        
        public UserModel User { get; set; }
        public IList<ShoppingCartItemModel> Items { get; set; } = new List<ShoppingCartItemModel>();
    }
}