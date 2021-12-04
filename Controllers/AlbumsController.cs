using System.Net;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spotify_clone2.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Spotify_clone2.ViewModels;
using Spotify_clone2.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Spotify_clone2.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly IAlbumRepository _albumRepository;
        private IWebHostEnvironment _hostingEnv;
        private readonly IArtistRepository _artistRepository;
        private readonly UserManager<User> _userManager;


        public AlbumsController(UserManager<User> userManager, IAlbumRepository albumRepository, IArtistRepository artistRepository, IWebHostEnvironment hostingEnv)
        {
            _albumRepository = albumRepository;
            _hostingEnv = hostingEnv;
            _artistRepository = artistRepository;
            _userManager = userManager;
        }

        // GET: Albums
        public async Task<IActionResult> Index()
        {
            return View(await _albumRepository.GetAsync());
        }

        // GET: Albums/Details/5
        public async Task<IActionResult> Details(int? id, int? pageNumber)
        {
            if (id == null)
            {
                return NotFound();
            }
            var (album, count) = await _albumRepository.GetPageByIdAsync((int)id, pageNumber ?? 1);
            if (album == null)
            {
                return NotFound();
            }
            ViewBag.count = count;
            ViewBag.pageNumber = pageNumber;
            return View((album));
        }

        // GET: Albums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "artist")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAlbumViewModel album)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            
            if (ModelState.IsValid)
            {
                Collection<Song> songs = new Collection<Song>();
                foreach (var songData in album.songs)
                {
                    var uniqueSongName = GetUniqueFileName(songData.song.FileName);
                    var songDir = Path.Combine(_hostingEnv.ContentRootPath, "wwwroot/Song");
                    if (!Directory.Exists(songDir))
                    {
                        Directory.CreateDirectory(songDir);
                    }
                    var songPath = Path.Combine(songDir, uniqueSongName);
                    await songData.song.CopyToAsync(new FileStream(songPath, FileMode.Create));
                    var uniqueSongCoverName = GetUniqueFileName(songData.songCover.FileName);
                    var songCoverDir = Path.Combine(_hostingEnv.ContentRootPath, "wwwroot/img/songsCover");
                    if (!Directory.Exists(songCoverDir))
                    {
                        Directory.CreateDirectory(songCoverDir);
                    }
                    var songCoverPath = Path.Combine(songCoverDir, uniqueSongCoverName);
                    await songData.songCover.CopyToAsync(new FileStream(songCoverPath, FileMode.Create));
                    var newSong = new Song()
                    {
                        songPath = uniqueSongName,
                        nomSong = songData.nom,
                        description = songData.description,
                        category = songData.Category,
                        songCover = uniqueSongCoverName,
                        artiste=user.artiste
                    };
                    songs.Add(newSong);
                }
                var uniqueAlbumName = GetUniqueFileName(album.albumCover.FileName);
                var AlbumDir = Path.Combine(_hostingEnv.ContentRootPath, "wwwroot/img/album");
                if (!Directory.Exists(AlbumDir))
                {
                    Directory.CreateDirectory(AlbumDir);
                }
                var filePath = Path.Combine(AlbumDir, uniqueAlbumName);
                await album.albumCover.CopyToAsync(new FileStream(filePath, FileMode.Create));
                var artiste = _artistRepository.getByUserID(user.Id);
                Album album1 = new Album()
                {
                    albumCover = uniqueAlbumName,
                    name = album.name,
                    Songs = songs,
                    Artiste = artiste,
                    ArtisteID = artiste.ArtisteId
                };
                
                await _albumRepository.CreateAsync(album1);
                return RedirectToAction(nameof(Index));
            }
            return View(album);
        }
        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                   + "_"
                   + Guid.NewGuid().ToString().Substring(0, 4)
                   + Path.GetExtension(fileName);
        }

        // GET: Albums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _albumRepository.GetByIdAsync((int)id);
            if (album == null)
            {
                return NotFound();
            }
            return View(album);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AlbumId,name")] Album album)
        {
            if (id != album.AlbumId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _albumRepository.UpdateAsync(album);
                }
                catch (DbUpdateConcurrencyException)
                {
                    bool albumExist = await _albumRepository.AlbumExist(album.AlbumId);
                    if (!albumExist)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(album);
        }

        // GET: Albums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _albumRepository.GetByIdAsync((int)id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var album = await _albumRepository.GetByIdAsync(id);
            await _albumRepository.DeleteAsync(album);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Songs(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _albumRepository.GetByIdAsync((int)id);
            if (album == null)
            {
                return NotFound();
            }
            return View(album);
        }

    }
}
