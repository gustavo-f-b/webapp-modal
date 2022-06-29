using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Modal.Models;
using WebApp.Modal.ViewModels;

namespace WebApp.Modal.Controllers
{
    public class ServicosClientesController : Controller
    {
        private readonly testeContext _context;
        private readonly IMapper _mapper;

        public ServicosClientesController(testeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: ServicosClientes
        public async Task<IActionResult> Index()
        {
            var testeContext = _mapper.Map<ServicosClientesViewModel>(await _context.ServicosClientes.Include(s => s.Cliente).Include(s => s.Servico).ToListAsync());
            
            return View(testeContext);
        }

        // GET: ServicosClientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ServicosClientesViewModel == null)
            {
                return NotFound();
            }

            var servicosClientesViewModel = await _context.ServicosClientesViewModel
                .Include(s => s.Cliente)
                .Include(s => s.Servico)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (servicosClientesViewModel == null)
            {
                return NotFound();
            }

            return View(servicosClientesViewModel);
        }

        // GET: ServicosClientes/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.ClienteViewModel, "Id", "Nome");
            ViewData["ServicoId"] = new SelectList(_context.Set<ServicoViewModel>(), "Id", "Descricao");
            return View();
        }

        // POST: ServicosClientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClienteId,ServicoId")] ServicosClientesViewModel servicosClientesViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(servicosClientesViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.ClienteViewModel, "Id", "Id", servicosClientesViewModel.ClienteId);
            ViewData["ServicoId"] = new SelectList(_context.Set<ServicoViewModel>(), "Id", "Id", servicosClientesViewModel.ServicoId);
            return View(servicosClientesViewModel);
        }

        // GET: ServicosClientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ServicosClientesViewModel == null)
            {
                return NotFound();
            }

            var servicosClientesViewModel = _mapper.Map<ServicosClientesViewModel>(await _context.ServicosClientes.Include(c => c.Cliente).FirstAsync(e => e.Id == id));

            if (servicosClientesViewModel == null)
                return NotFound();

            //ViewData["ClienteId"] = new SelectList(_context.Set<ClienteViewModel>(), "Id", "Nome", servicosClientesViewModel.ClienteId);
            ViewData["ServicoId"] = new SelectList(_mapper.Map<IEnumerable<ServicoViewModel>>(_context.Set<Servico>()), "Id", "Descricao", servicosClientesViewModel.ServicoId);
            return View(servicosClientesViewModel);
        }

        // POST: ServicosClientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClienteId,ServicoId")] ServicosClientesViewModel servicosClientesViewModel)
        {
            if (id != servicosClientesViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(_mapper.Map<ServicosCliente>(servicosClientesViewModel));
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicosClientesViewModelExists(servicosClientesViewModel.Id))
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
            ViewData["ClienteId"] = new SelectList(_context.ClienteViewModel, "Id", "Nome", servicosClientesViewModel.ClienteId);
            ViewData["ServicoId"] = new SelectList(_context.Set<ServicoViewModel>(), "Id", "Descricao", servicosClientesViewModel.ServicoId);
            return View(servicosClientesViewModel);
        }

        // GET: ServicosClientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ServicosClientesViewModel == null)
            {
                return NotFound();
            }

            var servicosClientesViewModel = await _context.ServicosClientesViewModel
                .Include(s => s.Cliente)
                .Include(s => s.Servico)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (servicosClientesViewModel == null)
            {
                return NotFound();
            }

            return View(servicosClientesViewModel);
        }

        // POST: ServicosClientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ServicosClientesViewModel == null)
            {
                return Problem("Entity set 'testeContext.ServicosClientesViewModel'  is null.");
            }
            var servicosClientesViewModel = await _context.ServicosClientesViewModel.FindAsync(id);
            if (servicosClientesViewModel != null)
            {
                _context.ServicosClientesViewModel.Remove(servicosClientesViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServicosClientesViewModelExists(int id)
        {
          return (_context.ServicosClientesViewModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
