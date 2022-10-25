using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Database.Models;

namespace Database.Controllers
{
    public class ClienteDireccionController : Controller
    {
        private readonly DetailTECContext _context;

        public ClienteDireccionController(DetailTECContext context)
        {
            _context = context;
        }

        // GET: ClienteDireccion
        public async Task<IActionResult> Index()
        {
            var detailTECContext = _context.ClienteDireccions.Include(c => c.CedulaNavigation);
            return View(await detailTECContext.ToListAsync());
        }

        // GET: ClienteDireccion/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.ClienteDireccions == null)
            {
                return NotFound();
            }

            var clienteDireccion = await _context.ClienteDireccions
                .Include(c => c.CedulaNavigation)
                .FirstOrDefaultAsync(m => m.Cedula == id);
            if (clienteDireccion == null)
            {
                return NotFound();
            }

            return View(clienteDireccion);
        }

        // GET: ClienteDireccion/Create
        public IActionResult Create()
        {
            ViewData["Cedula"] = new SelectList(_context.Clientes, "Cedula", "Cedula");
            return View();
        }

        // POST: ClienteDireccion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cedula,Direccion")] ClienteDireccion clienteDireccion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clienteDireccion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Cedula"] = new SelectList(_context.Clientes, "Cedula", "Cedula", clienteDireccion.Cedula);
            return View(clienteDireccion);
        }

        // GET: ClienteDireccion/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.ClienteDireccions == null)
            {
                return NotFound();
            }

            var clienteDireccion = await _context.ClienteDireccions.FindAsync(id);
            if (clienteDireccion == null)
            {
                return NotFound();
            }
            ViewData["Cedula"] = new SelectList(_context.Clientes, "Cedula", "Cedula", clienteDireccion.Cedula);
            return View(clienteDireccion);
        }

        // POST: ClienteDireccion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Cedula,Direccion")] ClienteDireccion clienteDireccion)
        {
            if (id != clienteDireccion.Cedula)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clienteDireccion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteDireccionExists(clienteDireccion.Cedula))
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
            ViewData["Cedula"] = new SelectList(_context.Clientes, "Cedula", "Cedula", clienteDireccion.Cedula);
            return View(clienteDireccion);
        }

        // GET: ClienteDireccion/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.ClienteDireccions == null)
            {
                return NotFound();
            }

            var clienteDireccion = await _context.ClienteDireccions
                .Include(c => c.CedulaNavigation)
                .FirstOrDefaultAsync(m => m.Cedula == id);
            if (clienteDireccion == null)
            {
                return NotFound();
            }

            return View(clienteDireccion);
        }

        // POST: ClienteDireccion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.ClienteDireccions == null)
            {
                return Problem("Entity set 'DetailTECContext.ClienteDireccions'  is null.");
            }
            var clienteDireccion = await _context.ClienteDireccions.FindAsync(id);
            if (clienteDireccion != null)
            {
                _context.ClienteDireccions.Remove(clienteDireccion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteDireccionExists(string id)
        {
          return _context.ClienteDireccions.Any(e => e.Cedula == id);
        }
    }
}
