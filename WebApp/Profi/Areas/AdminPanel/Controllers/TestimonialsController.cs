using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Profi.DAL;
using Profi.Models;
using Profi.ViewModels;

namespace Profi.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class TestimonialsController : Controller
    {
        private readonly AppDbContext _context;

        public TestimonialsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Testimonials.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testimonials = await _context.Testimonials
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testimonials == null)
            {
                return NotFound();
            }

            return View(testimonials);
        }

        public IActionResult Create()
        {
            return View();
        }


 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateVM vm)
        {
            if (ModelState.IsValid)
            {
                Testimonials testimonials = new Testimonials
                {
                    Name=vm.Name,
                    Description=vm.Description,
                    Theme=vm.Theme,
                    Star=vm.StarCount
                };
                _context.Testimonials.Add(testimonials);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //if (testimonials.Url.Contains("/image"))
            //{
            //    ModelState.AddModelError("Photo", "Type of file must be image!");
            //}
            return View(vm);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testimonials = await _context.Testimonials.FindAsync(id);
            if (testimonials == null)
            {
                return NotFound();
            }
            return View(testimonials);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Testimonials testimonials)
        {
            if (id != testimonials.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testimonials);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestimonialsExists(testimonials.Id))
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
            return View(testimonials);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testimonials = await _context.Testimonials
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testimonials == null)
            {
                return NotFound();
            }

            return View(testimonials);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var testimonials = await _context.Testimonials.FindAsync(id);
            _context.Testimonials.Remove(testimonials);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestimonialsExists(int id)
        {
            return _context.Testimonials.Any(e => e.Id == id);
        }
    }
}
