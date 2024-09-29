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
    public class CallOborudovaniyeController : Controller
    {
        private readonly FireDepartmentDBContext _context;

        public CallOborudovaniyeController(FireDepartmentDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var fireDepartmentDBContext = _context.CallOborudovaniye.Include(c => c.Call).Include(c => c.Oborudovaniye);
            return View(await fireDepartmentDBContext.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["CallId"] = new SelectList(_context.Call, "Id", "Id");
            ViewData["OborudovaniyeId"] = new SelectList(_context.Oborudovaniye, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CallId,OborudovaniyeId")] CallOborudovaniye callOborudovaniye)
        {
            try
            {
                _context.Add(callOborudovaniye);
                await _context.SaveChangesAsync();
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

            var callOborudovaniye = await _context.CallOborudovaniye
                .Include(c => c.Call)
                .Include(c => c.Oborudovaniye)
                .FirstOrDefaultAsync(m => m.CallId == id);
            if (callOborudovaniye == null)
            {
                return NotFound();
            }

            return View(callOborudovaniye);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var callOborudovaniye = await _context.CallOborudovaniye.FindAsync(id);
            if (callOborudovaniye != null)
            {
                _context.CallOborudovaniye.Remove(callOborudovaniye);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CallOborudovaniyeExists(int id)
        {
            return _context.CallOborudovaniye.Any(e => e.CallId == id);
        }
    }
}
