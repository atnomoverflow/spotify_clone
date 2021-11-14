using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Spotify_clone2.Models
{
    public class User: IdentityUser
    {
        [PersonalData]
        public string Nom { get; set; }
        [PersonalData]
        public string Prenom { get; set; }
        [PersonalData]
        public DateTime DOB { get; set; }
    }
}
