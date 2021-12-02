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
<<<<<<< HEAD
        //Task getMostPopulareSong(Artiste artiste);
=======
        Task<IList<Song>> getMostPopulareSong(Artiste artiste);
>>>>>>> 55e0bf617ef6e8692a105b75a085eb9cc523531a
    }
}