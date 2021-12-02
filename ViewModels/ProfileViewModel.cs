using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Spotify_clone2.ViewModels
{
    public class ProfileViewModel
    {
        public ChangePasswordViewModel ChangePasswordViewModel { get; set; }
        public UserDetailViewModel UserDetailViewModel { get; set; }
    }
    public class UserDetailViewModel
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
    public class ChangePasswordViewModel
    {
        [Required]
        [Display(Name = "Old Password")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required]
        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("NewPassword", ErrorMessage = "Password and confirmation password not match.")]
        public string ConfirmPassword { get; set; }
    }

}
