using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotify_clone2.Models
{
    public class PlayList
    {
        private int idPlayList { get; set; }
        private ICollection<Song> songs { get; set; }
    }
}
