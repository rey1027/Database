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
    public class InsumoProductoController : Controller
    {
        private readonly DetailTECContext _context;

        public InsumoProductoController(DetailTECContext context)
        {
            _context = context;
        }

        // GET: InsumoProducto
        public async Task<IActionResult> Index()
        {
              return View(await _context.InsumoProductos.ToListAsync());
        }

        // GET: InsumoProducto/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.InsumoProductos == null)
            {
                return NotFound();
            }

            var insumoProducto = await _context.InsumoProductos
                .FirstOrDefaultAsync(m => m.NombreIP == id);
            if (insumoProducto == null)
            {
                return NotFound();
            }

            return View(insumoProducto);
        }

        // GET: InsumoProducto/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InsumoProducto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NombreIP,Marca,Costo")] InsumoProducto insumoProducto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insumoProducto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(insumoProducto);
        }

        // GET: InsumoProducto/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.InsumoProductos == null)
            {
                return NotFound();
            }

            var insumoProducto = await _context.InsumoProductos.FindAsync(id);
            if (insumoProducto == null)
            {
                return NotFound();
            }
            return View(insumoProducto);
        }

        // POST: InsumoProducto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NombreIP,Marca,Costo")] InsumoProducto insumoProducto)
        {
            if (id != insumoProducto.NombreIP)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insumoProducto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsumoProductoExists(insumoProducto.NombreIP))
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
            return View(insumoProducto);
        }

        // GET: InsumoProducto/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.InsumoProductos == null)
            {
                return NotFound();
            }

            var insumoProducto = await _context.InsumoProductos
                .FirstOrDefaultAsync(m => m.NombreIP == id);
            if (insumoProducto == null)
            {
                return NotFound();
            }

            return View(insumoProducto);
        }

        // POST: InsumoProducto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.InsumoProductos == null)
            {
                return Problem("Entity set 'DetailTECContext.InsumoProductos'  is null.");
            }
            var insumoProducto = await _context.InsumoProductos.FindAsync(id);
            if (insumoProducto != null)
            {
                _context.InsumoProductos.Remove(insumoProducto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsumoProductoExists(string id)
        {
          return _context.InsumoProductos.Any(e => e.NombreIP == id);
        }
    }
}
