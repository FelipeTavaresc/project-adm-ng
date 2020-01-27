using AdmNerdGo.Data;
using AdmNerdGo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdmNerdGo.Services
{
    public class LojaServices
    {
        private readonly AdmNerdGoContext _context;

        public LojaServices(AdmNerdGoContext context)
        {
            _context = context;
        }

        public async Task<List<Loja>> FindAllAsync()
        {
            return await _context.Loja.ToListAsync();
        }

        public async Task InsertAsync(Loja obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Loja obj)
        {
            bool hasAny = await _context.Loja.AnyAsync(x => x.Id == obj.Id);

            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<Loja>> FindByIdAsync(int id)
        {
            return await _context.Loja.Where(x => x.Id == id).ToListAsync();
        }
    }
}
