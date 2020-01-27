using AdmNerdGo.Data;
using AdmNerdGo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdmNerdGo.Services
{
    public class CompareServices
    {
        private readonly AdmNerdGoContext _context;

        public CompareServices(AdmNerdGoContext context)
        {
            _context = context;
        }

        public async Task<List<Compare>> FindAllAsync()
        {
            return await _context.Compare.ToListAsync();
        }

        public async Task<List<Compare>> ListComparationByProductId(int productId)
        {
            var result = from obj in _context.Compare select obj;

            result = result.Where(x => x.ProdutoId == productId);

            return await result.ToListAsync();
        }

        public async Task<List<Produto>> FindProductByIdAsync(int id)
        {
            var result = from obj in _context.Produto select obj;

            result = result.Where(x => x.Id == id);

            return await result.ToListAsync();
        }

        public async Task<List<Loja>> FindStoreByIdAsync(int productId)
        {
            return await _context.Compare.Where(obj => obj.ProdutoId == productId).Select(obj => obj.Loja).ToListAsync();
        }

        public async Task InsertAsync(Compare obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }
    }
}
