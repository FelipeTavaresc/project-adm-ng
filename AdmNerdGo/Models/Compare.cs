using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdmNerdGo.Models
{
    public class Compare
    {
        public int Id { get; set; }
        public Loja Loja { get; set; }
        public Produto Produto { get; set; }
        public double Preco { get; set; }
        public string Parcelamento { get; set; }
        public string Link { get; set; }
        public int LojaId { get; set; }
        public int ProdutoId { get; set; }

        public Compare()
        {

        }
    }
}
