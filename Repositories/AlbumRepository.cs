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
            return await _context.Albums.AnyAsync(x => x.AlbumID == id);
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
            var album = await _context.Albums.Include("Songs").FirstOrDefaultAsync(x => x.AlbumID == id);
            return album;
        }

        public async Task<(Album, int)> GetPageByIdAsync(int id, int pageNumber = 1)
        {
            var albumSongs = _context.Songs.Where(x => x.AlbumId == id);
            var count = albumSongs.Count();
            albumSongs = albumSongs.Skip(((int)pageNumber - 1) * 6).Take(6);
            var album = await _context.Albums.FirstOrDefaultAsync(x => x.AlbumID == id);
            album.Artiste = await _context.Artistes.Include("user").FirstOrDefaultAsync(x => x.ArtisteId == album.ArtisteID);
            album.Songs = await albumSongs.ToListAsync();
            return (album, count);
        }
        public async Task<(Artiste, int)> GetAlbumsByIdAsync(int id, int pageNumber = 1)
        {
            var albums = _context.Albums.Where(x => x.ArtisteID == id);
            var count = albums.Count();
            albums = albums.Skip(((int)pageNumber - 1) * 6).Take(6);
            var artist = await _context.Artistes.Include("user").FirstOrDefaultAsync(x => x.ArtisteId == id);
            artist.Albums = await albums.ToListAsync();
            return (artist, count);
        }

        public async Task<Album> UpdateAsync(Album album)
        {
            _context.Albums.UpdateRange(album);
            _context.Entry(album).Property(u => u.ArtisteID).IsModified = false;
            _context.Entry(album).Property(u => u.albumCover).IsModified = false;
            await _context.SaveChangesAsync();
            return album;
        }

        public async Task<IEnumerable<Album>> getAlbumPage(int pageNumber, int pageSize)
        {
            var PageNumber = pageNumber < 1 ? 1 : pageNumber;
            var PageSize = pageSize > 10 ? 10 : pageSize;
            var albumQuerry = (from album in _context.Albums
                               select album).Skip(((int)pageNumber - 1) * PageSize).Take(PageSize);
            var album_result = await albumQuerry.ToListAsync();
            foreach (var album in album_result)
            {
                album.Artiste = await _context.Artistes.FirstOrDefaultAsync(x => x.ArtisteId == album.ArtisteID);
            }
            return album_result;
        }
    }
}