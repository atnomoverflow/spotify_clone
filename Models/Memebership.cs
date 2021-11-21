using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Spotify_clone2.Models
{
    public class Memebership
    {

        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string Status { get; set; }
        public DateTime CurrentPeriodEnd { get; set; }
    }
}
