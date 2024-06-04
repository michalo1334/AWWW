using Lab.Data;
using Lab.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab.Controllers;

public class ShoppingCartController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<UserModel> _userManager;

    public ShoppingCartController(ApplicationDbContext context, UserManager<UserModel> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // GET: ShoppingCart
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        var shoppingCart = await _context.ShoppingCarts
            .Include(sc => sc.Items)
            .ThenInclude(sci => sci.Product)
            .FirstOrDefaultAsync(sc => sc.User.Id == user.Id);

        if(shoppingCart == null)
        {
            shoppingCart = new ShoppingCartModel
            {
                User = user,
                Items = new List<ShoppingCartItemModel>()
            };
            _context.ShoppingCarts.Add(shoppingCart);
            await _context.SaveChangesAsync();
        }

        return View(shoppingCart);
    }
}