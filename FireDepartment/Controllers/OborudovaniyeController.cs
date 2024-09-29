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
    public class OborudovaniyeController : Controller
    {
        private readonly FireDepartmentDBContext _context;

        public OborudovaniyeController(FireDepartmentDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Oborudovaniye.ToListAsync());
        }

        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search(bool? isAvailable, bool? isNotAvailable)
        {
            if (isAvailable == null && isNotAvailable == null)
            {
                return RedirectToAction("Index");
            }

            var query = _context.Oborudovaniye.AsQueryable();

            if (isAvailable == true)
            {
                query = query.Where(s => s.Status == false);
            }

            if (isNotAvailable == true)
            {
                query = query.Where(s => s.Status == true);
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

            var oborudovaniye = await _context.Oborudovaniye
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oborudovaniye == null)
            {
                return NotFound();
            }

            return View(oborudovaniye);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Type,DateTimeOfService,Status")] Oborudovaniye oborudovaniye)
        {
            try
            {
                _context.Add(oborudovaniye);
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

            var oborudovaniye = await _context.Oborudovaniye.FindAsync(id);
            if (oborudovaniye == null)
            {
                return NotFound();
            }
            return View(oborudovaniye);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Type,DateTimeOfService,Status")] Oborudovaniye oborudovaniye)
        {
            if (id != oborudovaniye.Id)
            {
                return NotFound();
            }

            try
            {
                _context.Update(oborudovaniye);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OborudovaniyeExists(oborudovaniye.Id))
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

            var oborudovaniye = await _context.Oborudovaniye
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oborudovaniye == null)
            {
                return NotFound();
            }

            return View(oborudovaniye);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var oborudovaniye = await _context.Oborudovaniye.FindAsync(id);
            if (oborudovaniye != null)
            {
                _context.Oborudovaniye.Remove(oborudovaniye);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OborudovaniyeExists(int id)
        {
            return _context.Oborudovaniye.Any(e => e.Id == id);
        }
    }
}
