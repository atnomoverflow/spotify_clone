﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotify_clone2.Models
{
    public class Album
    {
        public virtual Artiste Artiste { get; set; }
        public int ArtisteID { get; set; }

        public int AlbumID { get; set; }
        public string name { get; set; }
        public string albumCover { get; set; }
        public ICollection<Song> Songs { get; set; }
    }
}