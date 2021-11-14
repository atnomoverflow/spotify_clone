using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotify_clone2.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string designation { get; set; }
        public string description { get; set; }
        public ICollection<Song> songs { get; set; }
    }
}
