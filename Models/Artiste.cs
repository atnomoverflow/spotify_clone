using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Spotify_clone2.Models
{
    public class Artiste : User
    {
        public int ArtisteId { get; set; }
        public ICollection<Album> Albums { get; set; }
    }
}
