using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Administration.Data;
using Administration.Models;

namespace Administration.Controllers
{
    public class HtmlCoursController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HtmlCoursController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HtmlCours
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {
            var HtmlCours = from s in _context.HtmlCours
                           select s;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                HtmlCours = HtmlCours.Where(s => s.Nom.Contains(searchString)
                                       || s.Description.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    HtmlCours = HtmlCours.OrderByDescending(s => s.Nom);
                    break;
                case "Date":
                    HtmlCours = HtmlCours.OrderBy(s => s.DateAjout);
                    break;
                case "date_desc":
                    HtmlCours = HtmlCours.OrderByDescending(s => s.DateAjout);
                    break;
                default:
                    HtmlCours = HtmlCours.OrderBy(s => s.Nom);
                    break;
            }
            int pageSize = 6;
            return View(await PaginatedList<HtmlCours>.CreateAsync(HtmlCours.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: HtmlCours/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var htmlCours = await _context.HtmlCours
                .SingleOrDefaultAsync(m => m.Id == id);
            if (htmlCours == null)
            {
                return NotFound();
            }

            return View(htmlCours);
        }

        // GET: HtmlCours/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HtmlCours/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Description,UrlFiche,DateAjout")] HtmlCours htmlCours)
        {
            if (ModelState.IsValid)
            {
                htmlCours.DateAjout = DateTime.Now;
                _context.Add(htmlCours);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(htmlCours);
        }

        // GET: HtmlCours/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var htmlCours = await _context.HtmlCours.SingleOrDefaultAsync(m => m.Id == id);
            if (htmlCours == null)
            {
                return NotFound();
            }
            return View(htmlCours);
        }

        // POST: HtmlCours/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Description,UrlFiche,DateAjout")] HtmlCours htmlCours)
        {
            if (id != htmlCours.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(htmlCours);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HtmlCoursExists(htmlCours.Id))
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
            return View(htmlCours);
        }

        // GET: HtmlCours/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var htmlCours = await _context.HtmlCours
                .SingleOrDefaultAsync(m => m.Id == id);
            if (htmlCours == null)
            {
                return NotFound();
            }

            return View(htmlCours);
        }

        // POST: HtmlCours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var htmlCours = await _context.HtmlCours.SingleOrDefaultAsync(m => m.Id == id);
            _context.HtmlCours.Remove(htmlCours);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HtmlCoursExists(int id)
        {
            return _context.HtmlCours.Any(e => e.Id == id);
        }
    }
}
