using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CognitiveServicesFace.Data;
using CognitiveServicesFace.Models;

namespace CognitiveServicesFace.Controllers
{
    public class EmotionsController : Controller
    {
        private readonly CognitiveServicesFaceContext _context;

        public EmotionsController(CognitiveServicesFaceContext context)
        {
            _context = context;
        }

        // GET: Emotions
        public async Task<IActionResult> Index()
        {
            var cognitiveServicesFaceContext = _context.Emotions.Include(e => e.Face);
            return View(await cognitiveServicesFaceContext.ToListAsync());
        }

        // GET: Emotions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emotionModel = await _context.Emotions
                .Include(e => e.Face)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emotionModel == null)
            {
                return NotFound();
            }

            return View(emotionModel);
        }

        // GET: Emotions/Create
        public IActionResult Create()
        {
            ViewData["FaceId"] = new SelectList(_context.Faces, "Id", "Id");
            return View();
        }

        // POST: Emotions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Score,FaceId,Type")] EmotionModel emotionModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(emotionModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FaceId"] = new SelectList(_context.Faces, "Id", "Id", emotionModel.FaceId);
            return View(emotionModel);
        }

        // GET: Emotions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emotionModel = await _context.Emotions.FindAsync(id);
            if (emotionModel == null)
            {
                return NotFound();
            }
            ViewData["FaceId"] = new SelectList(_context.Faces, "Id", "Id", emotionModel.FaceId);
            return View(emotionModel);
        }

        // POST: Emotions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Score,FaceId,Type")] EmotionModel emotionModel)
        {
            if (id != emotionModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emotionModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmotionModelExists(emotionModel.Id))
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
            ViewData["FaceId"] = new SelectList(_context.Faces, "Id", "Id", emotionModel.FaceId);
            return View(emotionModel);
        }

        // GET: Emotions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emotionModel = await _context.Emotions
                .Include(e => e.Face)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emotionModel == null)
            {
                return NotFound();
            }

            return View(emotionModel);
        }

        // POST: Emotions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emotionModel = await _context.Emotions.FindAsync(id);
            _context.Emotions.Remove(emotionModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmotionModelExists(int id)
        {
            return _context.Emotions.Any(e => e.Id == id);
        }
    }
}
