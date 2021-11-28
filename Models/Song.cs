using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotify_clone2.Models
{
    public enum Category
    {
         Pop,
         Classical,
         Hip_Hop,
         EDM,
         Country,
         Metal,
         Gospel,
         Folk,
         Jazz,
         Ballads,
         Blues,
         Funk,
         Reggae,
         Ambient,
        World
    }
    public enum tags
    {
        quitar, piano, music, electronic, minimal, chill, pop, cultural, live, popular
    }


    public class Song
    {
        public int SongId { get; set; }
        public string nomSong { get; set; }
        public string description { get; set; }
        public double duration { get; set; }
        public Category category { get; set; }
       /* public ICollection<Enum> tags { get; set; }*/
        public Album album { get; set; }
        public int likes { get; set; }
        public int views { get; set; }
        public Artiste artiste { get; set; }
    }
}
