using System.Collections.Generic;
using System.Threading.Tasks;
using Spotify_clone2.Models;
using Microsoft.EntityFrameworkCore;

namespace Spotify_clone2.Repositories
{
    public interface IAlbumRepository
    {
        Task<Album> UpdateAsync(Album album);
        Task<IEnumerable<Album>> GetAsync();
        Task<Album> GetByIdAsync(int id);
        Task<(Album,int)> GetPageByIdAsync(int id, int pageNumber);
        Task<Album> CreateAsync(Album album);
        Task DeleteAsync(Album artist);
        Task<bool> AlbumExist(int id);
    }
}