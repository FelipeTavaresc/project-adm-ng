using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdmNerdGo.Models.ViewModels
{
    public class CompareFormViewModel
    {
        public Compare Compare { get; set; }
        public Produto Produto { get; set; }
        public ICollection<Produto> Produtos { get; set; }
        public ICollection<Loja> Lojas { get; set; }
        public ICollection<Compare> ListCompare { get; set; }
    }
}
