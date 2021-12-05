using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Spotify_clone2.Models;

namespace Spotify_clone2.Models
{
    public class AppDbContext : IdentityDbContext
    {
        public DbSet<Song> Songs { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Memebership> Memeberships { get; set; }
        public DbSet<PlayList> PlayLists { get; set; }
        public DbSet<Client> Clients { get; set; }

        public DbSet<Artiste> Artistes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=localhost;Database=spotify;User Id=sa;Password=Asefb@101;");
            optionsBuilder.UseSqlServer(@"Server=LAPTOP-4SRJP1SK;Database=spotify;Trusted_Connection=True;");
        }


    }
}
