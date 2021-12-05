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
                                        orderby song.likes
                                        select song;
            var result = await mostPopularSongQuerry.ToListAsync();
            foreach (var song in result)
            {
                var album = await _context.Albums.FirstOrDefaultAsync(x => x.AlbumID == song.AlbumId);
                album.Artiste = await _context.Artistes.Include("User").FirstOrDefaultAsync(x => x.ArtisteId == album.ArtisteID);
                song.Album = album;
            }
            return result;
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

                                        orderby song.createdAt
                                        select song;
            var result = await mostPopularSongQuerry.Take(3).ToListAsync();
            foreach (var song in result)
            {
                var album = await _context.Albums.FirstOrDefaultAsync(x => x.AlbumID == song.AlbumId);
                album.Artiste = await _context.Artistes.Include("User").FirstOrDefaultAsync(x => x.ArtisteId == album.ArtisteID);
                song.Album = album;
            }
            return result;
        }

        public async Task<IEnumerable<Song>> getMostViewsSong()
        {
            var mostPopularSongQuerry = from song in _context.Songs
                                        orderby song.views
                                        select song;
            var result = await mostPopularSongQuerry.Take(3).ToListAsync();
            result = await addInfo(result);
            return result;
        }
        public async Task<IEnumerable<Song>> getSongPage(int pageNumber, int pageSize)
        {
            var PageNumber = pageNumber < 1 ? 1 : pageNumber;
            var PageSize = pageSize > 10 ? 10 : pageSize;
            var songsQuerry = (from song in _context.Songs
                               select song).Skip(((int)pageNumber - 1) * PageSize).Take(PageSize);
            var song_result = await addInfo(await songsQuerry.ToListAsync());
            return song_result;
        }
        private async Task<List<Song>> addInfo(List<Song> result)
        {
            foreach (var song in result)
            {
                var album = await _context.Albums.FirstOrDefaultAsync(x => x.AlbumID == song.AlbumId);
                album.Artiste = await _context.Artistes.Include("User").FirstOrDefaultAsync(x => x.ArtisteId == album.ArtisteID);
                song.Album = album;
            }
            return result;
        }

        public async Task<IEnumerable<Song>> GetByAlbumIdAsync(int id)
        {
            var Songs = await (from Song in _context.Songs where Song.AlbumId == id select Song).ToListAsync() ;
            return Songs;
        }
    }
}