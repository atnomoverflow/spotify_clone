using System.Collections.Generic;
using System.Threading.Tasks;
using Spotify_clone2.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace Spotify_clone2.Repositories
{
    public class SongRepository : ISongRepository
    {
        AppDbContext _context;
        public SongRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Song> CreateAsync(Song song)
        {
            _context.Songs.Add(song);
            await _context.SaveChangesAsync();
            return song;
        }

        public async Task DeleteAsync(Song Song)
        {
            _context.Remove(Song);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Song>> GetAsync()
        {
            return await _context.Songs.ToListAsync();
        }


        public async Task<Song> GetBySongIdAsync(int id)
        {
            var Song = await _context.Songs.SingleOrDefaultAsync(x => x.SongId == id);
            return Song;
        }

        public IQueryable<Song> getBySongName(string SongName)
        {
            var SearchQuery = from song in _context.Songs
                              where song.nomSong.Contains(SongName)
                              select song;
            return SearchQuery;
        }

        public async Task<IEnumerable<Song>> getMostPopularSong()
        {
            var mostPopularSongQuerry = from song in _context.Songs
                                        join artiste in _context.Artistes on song.artiste equals artiste
                                        join album in _context.Albums on song.Album equals album
                                        orderby song.likes
                                        select song;
            return await mostPopularSongQuerry.Take(3).ToListAsync();
        }



        public async Task<Song> UpdateAsync(Song song)
        {
            _context.UpdateRange(song);
            await _context.SaveChangesAsync();
            return song;
        }

        public async Task<IEnumerable<Song>> getMostRecentSong()
        {
            var mostPopularSongQuerry = from song in _context.Songs
                                        join artiste in _context.Artistes on song.artiste equals artiste
                                        join album in _context.Albums on song.Album equals album
                                        orderby song.createdAt
                                        select song;
            return await mostPopularSongQuerry.Take(3).ToListAsync();
        }

        public async Task<IEnumerable<Song>> getMostViewsSong()
        {
            var mostPopularSongQuerry = from song in _context.Songs
                                        join album in _context.Albums on song.Album equals album
                                        orderby song.views
                                        select song;
            var result = await mostPopularSongQuerry.Take(3).ToListAsync();
            return result;
        }
    }
}