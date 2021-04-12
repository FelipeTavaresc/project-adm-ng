using AdmNerdGo.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdmNerdGo.Library.Component
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly CategoriaServices _categoriaServices;

        public MenuViewComponent(CategoriaServices categoriaServices)
        {
            _categoriaServices = categoriaServices;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categoryList = _categoriaServices.FindAll();
            return View(categoryList);
        }
    }
}
