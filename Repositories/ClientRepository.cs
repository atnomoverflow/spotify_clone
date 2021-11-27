using System.Collections.Generic;
using System.Threading.Tasks;
using Spotify_clone2.Models;
using Microsoft.EntityFrameworkCore;


namespace Spotify_clone2.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _context;
        public ClientRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<User> CreateAsync(User client)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
            return client;

        }

        public async Task DeleteAsync(User client)
        {
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<User> GetByCustomerIdAsync(string id)
        {
            return await _context.Clients.SingleOrDefaultAsync(x => x.client.CustomerId == id);
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await _context.Clients.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> UpdateAsync(User client)
        {
            _context.Clients.UpdateRange(client);
            await _context.SaveChangesAsync();
            return client;
        }
    }
}
