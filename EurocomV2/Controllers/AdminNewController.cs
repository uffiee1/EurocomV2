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


        // GET: Doctor
        public async Task<IActionResult> DoktorIndex()
        {
            return View(_context.Doctor.ToList());
        }

        // GET: Doctor/Details/5
        public async Task<IActionResult> DoktorDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Doctor = await _context.Doctor
                .FirstOrDefaultAsync(m => m.ID == id);
            if (Doctor == null)
            {
                return NotFound();
            }

            return View(Doctor);
        }

        // GET: Doctor/Create
        public IActionResult DoktorCreate()
        {
            return View();
        }

        // POST: Doctor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DoktorCreate([Bind("ID,FirstName,Lastname,Specialty,Email")] Doctor Doctor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Doctor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(DoktorIndex));
            }
            return View(Doctor);
        }

        // GET: Doctor/Edit/5
        public async Task<IActionResult> DoktorEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Doctor = await _context.Doctor.FindAsync(id);
            if (Doctor == null)
            {
                return NotFound();
            }
            return View(Doctor);
        }

        // POST: Doctor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DoktorEdit(int id, [Bind("ID,FirstName,Lastname,Specialty,Email")] Doctor Doctor)
        {
            if (id != Doctor.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Doctor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorExists(Doctor.ID))
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
            return View(Doctor);
        }

        // GET: Doctor/Delete/5
        public async Task<IActionResult> DoktorDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Doctor = await _context.Doctor
                .FirstOrDefaultAsync(m => m.ID == id);
            if (Doctor == null)
            {
                return NotFound();
            }

            return View(Doctor);
        }

        // POST: Doctor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DoktorDeleteConfirmed(int id)
        {
            var Doctor = await _context.Doctor.FindAsync(id);
            _context.Doctor.Remove(Doctor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorExists(int id)
        {
            return _context.Doctor.Any(e => e.ID == id);
        }


        //////////////////////


        // GET: Users
        public async Task<IActionResult> PatientIndex()
        {
            return View(await _context.Users.ToListAsync());
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