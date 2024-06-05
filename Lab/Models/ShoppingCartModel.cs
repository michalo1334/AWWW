using System.ComponentModel.DataAnnotations;

namespace Lab.Models;

public class ShoppingCartModel
{
    [Key]
    public int Id { get; set; }

    public UserModel Owner { get; set; }

    public virtual IList<ShoppingCartItemModel> Items { get; set; } = new List<ShoppingCartItemModel>();
}