using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotify_clone2.Models
{

    public class Client
    {
        public virtual User User { get; set; }

        [ForeignKey("User")]
        public string ClientId { get; set; }
        public ICollection<PlayList> PlayLists { get; set; }
        public string CustomerId { get; set; }
    }
}
