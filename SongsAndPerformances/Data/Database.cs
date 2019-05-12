using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Songs_and_Performances.Models;
using SongsAndPerformances.Models;

namespace Songs_and_Performances.Data
{
    public class Database : DbContext
    {
        public Database(DbContextOptions<Database> options) : base(options)
        {
        }
       
        public DbSet<Song> Songs { get; set; }
        public DbSet<Performer> Performers { get; set; }
        public DbSet<Performance> Performances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Song>().ToTable("Song");
            modelBuilder.Entity<Performer>().ToTable("Performer");
            modelBuilder.Entity<Performance>().ToTable("Performance");
        }

        public DbSet<SongsAndPerformances.Models.Composer> Composer { get; set; }

        public DbSet<SongsAndPerformances.Models.ComposerSong> ComposerSong { get; set; }
    }
}
