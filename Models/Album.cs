using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotify_clone2.Models
{
    public class Album
    {
        private int idAlbum { get; set; }
        private ICollection<Song> songs { get; set; }
    }
}
