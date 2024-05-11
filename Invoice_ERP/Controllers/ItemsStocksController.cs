using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Invoice_ERP.Data;
using Invoice_ERP.Models;

namespace Invoice_ERP.Controllers
{
    public class ItemsStocksController : Controller
    {
        private readonly Invoice_ERPContext _context;

        public ItemsStocksController(Invoice_ERPContext context)
        {
            _context = context;
        }

        // GET: ItemsStocks
        public async Task<IActionResult> Index()
        {
            var invoice_ERPContext = _context.ItemsStock.Include(i => i.Category).Include(i => i.Supplier).Include(i => i.UnitOfMeasure);
            return View(await invoice_ERPContext.ToListAsync());
        }

        // GET: ItemsStocks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemsStock = await _context.ItemsStock
                .Include(i => i.Category)
                .Include(i => i.Supplier)
                .Include(i => i.UnitOfMeasure)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemsStock == null)
            {
                return NotFound();
            }

            return View(itemsStock);
        }

        // GET: ItemsStocks/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.CategoryModel, "Id", "CreatedBy");
            ViewData["SupplierId"] = new SelectList(_context.Supplier, "Id", "CreatedBy");
            ViewData["UnitOfMeasureId"] = new SelectList(_context.UnitOfMeasure, "Id", "CreatedBy");
            return View();
        }


        // POST: ItemsStocks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,UnitPrice,SellPrice,Quantity,CategoryId,SupplierId,UnitOfMeasureId,ManufactureDate,ExpiryDate,SKU,Note,TotalEarned,CreatedOn,CreatedBy")] ItemsStock itemsStock)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemsStock);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.CategoryModel, "Id", "CreatedBy", itemsStock.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Supplier, "Id", "CreatedBy", itemsStock.SupplierId);
            ViewData["UnitOfMeasureId"] = new SelectList(_context.UnitOfMeasure, "Id", "CreatedBy", itemsStock.UnitOfMeasureId);
            return View(itemsStock);
        }

        // GET: ItemsStocks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemsStock = await _context.ItemsStock.FindAsync(id);
            if (itemsStock == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.CategoryModel, "Id", "CreatedBy", itemsStock.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Supplier, "Id", "CreatedBy", itemsStock.SupplierId);
            ViewData["UnitOfMeasureId"] = new SelectList(_context.UnitOfMeasure, "Id", "CreatedBy", itemsStock.UnitOfMeasureId);
            return View(itemsStock);
        }

        // POST: ItemsStocks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,UnitPrice,SellPrice,Quantity,CategoryId,SupplierId,UnitOfMeasureId,ManufactureDate,ExpiryDate,SKU,Note,TotalEarned,CreatedOn,CreatedBy")] ItemsStock itemsStock)
        {
            if (id != itemsStock.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemsStock);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemsStockExists(itemsStock.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.CategoryModel, "Id", "CreatedBy", itemsStock.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Supplier, "Id", "CreatedBy", itemsStock.SupplierId);
            ViewData["UnitOfMeasureId"] = new SelectList(_context.UnitOfMeasure, "Id", "CreatedBy", itemsStock.UnitOfMeasureId);
            return View(itemsStock);
        }

        // GET: ItemsStocks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemsStock = await _context.ItemsStock
                .Include(i => i.Category)
                .Include(i => i.Supplier)
                .Include(i => i.UnitOfMeasure)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemsStock == null)
            {
                return NotFound();
            }

            return View(itemsStock);
        }

        // POST: ItemsStocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemsStock = await _context.ItemsStock.FindAsync(id);
            if (itemsStock != null)
            {
                _context.ItemsStock.Remove(itemsStock);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemsStockExists(int id)
        {
            return _context.ItemsStock.Any(e => e.Id == id);
        }
    }
}
