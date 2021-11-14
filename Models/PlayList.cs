using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotify_clone2.Models
{
    public class PlayList
    {
        public int PlayListId { get; set; }
        public ICollection<Song> songs { get; set; }
    }
}
