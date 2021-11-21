using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Spotify_clone2.ViewModels
{
    public class ProfileViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        public string Nom { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Prenom { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DOB { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Username { get; set; }

    }
}
