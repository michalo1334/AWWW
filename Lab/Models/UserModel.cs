using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Lab.Models
{
    public class UserModel : IdentityUser
    {
        public int ShoppingCartId { get; set; }
        
        [ForeignKey(nameof(ShoppingCartId))]
        public ShoppingCartModel ShoppingCart { get; set; } = new();
    }
}