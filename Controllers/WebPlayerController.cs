﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotify_clone2.Controllers
{
    public class WebPlayerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}