using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotify_clone2.Models
{
    public class Song
    {
        private int idSong { get; set; }
        private string nomSong { get; set; }
        private string description { get; set; }
        private double duration { get; set; }
        private ICollection<Category> type { get; set; }
        private Album album { get; set; }
        private int likes { get; set; }
        private int views { get; set; }
    }
}
