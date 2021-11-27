using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;


namespace Spotify_clone2.Models
{
    public class Artiste
    {
        public virtual User user { get; set; }
        public string userID { get; set; }

        public int ArtisteId { get; set; }
        public ICollection<Album> Albums { get; set; }
    }
}
