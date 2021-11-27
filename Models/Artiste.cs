using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;


namespace Spotify_clone2.Models
{
    public class Artiste 
    {
        public virtual User User { get; set; }
        
        [ForeignKey("User")]
        public string ArtisteId { get; set; }
        public ICollection<Album> Albums { get; set; }
    }
}
