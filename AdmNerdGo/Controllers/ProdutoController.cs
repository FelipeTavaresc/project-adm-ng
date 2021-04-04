using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AdmNerdGo.Data;
using AdmNerdGo.Models;
using AdmNerdGo.Models.ViewModels;
using AdmNerdGo.Services;
using AdmNerdGo.Util;
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
            if (!ModelState.IsValid)
            {
                var categorias = await _categoriaServices.FindAllAsync();
                var viewModel = new ProdutoFormViewModel { Produto = produto, Categorias = categorias };
                return View(viewModel);
            }

            var prodId = await _produtoServices.InsertAndReturnIdAsync(produto);

            if (Imagem.Count > 0)
            {
                var image = Functions.ConvertImageToByte(Imagem);
                var slug = AdmNerdGo.Library.Util.GenerateSlug(produto.Descricao);
                var imagePath = Functions.SaveImageInDirectory(image, prodId.ToString(), slug);
                var imageName = prodId.ToString() + "-" + slug + ".jpg";
                Functions.UploadImageToFtp(imagePath, imageName);
            }

            var desCategoria = _categoriaServices.FindCategoriaById(produto.CategoriaId).Descricao;

            //return RedirectToAction(nameof(Index));
            var categoria = Functions.RemoveDiacritics(desCategoria);
            return RedirectToAction("Index", categoria);
        }

        public IActionResult Categoria(int id, int? pageNumber)
        {
            var list = _produtoServices.FindByCategoryId(id, pageNumber);
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