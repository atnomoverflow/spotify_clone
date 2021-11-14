using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spotify_clone2.Models.Enums;
namespace Spotify_clone2.Models
{
    public class Memebership
    {
        public int MemebershipId { get; set; }
        public MembershipType MembershipType { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
