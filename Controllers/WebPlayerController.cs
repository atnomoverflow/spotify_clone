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
        int PageSize = 10;

        private readonly IAlbumRepository _albumRepository;
        private readonly ISongRepository _songRepository;
        private readonly IArtistRepository _artistRepository;
        public WebPlayerController(IAlbumRepository albumRepository, ISongRepository songRepository, IArtistRepository artistRepository)
        {
            _albumRepository = albumRepository;
            _songRepository = songRepository;
            _artistRepository = artistRepository;
        }
        public async Task<IActionResult> Index()
        {
            var popluarSong = await _songRepository.getMostPopularSong();
            var mostViews = await _songRepository.getMostViewsSong();
            var newSong = await _songRepository.getMostRecentSong();
            DashbordIndexViewModel model = new DashbordIndexViewModel(mostViews, popluarSong, newSong);
            return View(model);
        }
        public async Task<IActionResult> SongFilter(int pageNumber)
        {

            var SongResult = await _songRepository.getSongPage(pageNumber, PageSize);

            return View(SongResult);
        }
        public async Task<IActionResult> AlbumFilter(string options, int pageNumber)
        {
            var AlbumResult = await _albumRepository.getAlbumPage(pageNumber, PageSize);

            return View(AlbumResult);
        }
        public async Task<IActionResult> ArtistFilter(int pageNumber)
        {
            var ArtistsResult = await _songRepository.getSongPage(pageNumber, PageSize);
            return View(ArtistsResult);
        }
    }
}
