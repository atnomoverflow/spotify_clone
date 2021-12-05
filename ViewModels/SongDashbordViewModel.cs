using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spotify_clone2.Models;
namespace Spotify_clone2.ViewModels
{
    public class DashbordIndexViewModel
    {
        public IEnumerable<Song> mostLikesSongs { get; set; }
        public IEnumerable<Song> recentSongs { get; set; }
        public IEnumerable<Song> mostViewsSongs { get; set; }
        public DashbordIndexViewModel(IEnumerable<Song> views, IEnumerable<Song> likes, IEnumerable<Song> recent)
        {
            mostLikesSongs = likes;
            mostViewsSongs = views;
            recentSongs = recent;
        }

    }
}