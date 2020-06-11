using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EurocomV2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EurocomV2.Controllers
{
    public class AdminNewController : Controller
    {
        private readonly AppDbContext _context;

        public AdminNewController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        // GET: Admin
        public async Task<IActionResult> DoktorIndex()
        {
            return View(_context.AdminCRUD.ToList());
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> DoktorDetails(int? id)
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
        public IActionResult DoktorCreate()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DoktorCreate([Bind("ID,FirstName,Lastname,Specialty,Email")] AdminCRUD adminCRUD)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adminCRUD);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(DoktorIndex));
            }
            return View(adminCRUD);
        }

        // GET: Admin/Edit/5
        public async Task<IActionResult> DoktorEdit(int? id)
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
        public async Task<IActionResult> DoktorEdit(int id, [Bind("ID,FirstName,Lastname,Specialty,Email")] AdminCRUD adminCRUD)
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
                return RedirectToAction(nameof(DoktorIndex));
            }
            return View(adminCRUD);
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> DoktorDelete(int? id)
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
        public async Task<IActionResult> DoktorDeleteConfirmed(int id)
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


        //////////////////////


        // GET: Users
        public async Task<IActionResult> PatientIndex()
        {
            return View(await _context.User.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> PatientDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult PatientCreate()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PatientCreate([Bind("UserId,FirstName,Lastname,Username,Email,Password,PhoneNumber,Agreement")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(PatientIndex));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> PatientEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PatientEdit(int id, [Bind("UserId,FirstName,Lastname,Username,Email,Password,PhoneNumber,Agreement")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(PatientIndex));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> PatientDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PatientDeleteConfirmed(int id)
        {
            var user = await _context.User.FindAsync(id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.UserId == id);
        }
    }
}