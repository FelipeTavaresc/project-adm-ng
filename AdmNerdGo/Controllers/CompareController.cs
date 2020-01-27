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
            //var produto = await _compareServices.FindProductByIdAsync(1);
            //var loja = await _compareServices.FindStoreByIdAsync(produto[0].Id);
            //var comparacao = await _compareServices.ListComparationByProductId(1);

            //var viewModel = new CompareFormViewModel { Produtos = produto, Lojas = loja, Compare = comparacao };

            //return View(viewModel);
            return View();
        }

        public async Task<IActionResult> Create()
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
            return RedirectToAction(nameof(Index));
        }
    }
}