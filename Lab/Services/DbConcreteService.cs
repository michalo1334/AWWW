namespace Lab.Services;

using Lab.Data;
using Lab.Interfaces;

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
            .Where(p => p.Description.Contains(phrase))
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

    public void CreateTag(string name)
    {
        _context.Tags.Add(new TagModel { Title = name });
        _context.SaveChanges();
    }
}

