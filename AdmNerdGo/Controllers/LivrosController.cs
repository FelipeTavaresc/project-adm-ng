using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdmNerdGo.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdmNerdGo.Controllers
{
    public class LivrosController : Controller
    {
        private readonly ProdutoServices _produtoServices;

        public LivrosController(ProdutoServices produtoServices)
        {
            _produtoServices = produtoServices;
        }

        public IActionResult Index(int? pageNumber)
        {
            var list = _produtoServices.FindByCategoryId(4, pageNumber);
            return View(list);
        }
    }
}