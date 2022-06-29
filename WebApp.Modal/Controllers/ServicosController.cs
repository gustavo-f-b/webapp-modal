using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Modal.Models;
using WebApp.Modal.ViewModels;

namespace WebApp.Modal.Controllers
{
    public class ServicosController : Controller
    {
        private readonly testeContext _context;

        public ServicosController(testeContext context)
        {
            _context = context;
        }

        // GET: Servicos
        public async Task<IActionResult> Index()
        {
              return _context.ServicoViewModel != null ? 
                          View(await _context.ServicoViewModel.ToListAsync()) :
                          Problem("Entity set 'testeContext.ServicoViewModel'  is null.");
        }

        // GET: Servicos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ServicoViewModel == null)
            {
                return NotFound();
            }

            var servicoViewModel = await _context.ServicoViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (servicoViewModel == null)
            {
                return NotFound();
            }

            return View(servicoViewModel);
        }

        // GET: Servicos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Servicos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descricao")] ServicoViewModel servicoViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(servicoViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(servicoViewModel);
        }

        // GET: Servicos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ServicoViewModel == null)
            {
                return NotFound();
            }

            var servicoViewModel = await _context.ServicoViewModel.FindAsync(id);
            if (servicoViewModel == null)
            {
                return NotFound();
            }
            return View(servicoViewModel);
        }

        // POST: Servicos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao")] ServicoViewModel servicoViewModel)
        {
            if (id != servicoViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(servicoViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicoViewModelExists(servicoViewModel.Id))
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
            return View(servicoViewModel);
        }

        // GET: Servicos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ServicoViewModel == null)
            {
                return NotFound();
            }

            var servicoViewModel = await _context.ServicoViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (servicoViewModel == null)
            {
                return NotFound();
            }

            return View(servicoViewModel);
        }

        // POST: Servicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ServicoViewModel == null)
            {
                return Problem("Entity set 'testeContext.ServicoViewModel'  is null.");
            }
            var servicoViewModel = await _context.ServicoViewModel.FindAsync(id);
            if (servicoViewModel != null)
            {
                _context.ServicoViewModel.Remove(servicoViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServicoViewModelExists(int id)
        {
          return (_context.ServicoViewModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
