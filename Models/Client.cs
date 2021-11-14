using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotify_clone2.Models
{

    public class Client : User
    {
        public int ClientId { get; set; }
        public ICollection<PlayList> PlayLists { get; set; }
    }
}
