using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Lab.Models;

namespace Lab.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public DbSet<TagModel> Tags { get; set; }
    public DbSet<ProductModel> Products { get; set; }
    public DbSet<CatalogModel> Catalogs { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}
