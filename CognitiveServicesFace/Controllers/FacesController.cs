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
    public class FacesController : Controller
    {
        private readonly CognitiveServicesFaceContext _context;

        public FacesController(CognitiveServicesFaceContext context)
        {
            _context = context;
        }

        // GET: Faces
        public async Task<IActionResult> Index()
        {
            var cognitiveServicesFaceContext = _context.Faces.Include(f => f.Picture);
            return View(await cognitiveServicesFaceContext.ToListAsync());
        }

        // GET: Faces/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faceModel = await _context.Faces
                .Include(f => f.Picture)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (faceModel == null)
            {
                return NotFound();
            }

            return View(faceModel);
        }

        // GET: Faces/Create
        public IActionResult Create()
        {
            ViewData["PictureId"] = new SelectList(_context.Pictures, "Id", "Id");
            return View();
        }

        // POST: Faces/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PictureId,Top,Left,Width,Height")] FaceModel faceModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(faceModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PictureId"] = new SelectList(_context.Pictures, "Id", "Id", faceModel.PictureId);
            return View(faceModel);
        }

        // GET: Faces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faceModel = await _context.Faces.FindAsync(id);
            if (faceModel == null)
            {
                return NotFound();
            }
            ViewData["PictureId"] = new SelectList(_context.Pictures, "Id", "Id", faceModel.PictureId);
            return View(faceModel);
        }

        // POST: Faces/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PictureId,Top,Left,Width,Height")] FaceModel faceModel)
        {
            if (id != faceModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(faceModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FaceModelExists(faceModel.Id))
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
            ViewData["PictureId"] = new SelectList(_context.Pictures, "Id", "Id", faceModel.PictureId);
            return View(faceModel);
        }

        // GET: Faces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faceModel = await _context.Faces
                .Include(f => f.Picture)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (faceModel == null)
            {
                return NotFound();
            }

            return View(faceModel);
        }

        // POST: Faces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var faceModel = await _context.Faces.FindAsync(id);
            _context.Faces.Remove(faceModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FaceModelExists(int id)
        {
            return _context.Faces.Any(e => e.Id == id);
        }
    }
}
