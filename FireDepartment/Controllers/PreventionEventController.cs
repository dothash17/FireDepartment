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
    public class PreventionEventController : Controller
    {
        private readonly FireDepartmentDBContext _context;

        public PreventionEventController(FireDepartmentDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var fireDepartmentDBContext = _context.PreventionEvent.Include(p => p.Sotrudnik);
            return View(await fireDepartmentDBContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preventionEvent = await _context.PreventionEvent
                .Include(p => p.Sotrudnik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (preventionEvent == null)
            {
                return NotFound();
            }

            return View(preventionEvent);
        }

        public IActionResult Create()
        {
            ViewData["SotrudnikId"] = new SelectList(_context.Sotrudniki, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DateTime,Location,Goal,SotrudnikId")] PreventionEvent preventionEvent)
        {
            try
            {
                _context.Add(preventionEvent);
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

            var preventionEvent = await _context.PreventionEvent.FindAsync(id);
            if (preventionEvent == null)
            {
                return NotFound();
            }
            ViewData["SotrudnikId"] = new SelectList(_context.Sotrudniki, "Id", "Id", preventionEvent.SotrudnikId);
            return View(preventionEvent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DateTime,Location,Goal,SotrudnikId")] PreventionEvent preventionEvent)
        {
            if (id != preventionEvent.Id)
            {
                return NotFound();
            }

            try
            {
                _context.Update(preventionEvent);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PreventionEventExists(preventionEvent.Id))
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

            var preventionEvent = await _context.PreventionEvent
                .Include(p => p.Sotrudnik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (preventionEvent == null)
            {
                return NotFound();
            }

            return View(preventionEvent);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var preventionEvent = await _context.PreventionEvent.FindAsync(id);
            if (preventionEvent != null)
            {
                _context.PreventionEvent.Remove(preventionEvent);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PreventionEventExists(int id)
        {
            return _context.PreventionEvent.Any(e => e.Id == id);
        }
    }
}
