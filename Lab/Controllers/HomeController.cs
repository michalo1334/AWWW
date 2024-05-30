using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Lab.Models;
using Lab.Data;
using Microsoft.EntityFrameworkCore;

namespace Lab.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;


    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Seed() {
        if (_context.Catalogs.Any()) {
            return RedirectToAction("Index");
        }

        var tag1 = new TagModel { Title = "Tag 1" };
        var tag2 = new TagModel { Title = "Tag 2" };
        var tag3 = new TagModel { Title = "Tag 3" };

        var catalog1 = new CatalogModel { Title = "Catalog 1", };
        var catalog2 = new CatalogModel { Title = "Catalog 2", };
        var catalog3 = new CatalogModel { Title = "Catalog 3", };

        var product1 = new ProductModel { Title = "Product 1", Tags = new List<TagModel> { tag1, tag2 } };
        var product2 = new ProductModel { Title = "Product 2", Tags = new List<TagModel> { tag2, tag3 } };
        var product3 = new ProductModel { Title = "Product 3", Tags = new List<TagModel> { tag1, tag3 } };

        catalog1.Products.Add(product1);
        catalog2.Products.Add(product2);
        catalog3.Products.Add(product3);
    

        _context.Catalogs.AddRange(catalog1, catalog2, catalog3);
        _context.Tags.AddRange(tag1, tag2, tag3);
        _context.Products.AddRange(product1, product2, product3);
        _context.SaveChanges();


        return RedirectToAction("Index");
    }

    public IActionResult Index()
    {
        var products = _context.Products.Include(p => p.Catalog).ToList();
        return View(products);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
