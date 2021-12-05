using System.Collections.Generic;
using System.Threading.Tasks;
using Spotify_clone2.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        public async Task<IList<Song>> getMostPopulareSong(Artiste artiste)
        {
            var songsQeury =
            await (from song in _context.Songs
                   where song.Album.Artiste.ArtisteId == artiste.ArtisteId
                   orderby song.likes
                   select song
            ).Take(2).ToListAsync();
            return songsQeury;
        }

        public Artiste getByUserName(string username)
        {
            var artiste= (from usr in _context.Artistes
                    where usr.User.UserName == username
                    select usr).FirstOrDefault();
            return artiste;
        }
        public Artiste getByUserID(string id)
        {
            var artiste = _context.Artistes.SingleOrDefault(x=>x.UserID==id);
            return artiste;
        }
    }
}