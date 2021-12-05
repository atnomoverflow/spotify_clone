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
        public string UserID { get; set; }

        public int ArtisteId { get; set; }
        public virtual ICollection<Album> Albums { get; set; }

        [Column(TypeName = "text")]
        public string bio { get; set; }
    }
}
