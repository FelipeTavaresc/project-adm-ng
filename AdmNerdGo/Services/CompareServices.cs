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

        public List<Compare> FindComparationByProductId(int productId)
        {
            var result = from obj in _context.Compare select obj;

            result = result.Where(x => x.ProdutoId == productId);

            return result.ToList();
        }

        public List<Produto> FindProductByIdAsync(int id)
        {
            var result = from obj in _context.Produto select obj;

            result = result.Where(x => x.Id == id);

            return result.ToList();
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

        public async Task<Compare> FindComparationById(int comparationId)
        {
            return await _context.Compare.Where(x => x.Id == comparationId).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Compare compare)
        {
            bool hasAny = await _context.Compare.AnyAsync(x => x.Id == compare.Id);

            if (!hasAny)
            {
                throw new Exception("Id not found");
            }

            try
            {
                _context.Update(compare);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var obj = await _context.Compare.FindAsync(id);
                _context.Compare.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception (e.Message);
            }
        }
    }
}
