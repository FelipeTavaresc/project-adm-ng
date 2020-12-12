using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdmNerdGo.Data;
using AdmNerdGo.Models;
using AdmNerdGo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AdmNerdGo.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly AdmNerdGoContext _context;
        private readonly CategoriaServices _categoriaServices;

        public CategoriaController(AdmNerdGoContext context, CategoriaServices categoriaServices)
        {
            _context = context;
            _categoriaServices = categoriaServices;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Categoria.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewBag.Categorias = _categoriaServices.FindAll().Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categoria.FindAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            ViewBag.Categorias = _categoriaServices.FindAll().Select(a => new SelectListItem(a.Descricao, a.Id.ToString()));
            return View(categoria);
        }

        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao,Slug")] Categoria categoria)
        {
            if (id != categoria.Id)
            {
                return NotFound();  
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoria);
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}