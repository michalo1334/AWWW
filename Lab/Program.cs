using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Lab.Data;
using Lab.Interfaces;
using Lab.Services;
using Lab.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<UserModel>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();


builder.Services.AddScoped<IDbService, DbConcreteService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();




//Seed(builder.Services);

async void Seed(IServiceCollection services)
{
    var bs = services.BuildServiceProvider();
    var roles = bs.GetRequiredService<RoleManager<IdentityRole>>();
    var users = bs.GetRequiredService<UserManager<UserModel>>();
    var db = bs.GetRequiredService<ApplicationDbContext>();

    if (!await roles.RoleExistsAsync("manager"))
        await roles.CreateAsync(new IdentityRole("manager"));
    if (db.Users.OfType<ManagerUserModel>().FirstOrDefault(i => i.Id == "1") == null)
    {
        var user = new ManagerUserModel()
        {
            UserName = @"manager@pcz.pl",
            NormalizedUserName = @"manager@pcz.pl",
            Email = @"manager@pcz.pl",
            NormalizedEmail = @"manager@pcz.pl",
            EmailConfirmed = true,
            Id = "1",
            SecurityStamp = string.Empty
        };
        await users.CreateAsync(user, "123##qweQWE");
        await users.AddToRoleAsync(user, "manager");
    }

    //Customer 1
    if(!await roles.RoleExistsAsync("customer"))
        await roles.CreateAsync(new IdentityRole("customer"));
    if (db.Users.OfType<UserModel>().FirstOrDefault(i => i.Id == "2") == null)
    {
        var user = new ManagerUserModel()
        {
            UserName = @"customer1@pcz.pl",
            NormalizedUserName = @"customer1@pcz.pl",
            Email = @"customer1@pcz.pl",
            NormalizedEmail = @"customer1@pcz.pl",
            EmailConfirmed = true,
            Id = "2",
            SecurityStamp = string.Empty
        };
        await users.CreateAsync(user, "123##qweQWE");
        await users.AddToRoleAsync(user, "customer");
    }

}

app.Run();
