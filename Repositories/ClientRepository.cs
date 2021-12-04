using System.Collections.Generic;
using System.Threading.Tasks;
using Spotify_clone2.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;


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
            await _context.Clients.AddAsync(client.client);
            await _context.SaveChangesAsync();
            return client;

        }

        public async Task DeleteAsync(User client)
        {
            _context.Clients.Remove(client.client);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Client>> GetAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Client> GetByCustomerIdAsync(string id)
        {
            return await _context.Clients.SingleOrDefaultAsync(x => x.CustomerId == id);
        }

        public async Task<Client> GetByUserIdAsync(string id)
        {
            return await _context.Clients.SingleOrDefaultAsync(x => x.userID == id);
        }
        public async Task<Client> GetByIdAsync(string id)
        {
            return await _context.Clients.SingleOrDefaultAsync(x => x.userID == id);
        }

        public async Task<User> UpdateAsync(User client)
        {
            _context.Clients.UpdateRange(client.client);
            await _context.SaveChangesAsync();
            return client;
        }

        public Client getByUserName(string username)
        {
            var user = (from usr in _context.Clients
                        where usr.user.UserName == username
                        select usr).FirstOrDefault();
            return user;
        }
    }
}
