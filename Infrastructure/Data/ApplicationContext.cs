using Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "J. R. R. Tolkien", Description = "Autor de fantasía épica" },
                new Author { Id = 2, Name = "Agatha Christie", Description = "Reina del misterio" },
                new Author { Id = 3, Name = "George Orwell", Description = "Autor de distopías y ensayo político" }
            );

            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Description = "Fantasía" },
                new Genre { Id = 2, Description = "Ciencia Ficción" },
                new Genre { Id = 3, Description = "Misterio" },
                new Genre { Id = 4, Description = "No Ficción" },
                new Genre { Id = 5, Description = "Distopía" }
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

            base.OnModelCreating(modelBuilder);
        }
    }
}
