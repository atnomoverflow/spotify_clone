﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spotify_clone2.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Spotify_clone2.ViewModels;
namespace Spotify_clone2.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _hostingEnv;

        public AlbumsController(AppDbContext context, IWebHostEnvironment hostingEnv)
        {
            _context = context;
            _hostingEnv = hostingEnv;
        }

        // GET: Albums
        public async Task<IActionResult> Index()
        {
            return View(await _context.Albums.ToListAsync());
        }

        // GET: Albums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .FirstOrDefaultAsync(m => m.AlbumId == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // GET: Albums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAlbumViewModel album)
        {
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
                    var newSong = new Song()
                    {
                        songPath = songPath,
                        nomSong = songData.nom,
                        description = songData.description,
                        category = songData.Category
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
                Album newAlbum = new Album()
                {
                    albumCover = uniqueAlbumName,
                    name = album.name,
                    songs = songs
                };
                _context.Add(newAlbum);
                await _context.SaveChangesAsync();
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

            var album = await _context.Albums.FindAsync(id);
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
                    _context.Update(album);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumExists(album.AlbumId))
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

            var album = await _context.Albums
                .FirstOrDefaultAsync(m => m.AlbumId == id);
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
            var album = await _context.Albums.FindAsync(id);
            _context.Albums.Remove(album);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlbumExists(int id)
        {
            return _context.Albums.Any(e => e.AlbumId == id);
        }


        public async Task<IActionResult> Songs(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .FirstOrDefaultAsync(m => m.AlbumId == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

    }
}
