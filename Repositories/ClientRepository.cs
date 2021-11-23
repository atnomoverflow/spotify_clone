﻿using System.Collections.Generic;
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
        public async Task<Client> CreateAsync(Client client)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
            return client;

        }

        public async Task DeleteAsync(Client client)
        {
            _context.Clients.Remove(client);
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

        public async Task<Client> GetByIdAsync(string id)
        {
            return await _context.Clients.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Client> UpdateAsync(Client client)
        {
            _context.Clients.UpdateRange(client);
            await _context.SaveChangesAsync();
            return client;
        }
    }
}