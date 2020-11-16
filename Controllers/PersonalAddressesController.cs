using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AddressBook.Data;
using AddressBook.Models;
using AddressBook.Utilities;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace AddressBook.Controllers
{
    public class PersonalAddressesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonalAddressesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PersonalAddresses
        public async Task<IActionResult> Index(PersonalAddress personalAddress)
        {


                return View(await _context.PersonalAddresses.ToListAsync());
        }

        // GET: PersonalAddresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalAddresses = await _context.PersonalAddresses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personalAddresses == null)
            {
                return NotFound();
            }

            if (personalAddresses.Image != null)
            {
                ViewData["Image"] = ImageHelper.GetImage(personalAddresses);
            }

            return View(personalAddresses);
        }

        // GET: PersonalAddresses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PersonalAddresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Image,Email,Address1,Address2,City,State,ZipCode,Phone")] PersonalAddress personalAddresses, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                personalAddresses.DateAdded = DateTime.Now;

                if (image != null)
                {
                    var ms = new MemoryStream();
                    image.CopyTo(ms);
                    byte[] bytes = ms.ToArray();
                    ms.Close();
                    ms.Dispose();
                    var binary = Convert.ToBase64String(bytes);
                    var ext = Path.GetExtension(image.FileName);
                    personalAddresses.FileName = $"data:image/{ext};base64,{binary}";
                    personalAddresses.Image = bytes;
                }
                    _context.Add(personalAddresses);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                
            }
            return View(personalAddresses);
        }

        // GET: PersonalAddresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalAddresses = await _context.PersonalAddresses.FindAsync(id);
            if (personalAddresses == null)
            {
                return NotFound();
            }
            return View(personalAddresses);
        }

        // POST: PersonalAddresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Image,Email,Address1,Address2,City,State,ZipCode,Phone,DateAdded")] PersonalAddress personalAddresses, IFormFile newImage)
        {
            if (id != personalAddresses.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (newImage != null)
                    {
                        personalAddresses.FileName = newImage.FileName;

                        var ms = new MemoryStream();
                        newImage.CopyTo(ms);
                        personalAddresses.Image = ms.ToArray();

                        ms.Close();
                        ms.Dispose();
                    }

                    _context.Update(personalAddresses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonalAddressesExists(personalAddresses.Id))
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
            return View(personalAddresses);
        }

        // GET: PersonalAddresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalAddresses = await _context.PersonalAddresses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personalAddresses == null)
            {
                return NotFound();
            }

            return View(personalAddresses);
        }

        // POST: PersonalAddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personalAddresses = await _context.PersonalAddresses.FindAsync(id);
            _context.PersonalAddresses.Remove(personalAddresses);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonalAddressesExists(int id)
        {
            return _context.PersonalAddresses.Any(e => e.Id == id);
        }
    }
}
