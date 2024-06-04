using Microsoft.AspNetCore.Identity;

namespace Lab.Models
{
    public class UserModel : IdentityUser
    {
        public ShoppingCartModel ShoppingCart { get; set; }
    }
}