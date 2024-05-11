using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Invoice_ERP.Data;
using Invoice_ERP.Models;
using Microsoft.AspNetCore.Identity;
using Invoice_ERP.Areas.Identity.Data;
using System.Globalization;

namespace Invoice_ERP.Controllers
{
    public class CategoryController : Controller
    {
        private readonly UserManager<Invoice_ERPUser> _userManager;
        private readonly Invoice_ERPContext _context;

        // Constructor with UserManager and Invoice_ERPContext
        public CategoryController(UserManager<Invoice_ERPUser> userManager, Invoice_ERPContext context)
        {
            _userManager = userManager;
            _context = context;
        }



        // GET: CategoryModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.CategoryModel.ToListAsync());
        }

        // GET: CategoryModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryModel = await _context.CategoryModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryModel == null)
            {
                return NotFound();
            }

            return View(categoryModel);
        }

        // GET: CategoryModels/Create
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
            Category category = new Category
            {
                CreatedBy = createdBy,
                CreatedOn = DateTime.ParseExact(DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"), "dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture)
            };

            return View(category);
        }


        // POST: CategoryModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,CreatedOn,CreatedBy")] Category categoryModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoryModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryModel);
        }

        // GET: CategoryModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryModel = await _context.CategoryModel.FindAsync(id);
            if (categoryModel == null)
            {
                return NotFound();
            }
            return View(categoryModel);
        }

        // POST: CategoryModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,CreatedOn,CreatedBy")] Category categoryModel)
        {
            if (id != categoryModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoryModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryModelExists(categoryModel.Id))
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
            return View(categoryModel);
        }

        // GET: CategoryModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryModel = await _context.CategoryModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryModel == null)
            {
                return NotFound();
            }

            return View(categoryModel);
        }

        // POST: CategoryModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoryModel = await _context.CategoryModel.FindAsync(id);
            if (categoryModel != null)
            {
                _context.CategoryModel.Remove(categoryModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryModelExists(int id)
        {
            return _context.CategoryModel.Any(e => e.Id == id);
        }
    }
}
