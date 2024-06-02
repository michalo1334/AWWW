using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab.Data;
using Microsoft.AspNetCore.Authorization;

namespace Lab.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CatalogController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Catalog
        public async Task<IActionResult> Index()
        {
            return View(await _context.Catalogs.ToListAsync());
        }

        // GET: Catalog/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogModel = await _context.Catalogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catalogModel == null)
            {
                return NotFound();
            }

            return View(catalogModel);
        }

        // GET: Catalog/Create
        [Authorize(Roles = "manager")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Catalog/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> Create([Bind("Id,Title")] CatalogModel catalogModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(catalogModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(catalogModel);
        }

        // GET: Catalog/Edit/5
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogModel = await _context.Catalogs.FindAsync(id);
            if (catalogModel == null)
            {
                return NotFound();
            }
            return View(catalogModel);
        }

        // POST: Catalog/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title")] CatalogModel catalogModel)
        {
            if (id != catalogModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(catalogModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatalogModelExists(catalogModel.Id))
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
            return View(catalogModel);
        }

        // GET: Catalog/Delete/5
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogModel = await _context.Catalogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catalogModel == null)
            {
                return NotFound();
            }

            return View(catalogModel);
        }

        // POST: Catalog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var catalogModel = await _context.Catalogs.FindAsync(id);
            if (catalogModel != null)
            {
                _context.Catalogs.Remove(catalogModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatalogModelExists(int id)
        {
            return _context.Catalogs.Any(e => e.Id == id);
        }
    }
}
