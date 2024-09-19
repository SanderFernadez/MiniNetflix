
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Database.Contexts
{
    public class ApplicationContext : DbContext
    {

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }



        public DbSet<Serie> Series { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Producer> Producers { get; set; }

        public DbSet<genre_serie> genre_Series { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            #region Tables
            modelBuilder.Entity<Serie>().ToTable("Series");
            modelBuilder.Entity<Genre>().ToTable("Genres");
            modelBuilder.Entity<Producer>().ToTable("Producers");
            modelBuilder.Entity<genre_serie>().ToTable("GenreSeries");
            #endregion

            #region "Primary Keys"
            modelBuilder.Entity<Serie>().HasKey(s => s.Id);
            modelBuilder.Entity<Genre>().HasKey(g => g.Id);
            modelBuilder.Entity<Producer>().HasKey(p => p.Id);
            modelBuilder.Entity<genre_serie>().HasKey(sg => new { sg.SerieId, sg.GenreId });
            #endregion

            #region RelationShips

            modelBuilder.Entity<Producer>()
                .HasMany<Serie>(p => p.Series)
                .WithOne(s => s.Producer)
                .HasForeignKey(s => s.ProducerId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Serie>()
                    .HasOne(s => s.Genre)
                    .WithMany()
                    .HasForeignKey(s => s.GenreId)
                    .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<genre_serie>()
             .HasOne(sg => sg.Serie)
             .WithMany(s => s.SecondaryGenres)
             .HasForeignKey(sg => sg.SerieId);

            modelBuilder.Entity<genre_serie>()
                .HasOne(sg => sg.Genre)
                .WithMany(g => g.Series)
                .HasForeignKey(sg => sg.GenreId);

            #endregion

            #region "Property configurations"

            #region Serie

            modelBuilder.Entity<Serie>().Property(s => s.Title).HasMaxLength(100);

            #endregion

            #region Producer

            modelBuilder.Entity<Producer>().Property(p => p.Name).HasMaxLength(30);

            #endregion

            #region Genre

            modelBuilder.Entity<Genre>().Property(g => g.Name).HasMaxLength(20);
            #endregion

            #endregion




        }









    }
}
