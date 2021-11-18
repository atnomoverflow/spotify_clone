using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Spotify_clone2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Spotify_clone2.Controllers
{
    public class premiumController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        
    }
}
