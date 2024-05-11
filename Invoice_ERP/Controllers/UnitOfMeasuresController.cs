using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Invoice_ERP.Data;
using Invoice_ERP.Models;
using Invoice_ERP.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;

namespace Invoice_ERP.Controllers
{
    [Authorize(Policy = "AdminPolicy")]
    public class UnitOfMeasuresController : Controller
    {
        private readonly UserManager<Invoice_ERPUser> _userManager;
        private readonly Invoice_ERPContext _context;

        // Constructor with UserManager and Invoice_ERPContext
        public UnitOfMeasuresController(UserManager<Invoice_ERPUser> userManager, Invoice_ERPContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: UnitOfMeasures
        public async Task<IActionResult> Index()
        {
            return View(await _context.UnitOfMeasure.ToListAsync());
        }

        // GET: UnitOfMeasures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unitOfMeasure = await _context.UnitOfMeasure
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unitOfMeasure == null)
            {
                return NotFound();
            }

            return View(unitOfMeasure);
        }

        // GET: UnitOfMeasures/Create
       
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                // Handle the case when the user is not logged in
                return RedirectToAction("Login", "Account"); // Redirect to login page or handle the scenario accordingly
            }

            // Fill the CreatedBy field with username + role as a string
            string createdBy = $"{user.firstName} {user.lastName} ({string.Join(", ", await _userManager.GetRolesAsync(user))})";

            // Create a new Category instance
            UnitOfMeasure unitOfMeasure = new UnitOfMeasure
            {
                CreatedBy = createdBy,
                CreatedOn = DateTime.ParseExact(DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"), "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture)
            };

            return View(unitOfMeasure);
        }

        // POST: UnitOfMeasures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,CreatedOn,CreatedBy")] UnitOfMeasure unitOfMeasure)
        {
            if (ModelState.IsValid)
            {
                _context.Add(unitOfMeasure);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(unitOfMeasure);
        }

        // GET: UnitOfMeasures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unitOfMeasure = await _context.UnitOfMeasure.FindAsync(id);
            if (unitOfMeasure == null)
            {
                return NotFound();
            }
            return View(unitOfMeasure);
        }

        // POST: UnitOfMeasures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,CreatedOn,CreatedBy")] UnitOfMeasure unitOfMeasure)
        {
            if (id != unitOfMeasure.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(unitOfMeasure);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnitOfMeasureExists(unitOfMeasure.Id))
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
            return View(unitOfMeasure);
        }

        // GET: UnitOfMeasures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unitOfMeasure = await _context.UnitOfMeasure
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unitOfMeasure == null)
            {
                return NotFound();
            }

            return View(unitOfMeasure);
        }

        // POST: UnitOfMeasures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var unitOfMeasure = await _context.UnitOfMeasure.FindAsync(id);
            if (unitOfMeasure != null)
            {
                _context.UnitOfMeasure.Remove(unitOfMeasure);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UnitOfMeasureExists(int id)
        {
            return _context.UnitOfMeasure.Any(e => e.Id == id);
        }
    }
}
