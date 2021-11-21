using System.Collections.Generic;
using System.Threading.Tasks;
using Spotify_clone2.Models;
using Microsoft.EntityFrameworkCore;

namespace Spotify_clone2.Repositories
{
    public class MembershipRepository : IMembershipRepository
    {
        private readonly AppDbContext _context;
        public MembershipRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Memebership> CreateAsync(Memebership memebership)
        {
            await _context.Memeberships.AddAsync(memebership);
            await _context.SaveChangesAsync();
            return memebership;

        }

        public async Task DeleteAsync(Memebership memebership)
        {
            _context.Memeberships.Remove(memebership);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Memebership>> GetAsync()
        {
            return await _context.Memeberships.ToListAsync();
        }

        public async Task<Memebership> GetByCustomerIdAsync(string id)
        {
            return await _context.Memeberships.SingleOrDefaultAsync(x => x.CustomerId == id);
        }

        public async Task<Memebership> GetByIdAsync(string id)
        {
            return await _context.Memeberships.SingleOrDefaultAsync(x=> x.Id==id);
        }

        public async Task<Memebership> UpdateAsync(Memebership memebership)
        {
            _context.Memeberships.UpdateRange(memebership);
            await _context.SaveChangesAsync();
            return memebership;
        }
    }
}