using AdmNerdGo.Data;
using AdmNerdGo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdmNerdGo.Services
{
    public class ProdutoServices
    {
        private readonly AdmNerdGoContext _context;

        public ProdutoServices(AdmNerdGoContext context)
        {
            _context = context;
        }

        public async Task<List<Produto>> FindAllAsync()
        {
            return await _context.Produto.ToListAsync();
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
