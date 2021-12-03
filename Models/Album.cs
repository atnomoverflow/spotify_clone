using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotify_clone2.Models
{
    public class Album
    {
        public Artiste artiste { get; set; }
        public int AlbumId { get; set; }
        public string name { get; set; }
        public string albumCover { get; set; }
        public ICollection<Song> songs { get; set; }
    }
}