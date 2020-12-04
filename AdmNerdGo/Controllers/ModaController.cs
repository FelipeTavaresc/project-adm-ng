using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdmNerdGo.Models;
using AdmNerdGo.Models.ViewModels;
using AdmNerdGo.Services;
using AdmNerdGo.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdmNerdGo.Controllers
{
    public class ModaController : Controller
    {
        private readonly ProdutoServices _produtoServices;
        private readonly CompareServices _compareServices;
        private readonly CategoriaServices _categoriaServices;

        public ModaController(ProdutoServices produtoServices, CompareServices compareServices, CategoriaServices categoriaServices)
        {
            _produtoServices = produtoServices;
            _compareServices = compareServices;
            _categoriaServices = categoriaServices;
        }

        public IActionResult Index(int? pageNumber)
        {
            var list = _produtoServices.FindByCategoryId(5, pageNumber);
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
        public async Task<IActionResult> Edit(int id, Produto produto, List<IFormFile> Imagem)
        {
            if (id != produto.Id)
            {
                return NotFound();
            }

            produto.Imagem = Functions.ConvertImageToByte(Imagem);

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
                return RedirectToAction(nameof(Index));
            }

            _produtoServices.Delete(produtoId);
            return RedirectToAction(nameof(Index));
        }
    }
}