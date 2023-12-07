using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment5_GC.Data;
using Assignment5_GC.Models;

namespace Assignment5_GC.Controllers
{
    public class MusicsController : Controller
    {
        private readonly Assignment5_GCContext _context;

        public MusicsController(Assignment5_GCContext context)
        {
            _context = context;
        }


        // GET: Music
        public async Task<IActionResult> Index(string musicGenre, string musicPerformers)
        {
            if (_context.Music == null)
            {
                return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
            }

            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.Music
                                            orderby m.genre
                                            select m.genre;

            IQueryable<string> performersQuery = from m in _context.Music
                                                 orderby m.performer
                                                 select m.performer;

            var music = from m in _context.Music
                        select m;

            if (!string.IsNullOrEmpty(musicPerformers))
            {
                music = music.Where(x => x.performer == musicPerformers);
            }

            if (!string.IsNullOrEmpty(musicGenre))
            {
                music = music.Where(x => x.genre == musicGenre);
            }

            var MusicGenreVM = new MusicGenreViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Performers = new SelectList(await performersQuery.Distinct().ToListAsync()),
                Musics = await music.ToListAsync()
            };

            return View(MusicGenreVM);
        }

        public JsonResult GetPerformersByGenre(string musicGenre)
        {
            var performers = _context.Music
                .Where(x => x.genre == musicGenre)
                .OrderBy(x => x.performer)
                .Select(x => x.performer)
                .Distinct()
                .ToList();

            return Json(performers);
        }


        // GET: Musics
        /*
        public async Task<IActionResult> Index()
        {
            return View(await _context.Music.ToListAsync());
        }
        */
        // GET: Musics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var music = await _context.Music
                .FirstOrDefaultAsync(m => m.id == id);
            if (music == null)
            {
                return NotFound();
            }

            return View(music);
        }

        // GET: Musics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Musics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,title,purchase_type,performer,genre,year,price")] Music music)
        {
            if (ModelState.IsValid)
            {
                _context.Add(music);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(music);
        }

        // GET: Musics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var music = await _context.Music.FindAsync(id);
            if (music == null)
            {
                return NotFound();
            }
            return View(music);
        }

        // POST: Musics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,title,purchase_type,performer,genre,year,price")] Music music)
        {
            if (id != music.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(music);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MusicExists(music.id))
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
            return View(music);
        }

        // GET: Musics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var music = await _context.Music
                .FirstOrDefaultAsync(m => m.id == id);
            if (music == null)
            {
                return NotFound();
            }

            return View(music);
        }

        // POST: Musics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var music = await _context.Music.FindAsync(id);
            if (music != null)
            {
                _context.Music.Remove(music);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MusicExists(int id)
        {
            return _context.Music.Any(e => e.id == id);
        }
    }
}
