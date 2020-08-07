using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdmNerdGo.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdmNerdGo.Controllers
{
    public class ColecionaveisController : Controller
    {
        private readonly ProdutoServices _produtoServices;

        public ColecionaveisController(ProdutoServices produtoServices)
        {
            _produtoServices = produtoServices;
        }

        public IActionResult Index(int? pageNumber)
        {
            var list = _produtoServices.FindByCategoryId(1, pageNumber);
            return View(list);
        }
    }
}