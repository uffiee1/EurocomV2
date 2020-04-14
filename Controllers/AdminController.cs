using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EurocomV2.Models;

namespace EurocomV2.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin
        public async Task<IActionResult> Index()
        {
            return View(_context.AdminCRUD.ToList());
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminCRUD = await _context.AdminCRUD
                .FirstOrDefaultAsync(m => m.ID == id);
            if (adminCRUD == null)
            {
                return NotFound();
            }

            return View(adminCRUD);
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,Lastname,Specialty,Email")] AdminCRUD adminCRUD)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adminCRUD);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adminCRUD);
        }

        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminCRUD = await _context.AdminCRUD.FindAsync(id);
            if (adminCRUD == null)
            {
                return NotFound();
            }
            return View(adminCRUD);
        }

        // POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,Lastname,Specialty,Email")] AdminCRUD adminCRUD)
        {
            if (id != adminCRUD.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adminCRUD);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminCRUDExists(adminCRUD.ID))
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
            return View(adminCRUD);
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminCRUD = await _context.AdminCRUD
                .FirstOrDefaultAsync(m => m.ID == id);
            if (adminCRUD == null)
            {
                return NotFound();
            }

            return View(adminCRUD);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adminCRUD = await _context.AdminCRUD.FindAsync(id);
            _context.AdminCRUD.Remove(adminCRUD);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminCRUDExists(int id)
        {
            return _context.AdminCRUD.Any(e => e.ID == id);
        }
    }
}
