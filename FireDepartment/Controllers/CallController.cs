using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FireDepartment.Models;
using FireDepartment.Models.Data;
using Microsoft.AspNetCore.Authorization;

namespace FireDepartment.Controllers
{
    public class CallController : Controller
    {
        private readonly FireDepartmentDBContext _context;

        public CallController(FireDepartmentDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var fireDepartmentDBContext = _context.Call.Include(c => c.Sotrudnik);
            return View(await fireDepartmentDBContext.ToListAsync());
        }

        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search(string? categoryFilter)
        {
            if (categoryFilter == null)
            {
                return RedirectToAction("Index");
            }

            var query = _context.Call.AsQueryable();

            if (categoryFilter != null)
            {
                query = query.Where(s => s.Category == categoryFilter);
            }

            var appDbContext = await query.ToListAsync();

            return View("Index", appDbContext);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var call = await _context.Call
                .Include(c => c.Sotrudnik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (call == null)
            {
                return NotFound();
            }

            return View(call);
        }

        public IActionResult Create()
        {
            ViewData["SotrudnikId"] = new SelectList(_context.Sotrudniki, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateTimeCall,Location,Category,Description,SotrudnikId")] Call call)
        {
            try
            {
                _context.Add(call);
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

            var call = await _context.Call.FindAsync(id);
            if (call == null)
            {
                return NotFound();
            }
            ViewData["SotrudnikId"] = new SelectList(_context.Sotrudniki, "Id", "Id", call.SotrudnikId);
            return View(call);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateTimeCall,Location,Category,Description,SotrudnikId")] Call call)
        {
            if (id != call.Id)
            {
                return NotFound();
            }

            try
            {
                _context.Update(call);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CallExists(call.Id))
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

            var call = await _context.Call
                .Include(c => c.Sotrudnik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (call == null)
            {
                return NotFound();
            }

            return View(call);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var call = await _context.Call.FindAsync(id);
            if (call != null)
            {
                _context.Call.Remove(call);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CallExists(int id)
        {
            return _context.Call.Any(e => e.Id == id);
        }
    }
}
