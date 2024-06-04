using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab.Data;
using Microsoft.AspNetCore.Authorization;
using Lab.Models;

namespace Lab.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productModel = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productModel == null)
            {
                return NotFound();
            }

            return View(productModel);
        }

        // GET: Product/Create
        [Authorize(Roles = "manager")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> Create([Bind("Id,Title,Description")] ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productModel);
        }

        // GET: Product/Edit/5
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productModel = await _context.Products.FindAsync(id);
            if (productModel == null)
            {
                return NotFound();
            }
            return View(productModel);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description")] ProductModel productModel)
        {
            if (id != productModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductModelExists(productModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productModel);
        }

        // GET: Product/Delete/5
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productModel = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productModel == null)
            {
                return NotFound();
            }

            return View(productModel);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productModel = await _context.Products.FindAsync(id);
            if (productModel != null)
            {
                _context.Products.Remove(productModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        } 

        // POST: Product/AddToCart/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="customer")]
        public async Task<IActionResult> AddToCart(int id)
        {
            var shoppingCart = await _context.ShoppingCarts
                .Include(s => s.Items)
                .FirstOrDefaultAsync(s => s.User.Id == User.Identity.Name);
            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCartModel();
            }

            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            shoppingCart.Items.Add(new ShoppingCartItemModel()
            {
                Product = product
            });

            await _context.SaveChangesAsync();

            // Add product to cart
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="customer")]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var shoppingCart = await _context.ShoppingCarts
                .Include(s => s.Items)
                .FirstOrDefaultAsync(s => s.User.Id == User.Identity.Name);
            if (shoppingCart == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var item = shoppingCart.Items.FirstOrDefault(i => i.Product.Id == id);
            if (item != null)
            {
                shoppingCart.Items.Remove(item);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



        private bool ProductModelExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
