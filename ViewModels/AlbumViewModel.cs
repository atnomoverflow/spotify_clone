using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Spotify_clone2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Spotify_clone2.ViewModels
{
    public class CreateAlbumViewModel
    {
        public string name { get; set; }
        public IFormFile albumCover { get; set; }
        public List<SongViewModel> songs { get; set; }

    }
    public class SongViewModel
    {
        public string nom { set; get; }
        public string description { get; set; }
        public IFormFile song { get; set; }
        public IFormFile songCover { get; set; }
        public Category Category { get; set; }
        public Tags Tag { get; set; }
        public IEnumerable<SelectListItem> categoryList { get; set; }
        public IEnumerable<SelectListItem> tagList { get; set; }

    }

}