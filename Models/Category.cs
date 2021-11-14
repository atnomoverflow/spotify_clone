using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotify_clone2.Models
{
    public class Category
    {
        private int idCat { get; set; }
        private string designation { get; set; }
        private string description { get; set; }
        private ICollection<Song> songs { get; set; }
    }
}
