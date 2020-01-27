using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdmNerdGo.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public ICollection<Produto> Produtos { get; set; }

        public Categoria()
        {
        }

        public Categoria(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }
    }
}
