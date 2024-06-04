using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Lab.Models;
using Microsoft.AspNetCore.Identity;
using Humanizer;

namespace Lab.Data;

public class ApplicationDbContext : IdentityDbContext<UserModel, IdentityRole, string>
{
    public DbSet<TagModel> Tags { get; set; }
    public DbSet<ProductModel> Products { get; set; }
    public DbSet<CatalogModel> Catalogs { get; set; }

    public DbSet<ShoppingCartModel> ShoppingCarts { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<UserModel>()
        .HasDiscriminator<string>("UType")
        .HasValue<UserModel>("user")
        .HasValue<ManagerUserModel>("manager");

    
    }
}
