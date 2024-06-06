using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Lab.Models;
using Lab.Data;
using Microsoft.EntityFrameworkCore;
using Lab.Interfaces;
using Lab.Services;
using NuGet.Packaging;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;

namespace Lab.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IDbService _dbService;


    public HomeController(ILogger<HomeController> logger, IDbService dbService)
    {
        _logger = logger;
        _dbService = dbService;
    }

    public IActionResult Test(int id) {
        return Content($"Test {id}");
    }

    public IActionResult Seed() {
        if (_dbService.AnyCatalogs()) {
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
    

        _dbService.AddCatalogs(catalog1, catalog2, catalog3);
        _dbService.AddTags(tag1, tag2, tag3);
        _dbService.AddProducts(product1, product2, product3);
        _dbService.Save();


        return RedirectToAction("Index");
    }

    public IActionResult Index()
    {
        //return Forbid();
        //return Unauthorized();
        //StatusCode(500);

        var products = _dbService.AllProducts();
        return View(products);
    }

    public IActionResult Seek(string q="") {
        var products = _dbService.ProductsByPhrase(q);
        return View("Index", products);
    }

    [HttpGet]
    public IActionResult Add() {
        return View();
    }

    [HttpPost]
    public IActionResult Add(TagModel tag) {
        _dbService.AddTags(tag);
        _dbService.Save();
        return RedirectToAction("Index");
    }

    [Authorize(Roles="manager")]
    public IActionResult Privacy()
    {
        ViewBag.userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { 
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            Code = HttpContext.Response.StatusCode,
            Title = ReasonPhrases.GetReasonPhrase(HttpContext.Response.StatusCode),
            Message = null
            });
    }
}
