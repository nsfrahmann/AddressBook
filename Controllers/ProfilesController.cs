using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AddressBook.Data;
using AddressBook.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using AddressBook.Utilities;
using Microsoft.AspNetCore.Identity;

namespace AddressBook.Controllers
{
    public class ProfilesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AddressUser> _userManager;

        public ProfilesController(ApplicationDbContext context, UserManager<AddressUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Profiles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Profile.ToListAsync());
        }

        // GET: Profiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profile = await _context.Profile
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profile == null)
            {
                return NotFound();
            }

            if (profile.Image != null)
            {
                ViewData["Image"] = ImageHelper.GetImage(profile);
            }

            return View(profile);
        }

        // GET: Profiles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Profiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,FirstName,LastName,Image,Email,Address1,Address2,City,State,ZipCode,Phone")] Profile profile, IFormFile image)
        {

            if (ModelState.IsValid)
            {                
                profile.DateAdded = DateTime.Now;

                if (image != null)
                {
                    profile.FileName = image.FileName;

                    var ms = new MemoryStream();
                    image.CopyTo(ms);
                    profile.Image = ms.ToArray();

                    ms.Close();
                    ms.Dispose();
                }

                _context.Add(profile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(profile);
        }

        // GET: Profiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profile = await _context.Profile.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }
            return View(profile);
        }

        // POST: Profiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,FirstName,LastName,FileName,Image,Email,Address1,Address2,City,State,ZipCode,Phone,DateAdded")] Profile profile, IFormFile newImage)
        {
            if (id != profile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (newImage != null)
                    {
                        profile.FileName = newImage.FileName;

                        var ms = new MemoryStream();
                        newImage.CopyTo(ms);
                        profile.Image = ms.ToArray();

                        ms.Close();
                        ms.Dispose();
                    }
                    _context.Update(profile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfileExists(profile.Id))
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
            return View(profile);
        }

        // GET: Profiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profile = await _context.Profile
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
        }

        // POST: Profiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profile = await _context.Profile.FindAsync(id);
            _context.Profile.Remove(profile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfileExists(int id)
        {
            return _context.Profile.Any(e => e.Id == id);
        }
    }
}
