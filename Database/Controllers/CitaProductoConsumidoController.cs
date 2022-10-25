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
    public class CitaProductoConsumidoController : Controller
    {
        private readonly DetailTECContext _context;

        public CitaProductoConsumidoController(DetailTECContext context)
        {
            _context = context;
        }

        // GET: CitaProductoConsumido
        public async Task<IActionResult> Index()
        {
            var detailTECContext = _context.CitaProductoConsumidos.Include(c => c.Citum).Include(c => c.InsumoProducto);
            return View(await detailTECContext.ToListAsync());
        }

        // GET: CitaProductoConsumido/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CitaProductoConsumidos == null)
            {
                return NotFound();
            }

            var citaProductoConsumido = await _context.CitaProductoConsumidos
                .Include(c => c.Citum)
                .Include(c => c.InsumoProducto)
                .FirstOrDefaultAsync(m => m.Placa == id);
            if (citaProductoConsumido == null)
            {
                return NotFound();
            }

            return View(citaProductoConsumido);
        }

        // GET: CitaProductoConsumido/Create
        public IActionResult Create()
        {
            ViewData["Placa"] = new SelectList(_context.Cita, "Placa", "Sucursal");
            ViewData["NombreIP"] = new SelectList(_context.InsumoProductos, "NombreIP", "NombreIP");
            return View();
        }

        // POST: CitaProductoConsumido/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Placa,Fecha,Sucursal,NombreIP,Marca,Cantidad")] CitaProductoConsumido citaProductoConsumido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(citaProductoConsumido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Placa"] = new SelectList(_context.Cita, "Placa", "Sucursal", citaProductoConsumido.Placa);
            ViewData["NombreIP"] = new SelectList(_context.InsumoProductos, "NombreIP", "NombreIP", citaProductoConsumido.NombreIP);
            return View(citaProductoConsumido);
        }

        // GET: CitaProductoConsumido/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CitaProductoConsumidos == null)
            {
                return NotFound();
            }

            var citaProductoConsumido = await _context.CitaProductoConsumidos.FindAsync(id);
            if (citaProductoConsumido == null)
            {
                return NotFound();
            }
            ViewData["Placa"] = new SelectList(_context.Cita, "Placa", "Sucursal", citaProductoConsumido.Placa);
            ViewData["NombreIP"] = new SelectList(_context.InsumoProductos, "NombreIP", "NombreIP", citaProductoConsumido.NombreIP);
            return View(citaProductoConsumido);
        }

        // POST: CitaProductoConsumido/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Placa,Fecha,Sucursal,NombreIP,Marca,Cantidad")] CitaProductoConsumido citaProductoConsumido)
        {
            if (id != citaProductoConsumido.Placa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(citaProductoConsumido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitaProductoConsumidoExists(citaProductoConsumido.Placa))
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
            ViewData["Placa"] = new SelectList(_context.Cita, "Placa", "Sucursal", citaProductoConsumido.Placa);
            ViewData["NombreIP"] = new SelectList(_context.InsumoProductos, "NombreIP", "NombreIP", citaProductoConsumido.NombreIP);
            return View(citaProductoConsumido);
        }

        // GET: CitaProductoConsumido/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CitaProductoConsumidos == null)
            {
                return NotFound();
            }

            var citaProductoConsumido = await _context.CitaProductoConsumidos
                .Include(c => c.Citum)
                .Include(c => c.InsumoProducto)
                .FirstOrDefaultAsync(m => m.Placa == id);
            if (citaProductoConsumido == null)
            {
                return NotFound();
            }

            return View(citaProductoConsumido);
        }

        // POST: CitaProductoConsumido/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CitaProductoConsumidos == null)
            {
                return Problem("Entity set 'DetailTECContext.CitaProductoConsumidos'  is null.");
            }
            var citaProductoConsumido = await _context.CitaProductoConsumidos.FindAsync(id);
            if (citaProductoConsumido != null)
            {
                _context.CitaProductoConsumidos.Remove(citaProductoConsumido);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CitaProductoConsumidoExists(int id)
        {
          return _context.CitaProductoConsumidos.Any(e => e.Placa == id);
        }
    }
}
