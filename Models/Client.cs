using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spotify_clone2.Models
{

    public class Client
    {
        public virtual User user { get; set; }
        public string UserID { get; set; }
        public int ClientId { get; set; }
        public virtual ICollection<PlayList> PlayLists { get; set; }
        public string CustomerId { get; set; }
    }
}
