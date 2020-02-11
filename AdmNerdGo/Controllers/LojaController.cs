using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AdmNerdGo.Models;
using AdmNerdGo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdmNerdGo.Controllers
{
    public class LojaController : Controller
    {
        private readonly LojaServices _lojaServices;

        public LojaController(LojaServices lojaServices)
        {
            _lojaServices = lojaServices;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _lojaServices.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Loja cliente, List<IFormFile> Logo)
        {
            foreach (var item in Logo)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        cliente.Logo = stream.ToArray();
                    }
                }
            }

            if (!ModelState.IsValid)
            {
                //var categorias = await _lojaServices.FindAllAsync();
                //var viewModel = new ProdutoFormViewModel { Produto = produto, Categorias = categorias };
                //return View(viewModel);
            }

            await _lojaServices.InsertAsync(cliente);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var obj = await _lojaServices.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return View("Loja não encontrada");
            }
            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Loja loja, List<IFormFile> Logo)
        {
            foreach (var item in Logo)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        loja.Logo = stream.ToArray();
                    }
                }
            }

            try
            {
                await _lojaServices.UpdateAsync(loja);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}