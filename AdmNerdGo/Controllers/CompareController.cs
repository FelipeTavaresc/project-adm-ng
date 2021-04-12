using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdmNerdGo.Models;
using AdmNerdGo.Models.ViewModels;
using AdmNerdGo.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdmNerdGo.Controllers
{
    public class CompareController : Controller
    {
        private readonly CompareServices _compareServices;
        private readonly ProdutoServices _produtoServices;
        private readonly LojaServices _lojaServices;

        public CompareController(CompareServices compareServices, ProdutoServices produtoServices, LojaServices lojaServices)
        {
            _compareServices = compareServices;
            _produtoServices = produtoServices;
            _lojaServices = lojaServices;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Comparacao(int? id)
        {
            var produto = _compareServices.FindProductByIdAsync(id.Value);
            var loja = await _compareServices.FindStoreByIdAsync(produto[0].Id);
            var comparacao = await _compareServices.ListComparationByProductId(id.Value);

            var viewModel = new CompareFormViewModel { Produtos = produto, Lojas = loja, ListCompare = comparacao };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            var lojas = await _lojaServices.FindAllAsync();
            var produto = await _produtoServices.FindByIdAsync(id);
            var viewModel = new CompareFormViewModel
            {
                Lojas = lojas,
                Produto = produto,
            };

            TempData["id-prod"] = produto.Id;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Compare compare)
        {
            if (!ModelState.IsValid)
            {
                var lojas = await _lojaServices.FindAllAsync();
                var produtos = await _produtoServices.FindAllAsync();
                var viewModel = new CompareFormViewModel
                {
                    Produtos = produtos,
                    Lojas = lojas
                };

                return View(viewModel);
            }

            await _compareServices.InsertAsync(compare);
            return RedirectToAction(nameof(Comparacao), new { id = compare.ProdutoId });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var comparacao = await _compareServices.FindComparationById(id);
            var loja = await _lojaServices.FindByIdListAsync(comparacao.LojaId);
            var produto = await _produtoServices.FindByIdAsync(comparacao.ProdutoId);
            var viewModel = new CompareFormViewModel { Compare = comparacao, Lojas = loja, Produto = produto };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Compare compare)
        {
            if (id != compare.Id)
            {
                return NotFound();
            }

            try
            {
                await _compareServices.UpdateAsync(compare);
                return RedirectToAction(nameof(Comparacao), new { id = compare.ProdutoId });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IActionResult> Delete(int id, int idproduto)
        {
            try
            {
                await _compareServices.DeleteAsync(id);
                return RedirectToAction(nameof(Comparacao), new { id = idproduto });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}