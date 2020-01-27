using AdmNerdGo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdmNerdGo.Data
{
    public class AdmNerdGoContext : DbContext
    {
        public AdmNerdGoContext(DbContextOptions<AdmNerdGoContext> options)
            :base(options)
        {
        }

        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Loja> Loja { get; set; }
        public DbSet<Compare> Compare { get; set; }
    }
}
