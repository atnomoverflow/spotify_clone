using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spotify_clone2.Models;

namespace Spotify_clone2.Repositories
{

    public interface ISongRepository
    {
        Task<Song> UpdateAsync(Song client);
        Task<IEnumerable<Song>> GetAsync();
        Task<Song> GetBySongIdAsync(int id);
        Task<Song> CreateAsync(Song song);
        Task DeleteAsync(Song Song);
        IQueryable<Song> getBySongName(string SongName);
        Task<IEnumerable<Song>> getMostPopularSong();
        Task<IEnumerable<Song>> getMostViewsSong();
        Task<IEnumerable<Song>> getMostRecentSong();
        Task<IEnumerable<Song>> getSongPage(int pageNumber, int pageSize);
    }
}
