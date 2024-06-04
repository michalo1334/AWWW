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
    public async Task<IActionResult> AddToCart(int id)
    {
        var userId = _userManager.GetUserId(User);

        UserModel userFull = await _context.Users
            .Where(u => u.Id == userId)
            .Include(u => u.ShoppingCart)
            .ThenInclude(sc => sc.Items)
            .ThenInclude(sci => sci.Product)
            .FirstOrDefaultAsync()!;

        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        var sc = userFull.ShoppingCart;

        if(sc.Items.Any(i => i.Product.Id == id))
        {
            var item = sc.Items.First(i => i.Product.Id == id);
            item.Quantity++;
        }
        else
        {
            sc.Items.Add(new ShoppingCartItemModel { Product = product, Quantity = 1 });
        }

        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    [Authorize(Roles = "customer")]    
    public async Task<IActionResult> RemoveFromCart(int id)
    {
        var userId = _userManager.GetUserId(User);

        UserModel userFull = await _context.Users
            .Where(u => u.Id == userId)
            .Include(u => u.ShoppingCart)
            .ThenInclude(sc => sc.Items)
            .ThenInclude(sci => sci.Product)
            .FirstOrDefaultAsync()!;

        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        var sc = userFull.ShoppingCart;

        if (sc.Items.Any(i => i.Product.Id == id))
        {
            var item = sc.Items.First(i => i.Product.Id == id);
            item.Quantity--;

            if (item.Quantity == 0)
            {
                sc.Items.Remove(item);
            }
        }

        _context.SaveChanges();

        return RedirectToAction("Index");
    } 

    [Authorize(Roles = "customer")]
    public async Task<IActionResult> ClearCart()
    {
        var userId = _userManager.GetUserId(User);

        UserModel userFull = await _context.Users
            .Where(u => u.Id == userId)
            .Include(u => u.ShoppingCart)
            .ThenInclude(sc => sc.Items)
            .ThenInclude(sci => sci.Product)
            .FirstOrDefaultAsync()!;

        var sc = userFull.ShoppingCart;

        sc.Items.Clear();

        _context.SaveChanges();

        return RedirectToAction("Index");
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