using System;

using System.ComponentModel.DataAnnotations;
namespace Spotify_clone2.Models
{
    public enum Category
    {
        [Display(Name = "Pop")]
        Pop,
        [Display(Name = "Classical")]
        Classical,
        [Display(Name = "Hip Hop")]
        Hip_Hop,
        [Display(Name = "EDM")]
        EDM,
        [Display(Name = "Country")]
        Country,
        [Display(Name = "Metal")]
        Metal,
        [Display(Name = "Gospel")]
        Gospel,
        [Display(Name = "Folk")]
        Folk,
        [Display(Name = "Jazz")]
        Jazz,
        [Display(Name = "Ballads")]
        Ballads,
        [Display(Name = "Blues")]
        Blues,
        [Display(Name = "Funk")]
        Funk,
        [Display(Name = "Reggae")]
        Reggae,
        [Display(Name = "Ambient")]
        Ambient,
        [Display(Name = "World")]
        World
    }
    public enum Tags
    {
        [Display(Name = "quitar")]
        quitar,
        [Display(Name = "piano")]
        piano,
        [Display(Name = "music")]
        music,
        [Display(Name = "electronic")]
        electronic,
        [Display(Name = "minimal")]
        minimal,
        [Display(Name = "chill")]
        chill,
        [Display(Name = "pop")]
        pop,
        [Display(Name = "cultural")]
        cultural,
        [Display(Name = "live")]
        live,
        [Display(Name = "popular")]
        popular

    }


    public class Song
    {
        public int SongId { get; set; }
        public string nomSong { get; set; }
        public string description { get; set; }
        public string songPath { get; set; }
        public string songCover { get; set; }
        public Category category { get; set; }
        /* public ICollection<Enum> tags { get; set; }*/
        public int AlbumId { get; set; }
        public Album Album { get; set; }
        public int likes { get; set; }
        public int views { get; set; }
        public DateTime createdAt { get; set; }
        public Artiste artiste { get; set; }
    }
}
