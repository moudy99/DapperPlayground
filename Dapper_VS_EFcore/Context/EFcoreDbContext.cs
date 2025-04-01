using Dapper_VS_EFcore.Models;
using Microsoft.EntityFrameworkCore;

namespace Dapper_VS_EFcore.Context
{
    public class EFcoreDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Screenshot> Screenshots { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public DbSet<GameDeveloper> GameDevelopers { get; set; }
        public DbSet<GamePublisher> GamePublishers { get; set; }
        public DbSet<GameCategory> GameCategories { get; set; }
        public DbSet<GameGenre> GameGenres { get; set; }
        public DbSet<GameTag> GameTags { get; set; }
        public DbSet<GameLanguage> GameLanguages { get; set; }

        public EFcoreDbContext(DbContextOptions<EFcoreDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // GameDeveloper (Game <-> Developer)
            modelBuilder.Entity<GameDeveloper>()
                .HasKey(gd => new { gd.GameID, gd.DeveloperID });

            modelBuilder.Entity<GameDeveloper>()
                .HasOne(gd => gd.Game)
                .WithMany(g => g.GameDevelopers)
                .HasForeignKey(gd => gd.GameID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GameDeveloper>()
                .HasOne(gd => gd.Developer)
                .WithMany(d => d.GameDevelopers)
                .HasForeignKey(gd => gd.DeveloperID)
                .OnDelete(DeleteBehavior.Cascade);

            // GamePublisher (Game <-> Publisher)
            modelBuilder.Entity<GamePublisher>()
                .HasKey(gp => new { gp.GameID, gp.PublisherID });

            modelBuilder.Entity<GamePublisher>()
                .HasOne(gp => gp.Game)
                .WithMany(g => g.GamePublishers)
                .HasForeignKey(gp => gp.GameID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GamePublisher>()
                .HasOne(gp => gp.Publisher)
                .WithMany(p => p.GamePublishers)
                .HasForeignKey(gp => gp.PublisherID)
                .OnDelete(DeleteBehavior.Cascade);

            // GameCategory (Game <-> Category)
            modelBuilder.Entity<GameCategory>()
                .HasKey(gc => new { gc.GameID, gc.CategoryID });

            modelBuilder.Entity<GameCategory>()
                .HasOne(gc => gc.Game)
                .WithMany(g => g.GameCategories)
                .HasForeignKey(gc => gc.GameID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GameCategory>()
                .HasOne(gc => gc.Category)
                .WithMany(c => c.GameCategories)
                .HasForeignKey(gc => gc.CategoryID)
                .OnDelete(DeleteBehavior.Cascade);

            // GameGenre (Game <-> Genre)
            modelBuilder.Entity<GameGenre>()
                .HasKey(gg => new { gg.GameID, gg.GenreID });

            modelBuilder.Entity<GameGenre>()
                .HasOne(gg => gg.Game)
                .WithMany(g => g.GameGenres)
                .HasForeignKey(gg => gg.GameID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GameGenre>()
                .HasOne(gg => gg.Genre)
                .WithMany(g => g.GameGenres)
                .HasForeignKey(gg => gg.GenreID)
                .OnDelete(DeleteBehavior.Cascade);

            // GameTag (Game <-> Tag)
            modelBuilder.Entity<GameTag>()
                .HasKey(gt => new { gt.GameID, gt.TagID });

            modelBuilder.Entity<GameTag>()
                .HasOne(gt => gt.Game)
                .WithMany(g => g.GameTags)
                .HasForeignKey(gt => gt.GameID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GameTag>()
                .HasOne(gt => gt.Tag)
                .WithMany(t => t.GameTags)
                .HasForeignKey(gt => gt.TagID)
                .OnDelete(DeleteBehavior.Cascade);

            // GameLanguage (Game <-> Language)
            modelBuilder.Entity<GameLanguage>()
                .HasKey(gl => new { gl.GameID, gl.LanguageID });

            modelBuilder.Entity<GameLanguage>()
                .HasOne(gl => gl.Game)
                .WithMany(g => g.GameLanguages)
                .HasForeignKey(gl => gl.GameID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GameLanguage>()
                .HasOne(gl => gl.Language)
                .WithMany(l => l.GameLanguages)
                .HasForeignKey(gl => gl.LanguageID)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-Many: Screenshots
            modelBuilder.Entity<Screenshot>()
                .HasOne(s => s.Game)
                .WithMany(g => g.Screenshots)
                .HasForeignKey(s => s.GameID)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-Many: Packages
            modelBuilder.Entity<Package>()
                .HasOne(p => p.Game)
                .WithMany(g => g.Packages)
                .HasForeignKey(p => p.GameID)
                .OnDelete(DeleteBehavior.Cascade);

            // Unique constraints on Name fields
            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<Developer>()
                .HasIndex(d => d.Name)
                .IsUnique();

            modelBuilder.Entity<Genre>()
                .HasIndex(g => g.Name)
                .IsUnique();

            modelBuilder.Entity<Language>()
                .HasIndex(l => l.Name)
                .IsUnique();

            modelBuilder.Entity<Publisher>()
                .HasIndex(p => p.Name)
                .IsUnique();

            modelBuilder.Entity<Tag>()
                .HasIndex(t => t.Name)
                .IsUnique();
        }
    }
}