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
    public class ClienteViewModelsController : Controller
    {
        private readonly testeContext _context;
        private readonly IMapper _mapper;

        public ClienteViewModelsController(testeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper; 
        }

        // GET: ClienteViewModels
        public async Task<IActionResult> Index()
        {

            return View(_mapper.Map<IEnumerable<ClienteViewModel>>(await _context.Clientes.ToListAsync()));
        }

        // GET: ClienteViewModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ClienteViewModel == null)
            {
                return NotFound();
            }

            var clienteViewModel = await _context.ClienteViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clienteViewModel == null)
            {
                return NotFound();
            }

            return View(clienteViewModel);
        }

        // GET: ClienteViewModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClienteViewModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] ClienteViewModel clienteViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clienteViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clienteViewModel);
        }

        // GET: ClienteViewModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ClienteViewModel == null)
            {
                return NotFound();
            }

            var clienteViewModel = _mapper.Map<ClienteViewModel>(await _context.Clientes.FindAsync(id));

            clienteViewModel.ServicosClientes = _mapper.Map<IEnumerable<ServicosClientesViewModel>>(await _context.ServicosClientes.Where(s => s.ClienteId == id).Include(s => s.Servico).ToListAsync());

            if (clienteViewModel == null)
                return NotFound();

            return View(clienteViewModel);
        }

        // POST: ClienteViewModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] ClienteViewModel clienteViewModel)
        {
            if (id != clienteViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(_mapper.Map<Cliente>(clienteViewModel));
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteViewModelExists(clienteViewModel.Id))
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
            return View(clienteViewModel);
        }

        // GET: ClienteViewModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ClienteViewModel == null)
            {
                return NotFound();
            }

            var clienteViewModel = await _context.ClienteViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clienteViewModel == null)
            {
                return NotFound();
            }

            return View(clienteViewModel);
        }

        // POST: ClienteViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ClienteViewModel == null)
            {
                return Problem("Entity set 'testeContext.ClienteViewModel'  is null.");
            }
            var clienteViewModel = await _context.ClienteViewModel.FindAsync(id);
            if (clienteViewModel != null)
            {
                _context.ClienteViewModel.Remove(clienteViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteViewModelExists(int id)
        {
          return (_context.ClienteViewModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
