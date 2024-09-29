using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FireDepartment.Models;
using FireDepartment.Models.Data;

namespace FireDepartment.Controllers
{
    public class InventoryController : Controller
    {
        private readonly FireDepartmentDBContext _context;

        public InventoryController(FireDepartmentDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var fireDepartmentDBContext = _context.Inventorie.Include(i => i.Oborudovaniye);
            return View(await fireDepartmentDBContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventorie
                .Include(i => i.Oborudovaniye)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        public IActionResult Create()
        {
            ViewData["OborudovaniyeId"] = new SelectList(_context.Oborudovaniye, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Location,State,Quantity,OborudovaniyeId")] Inventory inventory)
        {
            try
            {
                _context.Add(inventory);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException) { return View(ErrorConstants.InvalidInputError); throw; }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventorie.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }
            ViewData["OborudovaniyeId"] = new SelectList(_context.Oborudovaniye, "Id", "Name", inventory.OborudovaniyeId);
            return View(inventory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Location,State,Quantity,OborudovaniyeId")] Inventory inventory)
        {
            if (id != inventory.Id)
            {
                return NotFound();
            }

            try
            {
                _context.Update(inventory);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryExists(inventory.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (DbUpdateException) { return View(ErrorConstants.InvalidInputError); throw; }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventorie
                .Include(i => i.Oborudovaniye)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inventory = await _context.Inventorie.FindAsync(id);
            if (inventory != null)
            {
                _context.Inventorie.Remove(inventory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventoryExists(int id)
        {
            return _context.Inventorie.Any(e => e.Id == id);
        }
    }
}
