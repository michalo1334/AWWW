namespace Lab.Services;

using System.Collections;
using Lab.Data;
using Lab.Interfaces;
using Lab.Models;
using Microsoft.EntityFrameworkCore;

public class DbConcreteService : IDbService
{
    private readonly ApplicationDbContext _context;

    public DbConcreteService(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<ProductModel> ProductsByPhrase(string phrase)
    {
        return _context.Products
            .Where(p => p.Title.Contains(phrase))
            .Include(p => p.Catalog)
            .ToList();
    }

    public IList<TagModel> AllTags()
    {
        return _context.Tags.ToList();
    }

    public IList<CatalogModel> AllCatalogs()
    {
        return _context.Catalogs.ToList();
    }

    public bool AnyCatalogs()
    {
        return _context.Catalogs.Any();
    }

    public void AddProducts(params ProductModel[] products)
    {
        _context.Products.AddRange(products);
    }

    public void AddCatalogs(params CatalogModel[] catalogs)
    {
        _context.Catalogs.AddRange(catalogs);
    }

    public void AddTags(params TagModel[] tags)
    {
        _context.Tags.AddRange(tags);
    }

    public IList<ProductModel> AllProducts()
    {
        return _context.Products.Include(p => p.Catalog).ToList();
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    public void CreateTag(string name)
    {
        _context.Tags.Add(new TagModel { Title = name });
        _context.SaveChanges();
    }
}

