using Lab.Data;
using Lab.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;

namespace Lab.Controllers;

public class ShoppingCartController : Controller
{
    private readonly UserManager<UserModel> _userManager;
    private readonly ApplicationDbContext _context;

    public ShoppingCartController(UserManager<UserModel> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    [Authorize(Roles = "customer")]
    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User);

        UserModel userFull = await _context.Users
            .Where(u => u.Id == userId)
            .Include(u => u.ShoppingCart)
            .ThenInclude(sc => sc.Items)
            .ThenInclude(sci => sci.Product)
            .FirstOrDefaultAsync()!;


        var shoppingCart = userFull.ShoppingCart;

        return View(shoppingCart);
    }

    [Authorize(Roles = "customer")]
    public async Task<IActionResult> Seed()
    {
        var user = await _userManager.GetUserAsync(User);

        //get product id 1, 2, 3
        var products = await _context.Products.Take(3).ToListAsync();

        //create shopping cart
        var sc = new ShoppingCartModel();

        user.ShoppingCart = sc;

        sc.Items.Add(new ShoppingCartItemModel { Product = products[0], Quantity = 1});
        sc.Items.Add(new ShoppingCartItemModel { Product = products[1], Quantity = 2});
        sc.Items.Add(new ShoppingCartItemModel { Product = products[2], Quantity = 3});

        _context.SaveChanges();

        return RedirectToAction("Index");
    }
}