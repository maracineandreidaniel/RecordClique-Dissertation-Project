using Microsoft.EntityFrameworkCore;
using RecordClique.Models;
using RecordClique_DataAccess.Entities;

namespace RecordClique_DataAccess.Context
{
    public class RecordCliqueContext : DbContext
    {
        public RecordCliqueContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Artist> Artists { get; set; } 
        public DbSet<Album> Albums { get; set; } 
        public DbSet<AlbumGenreLink> AlbumGenreLinks { get; set; } 
        public DbSet<Comment> Comments { get; set; } 
        public DbSet<Genre> Genres { get; set; } 
        public DbSet<RecordLabel> RecordLabels { get; set; } 
        public DbSet<User> Users { get; set; } 
        public DbSet<UserAlbumLink> UserAlbumLinks { get; set; } 


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //M:M for Album-Genre
            modelBuilder.Entity<AlbumGenreLink>()
            .HasKey(ag => new { ag.FK_AlbumId, ag.FK_GenreId });
            modelBuilder.Entity<AlbumGenreLink>()
                .HasOne(ag => ag.Album)
                .WithMany(a => a.AlbumGenreLinks)
                .HasForeignKey(ag => ag.FK_AlbumId);
            modelBuilder.Entity<AlbumGenreLink>()
                .HasOne(ag => ag.Genre)
                .WithMany(a => a.AlbumGenreLinks)
                .HasForeignKey(ag => ag.FK_GenreId);

            //M:M for Album-Artist
            modelBuilder.Entity<AlbumArtistLink>()
            .HasKey(aa => new { aa.FK_AlbumId, aa.FK_ArtistId });
            modelBuilder.Entity<AlbumArtistLink>()
                .HasOne(aa => aa.Album)
                .WithMany(a => a.AlbumArtistLinks)
                .HasForeignKey(aa => aa.FK_AlbumId);
            modelBuilder.Entity<AlbumArtistLink>()
                .HasOne(aa => aa.Artist)
                .WithMany(a => a.AlbumArtistLinks)
                .HasForeignKey(aa => aa.FK_ArtistId);

            //M:M for Album-User
            modelBuilder.Entity<UserAlbumLink>()
            .HasKey(ua => new { ua.FK_AlbumId, ua.FK_UserId });
            modelBuilder.Entity<UserAlbumLink>()
                .HasOne(ua => ua.Album)
                .WithMany(a => a.UserAlbumLinks)
                .HasForeignKey(ua => ua.FK_AlbumId);
            modelBuilder.Entity<UserAlbumLink>()
                .HasOne(ua => ua.User)
                .WithMany(a => a.UserAlbumLinks)
                .HasForeignKey(ua => ua.FK_UserId);

            //M:1 for Album-RecordLabel
            modelBuilder.Entity<Album>()
                .HasOne(a => a.RecordLabel)
                .WithMany(rl => rl.Albums)
                .HasForeignKey(a => a.FK_RecordLabelId);

        }

    }
}
