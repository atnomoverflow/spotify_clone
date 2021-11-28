using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spotify_clone2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spotify_clone2.Models;
using Spotify_clone2.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Spotify_clone2.Controllers
{
    public class ArtistController : Controller
    {
        private readonly AppDbContext _context;
        public ArtistController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Artist/Detail/5
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.Artistes.FindAsync(id);

            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }

    }
}
