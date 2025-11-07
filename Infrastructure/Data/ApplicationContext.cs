using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Domain.Enums;

namespace Infrastructure.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Lecture> Lectures { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "J. R. R. Tolkien", Description = "Autor de fantasía épica" },
                new Author { Id = 2, Name = "Agatha Christie", Description = "Reina del misterio" },
                new Author { Id = 3, Name = "George Orwell", Description = "Autor de distopías y ensayo político" }
            );

            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, GenreName = "Fantasía" },
                new Genre { Id = 2, GenreName = "Ciencia Ficción" },
                new Genre { Id = 3, GenreName = "Misterio" },
                new Genre { Id = 4, GenreName = "No Ficción" },
                new Genre { Id = 5, GenreName = "Distopía" }
            );

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "Manu", Email = "manu@gmail.com", Role = UserRole.SuperAdmin, Password ="123456" },
                new User { Id = 2, Username = "Mati", Email = "mati@gmail.com", Role = UserRole.SuperAdmin, Password ="123456" }
            );

            modelBuilder.Entity<User>()
                .HasMany(u => u.Lectures)
                .WithOne(l => l.User)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Lecture>()
                .HasOne(l => l.Book)
                .WithMany()
                .HasForeignKey(l => l.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            // Tabla intermedia Book-Genre
            modelBuilder.Entity<Book>()
                .HasMany(b => b.Genres)
                .WithMany(g => g.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BookGenre",
                    j => j.HasOne<Genre>()
                          .WithMany()
                          .HasForeignKey("GenreId")
                          .OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne<Book>()
                          .WithMany()
                          .HasForeignKey("BookId")
                          .OnDelete(DeleteBehavior.Cascade)
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
