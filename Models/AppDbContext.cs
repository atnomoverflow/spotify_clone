using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Spotify_clone2.Models
{
    public class AppDbContext : IdentityDbContext
    {
        
        public DbSet<Song> Songs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Memebership> Memeberships { get; set; }
        public DbSet<PlayList> PlayLists { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
             optionsBuilder.UseSqlServer(@"Server=localhost;Database=spotify;User Id=sa;Password=Asefb@101;");
            //optionsBuilder.UseSqlServer(@"Server=LAPTOP-4SRJP1SK;Database=spotify;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Artiste>().ToTable("Artistes");
            modelBuilder.Entity<Client>().ToTable("Clients");
        }
        
    }
}
