using AdmNerdGo.Data;
using AdmNerdGo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdmNerdGo.Services
{
    public class CategoriaServices
    {
        private readonly AdmNerdGoContext _context;

        public CategoriaServices(AdmNerdGoContext context)
        {
            _context = context;
        }

        public async Task<List<Categoria>> FindAllAsync()
        {
            return await _context.Categoria.OrderBy(x => x.Descricao).ToListAsync();
        }

        public List<Categoria> FindAll()
        {
            return _context.Categoria.OrderBy(x => x.Id).ToList();
        }

        public Categoria FindCategoriaById(int id)
        {
            return _context.Categoria.Where(x => x.Id == id).FirstOrDefault();
        }

        public List<Categoria> FindSubCategoriaByCategoriaPaiId(int id)
        {
            return _context.Categoria.Where(x => x.CategoriaPaiId == id).ToList();
        }

        public void Delete(int Id)
        {
            Categoria categoria = FindCategoriaById(Id);
            _context.Remove(categoria);
            _context.SaveChanges();
        }
    }
}
