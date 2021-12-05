using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spotify_clone2.Repositories;
using Spotify_clone2.ViewModels;

namespace Spotify_clone2.Controllers
{
    public class WebPlayerController : Controller
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly ISongRepository _songRepository;

        public WebPlayerController(IAlbumRepository albumRepository,ISongRepository songRepository)
        {
            _albumRepository = albumRepository;
            _songRepository = songRepository;

        }
        public async Task<IActionResult> Index()
        {
            var popluarSong = await _songRepository.getMostPopularSong();
            var mostViews = await _songRepository.getMostViewsSong();
            var newSong = await _songRepository.getMostRecentSong();
            DashbordIndexViewModel model = new DashbordIndexViewModel(mostViews,popluarSong,newSong);
            return View(model);
        }
    }
}
