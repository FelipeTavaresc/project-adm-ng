using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AdmNerdGo.Data;
using AdmNerdGo.Models;
using AdmNerdGo.Models.ViewModels;
using AdmNerdGo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdmNerdGo.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ProdutoServices _produtoServices;
        private readonly CategoriaServices _categoriaServices;
        private readonly CompareServices _compareServices;

        public ProdutoController(ProdutoServices produtoServices, CategoriaServices categoriaServices, CompareServices compareServices)
        {
            _produtoServices = produtoServices;
            _categoriaServices = categoriaServices;
            _compareServices = compareServices;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _produtoServices.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var categorias = await _categoriaServices.FindAllAsync();
            var viewModel = new ProdutoFormViewModel { Categorias = categorias };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Produto produto, List<IFormFile> Imagem)
        {
            foreach (var item in Imagem)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        produto.Imagem = stream.ToArray();
                    }
                }
            }

            if (!ModelState.IsValid)
            {
                var categorias = await _categoriaServices.FindAllAsync();
                var viewModel = new ProdutoFormViewModel { Produto = produto, Categorias = categorias };
                return View(viewModel);
            }

            await _produtoServices.InsertAsync(produto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Categoria(int id)
        {
            var list = await _produtoServices.FindByCategoryId(id);
            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _produtoServices.FindByIdAsync(id.Value);
            var categorias = await _categoriaServices.FindAllAsync();
            var img = produto.Imagem;
            if (produto == null)
            {
                return NotFound();
            }

            ProdutoFormViewModel viewModel = new ProdutoFormViewModel { Produto = produto, Categorias = categorias, Imagem = img };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Produto produto)
        {
            if (id != produto.Id)
            {
                return NotFound();
            }

            try
            {
                await _produtoServices.UpdateAsync(produto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IActionResult Delete(int produtoId, int categoriaId)
        {
            var comparacao = _compareServices.FindComparationByProductId(produtoId);
            if (comparacao.Count > 0)
            {
                ViewData["MSG_E"] = "Este produto possui comparações";
                return RedirectToAction(nameof(Categoria), new { id = categoriaId });
            }

            _produtoServices.Delete(produtoId);
            return RedirectToAction(nameof(Categoria), new { id = categoriaId });
        }

    }
}