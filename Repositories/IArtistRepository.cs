using System.Collections.Generic;
using System.Threading.Tasks;
using Spotify_clone2.Models;
using Microsoft.EntityFrameworkCore;

namespace Spotify_clone2.Repositories
{
    public interface IArtistRepository
    {
        Task<Artiste> UpdateAsync(Artiste artiste);
        Task<IEnumerable<Artiste>> GetAsync();
        Task<Artiste> GetByIdAsync(int id);
        Task<Artiste> CreateAsync(Artiste artist);
        Task DeleteAsync(Artiste artist);
        Task getMostPopulareSong(Artiste artiste);
    }
}