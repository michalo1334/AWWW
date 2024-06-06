using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using Lab.Models;
using Lab.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Lab.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IStringLocalizer<ProductModel> _localizer;

        public ProductController(ApplicationDbContext context, IStringLocalizer<ProductModel> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = _localizer["Index"];

            var products = await _context.Products
                .Include(p => p.Catalog)
                .Include(p => p.Tags)
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description ?? "",
                    CatalogName = p.Catalog.Title,
                    Tags = p.Tags.ToList()
                }).ToListAsync();

            return View(products);
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productModel = await _context.Products
                .Include(p => p.Catalog)
                .Include(p => p.Tags)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (productModel == null)
            {
                return NotFound();
            }

            var productViewModel = new ProductViewModel
            {
                Id = productModel.Id,
                Title = productModel.Title,
                Description = productModel.Description,
                CatalogName = productModel.Catalog?.Title,
                Tags = productModel.Tags.ToList()
            };

            return View(productViewModel);
        }


        // GET: Product/Create
        [Authorize(Roles = "manager")]
        public IActionResult Create()
        {
            ViewBag.AvailableCatalogs = new SelectList(_context.Catalogs, "Title", "Title");
            ViewBag.AvailableTags = new SelectList(_context.Tags, "Id", "Title");
            ViewData["Title"] = _localizer["Create"];
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> Create([Bind("Title,Description,CatalogName,TagIds")] ProductViewModel productVM)
        {
            var catalog = await _context.Catalogs.FirstOrDefaultAsync(c => c.Title == productVM.CatalogName);

            var product = new ProductModel
            {
                Title = productVM.Title,
                Description = productVM.Description,
                Catalog = catalog
            };

            if (catalog == null)
                ModelState.AddModelError("CatalogName", _localizer["Catalog not found"]);

            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();

                // Adding Tags
                if (productVM.TagIds != null && productVM.TagIds.Any())
                {
                    var tags = _context.Tags.Where(t => productVM.TagIds.Contains(t.Id)).ToList();
                    product.Tags = tags;
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.AvailableCatalogs = new SelectList(_context.Catalogs, "Title", "Title");
            ViewBag.AvailableTags = new SelectList(_context.Tags, "Id", "Title");
            return View(productVM);
        }

        // GET: Product/Edit/5
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productModel = await _context.Products
                .Include(p => p.Tags)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (productModel == null)
            {
                return NotFound();
            }

            var productViewModel = new ProductViewModel
            {
                Id = productModel.Id,
                Title = productModel.Title,
                Description = productModel.Description,
                CatalogName = productModel.Catalog?.Title,
                TagIds = productModel.Tags.Select(t => t.Id).ToList()
            };

            ViewBag.AvailableCatalogs = new SelectList(_context.Catalogs, "Title", "Title");
            ViewBag.AvailableTags = new SelectList(_context.Tags, "Id", "Title");
            return View(productViewModel);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,CatalogName,TagIds")] ProductViewModel productVM)
        {
            if (id != productVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var productModel = await _context.Products
                        .Include(p => p.Tags)
                        .FirstOrDefaultAsync(p => p.Id == id);

                    if (productModel == null)
                    {
                        return NotFound();
                    }

                    var catalog = await _context.Catalogs.FirstOrDefaultAsync(c => c.Title == productVM.CatalogName);
                    if (catalog == null)
                    {
                        ModelState.AddModelError("CatalogName", "Catalog not found");
                        ViewBag.AvailableCatalogs = new SelectList(_context.Catalogs, "Title", "Title");
                        ViewBag.AvailableTags = new SelectList(_context.Tags, "Id", "Title");
                        return View(productVM);
                    }

                    productModel.Title = productVM.Title;
                    productModel.Description = productVM.Description;
                    productModel.Catalog = catalog;
                    productModel.Tags = _context.Tags.Where(t => productVM.TagIds.Contains(t.Id)).ToList();

                    _context.Update(productModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductModelExists(productVM.Id))
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

            ViewBag.AvailableCatalogs = new SelectList(_context.Catalogs, "Title", "Title");
            ViewBag.AvailableTags = new SelectList(_context.Tags, "Id", "Title");
            return View(productVM);
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

        private bool ProductModelExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
