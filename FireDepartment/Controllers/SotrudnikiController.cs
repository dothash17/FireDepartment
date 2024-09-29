using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FireDepartment.Models;
using FireDepartment.Models.Data;
using Microsoft.Data.SqlClient;
using NuGet.DependencyResolver;
using System.Data;

namespace FireDepartment.Controllers
{
    public class SotrudnikiController : Controller
    {
        private readonly FireDepartmentDBContext _context;

        public SotrudnikiController(FireDepartmentDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Sotrudniki.ToListAsync());
        }

        public async Task<IActionResult> CalculateSotrudnikExperience(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var connection = _context.Database.GetDbConnection() as SqlConnection)
            {
                if (connection != null)
                {
                    await connection.OpenAsync();

                    using var command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "CalculateSotrudnikExperience";

                    var sotrudnikIdParam = new SqlParameter("@SotrudnikID", id);
                    command.Parameters.Add(sotrudnikIdParam);

                    using var reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        var experienceYears = reader.GetInt32(0);

                        return Json(new { success = true, experienceYears });
                    }
                }
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search(string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
            {
                return RedirectToAction("Index");
            }

            var query = _context.Sotrudniki.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(s =>
                    s.LastName.Contains(searchString) ||
                    s.FirstName.Contains(searchString));
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

            var sotrudniki = await _context.Sotrudniki
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sotrudniki == null)
            {
                return NotFound();
            }

            return View(sotrudniki);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Rank,Specialization,PhoneNumber,Mail,DateOfReceipt")] Sotrudniki sotrudniki)
        {
            try
            {
                _context.Add(sotrudniki);
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

            var sotrudniki = await _context.Sotrudniki.FindAsync(id);
            if (sotrudniki == null)
            {
                return NotFound();
            }
            return View(sotrudniki);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Rank,Specialization,PhoneNumber,Mail,DateOfReceipt")] Sotrudniki sotrudniki)
        {
            if (id != sotrudniki.Id)
            {
                return NotFound();
            }

            try
            {
                _context.Update(sotrudniki);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SotrudnikiExists(sotrudniki.Id))
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

            var sotrudniki = await _context.Sotrudniki
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sotrudniki == null)
            {
                return NotFound();
            }

            return View(sotrudniki);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sotrudniki = await _context.Sotrudniki.FindAsync(id);
            if (sotrudniki != null)
            {
                _context.Sotrudniki.Remove(sotrudniki);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SotrudnikiExists(int id)
        {
            return _context.Sotrudniki.Any(e => e.Id == id);
        }
    }
}
