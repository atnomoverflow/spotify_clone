using System.Collections.Generic;
using System.Threading.Tasks;
using Spotify_clone2.Models;
using Microsoft.EntityFrameworkCore;

namespace Spotify_clone2.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly AppDbContext _context;
        public ArtistRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Artiste> UpdateAsync(Artiste artiste)
        {
            _context.Artistes.UpdateRange(artiste);
            await _context.SaveChangesAsync();
            return artiste;
        }
        public async Task<IEnumerable<Artiste>> GetAsync()
        {
            return await _context.Artistes.ToListAsync();

        }
        public async Task<Artiste> GetByIdAsync(int id)
        {
            return await _context.Artistes.SingleOrDefaultAsync(x => x.ArtisteId == id);
        }
        public async Task<Artiste> CreateAsync(Artiste artist)
        {
            await _context.Artistes.AddAsync(artist);
            await _context.SaveChangesAsync();
            return artist;
        }
        public async Task DeleteAsync(Artiste artist)
        {
            _context.Artistes.Remove(artist);
            await _context.SaveChangesAsync();
        }
        // public async Task<Song> getMostPopulareSong(Artiste artiste)
        // {

        // }
    }
}