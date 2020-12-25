using AdmNerdGo.Data;
using AdmNerdGo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace AdmNerdGo.Services
{
    public class ProdutoServices
    {
        private readonly AdmNerdGoContext _context;
        private readonly CategoriaServices _categoriaServices;
        private const int ITEM_PER_PAGE = 12;

        public ProdutoServices(AdmNerdGoContext context, CategoriaServices categoriaServices)
        {
            _context = context;
            _categoriaServices = categoriaServices;
        }

        public async Task<List<Produto>> FindAllAsync()
        {
            return await _context.Produto.ToListAsync();
        }

        public IPagedList<Produto> FindAll(int? pageNumber)
        {
            int pageNumberAux = pageNumber ?? 1;
            return  _context.Produto.ToPagedList<Produto>(pageNumberAux, ITEM_PER_PAGE);
        }

        public async Task InsertAsync(Produto obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Produto>> FindByCategoryId(int id)
        {
            return await _context.Produto.Where(x => x.Categoria.Id == id).ToListAsync();
        }

        public IPagedList<Produto> FindByCategoryId(int id, int? pageNumber)
        {
            int pageNumberAux = pageNumber ?? 1;
            IList<Produto> produtos = new List<Produto>();
            var subCategoria = _categoriaServices.FindSubCategoriaByCategoriaPaiId(id);
             
            var prodCategoriaPai = _context.Produto.Where(x => x.Categoria.Id == id).ToList();
            foreach (var pcp in prodCategoriaPai)
            {
                produtos.Add(pcp);
            }

            if (subCategoria != null)
            {
                var prodSubCategoria = _context.Produto.Where(x => x.Categoria.Id == subCategoria.Id).ToList();
                foreach (var psc in prodSubCategoria)
                {
                    produtos.Add(psc);
                }
            }

            return produtos.ToPagedList<Produto>(pageNumberAux, ITEM_PER_PAGE);
        }

        public async Task<Produto> FindByIdAsync(int id)
        {
            return await _context.Produto.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Produto produto)
        {
            bool hasAny = await _context.Produto.AnyAsync(x => x.Id == produto.Id);

            if (!hasAny)
            {
                throw new Exception("Id not found");
            }

            try
            {
                _context.Update(produto);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var obj = _context.Produto.Find(id);
                _context.Produto.Remove(obj);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
