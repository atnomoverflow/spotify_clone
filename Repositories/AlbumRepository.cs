using System.Collections.Generic;
using System.Threading.Tasks;
using Spotify_clone2.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace Spotify_clone2.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        public readonly AppDbContext _context;
        public AlbumRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AlbumExist(int id)
        {
            return await _context.Albums.AnyAsync(x => x.AlbumId == id);
        }

        public async Task<Album> CreateAsync(Album album)
        {
            _context.Add(album);
            await _context.SaveChangesAsync();
            return album;
        }

        public async Task DeleteAsync(Album album)
        {
            _context.Albums.Remove(album);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Album>> GetAsync()
        {
            return await _context.Albums.ToListAsync();
        }

        public async Task<Album> GetByIdAsync(int id)
        {
            var album = await _context.Albums.Include("Songs").FirstOrDefaultAsync(x => x.AlbumId == id);
            return album;
        }

       

        public async Task<Album> UpdateAsync(Album album)
        {
            _context.UpdateRange(album);
            await _context.SaveChangesAsync();
            return album;
        }
    }
}