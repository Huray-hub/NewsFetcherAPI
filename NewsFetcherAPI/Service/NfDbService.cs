using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NewsFetcherAPI.Data;

namespace NewsFetcherAPI.Service
{
    public class NfDbService : INfDbService
    {
        private readonly NfDbContext _context;

        public NfDbService(NfDbContext context) =>
            _context = context;

        public async Task<List<Category>> GetCategories()
        {
            try
            {
                return await _context.Categories.ToListAsync();
            }
            catch (Exception )
            {
                throw;
            }
        }
    }
}