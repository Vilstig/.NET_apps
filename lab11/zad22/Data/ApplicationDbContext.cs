// ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using zad2.Models; 

namespace zad2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Elektronika" },
                new Category { Id = 2, Name = "Odzież" },
                new Category { Id = 3, Name = "Książki" }
            );

            modelBuilder.Entity<Article>()
                .HasOne(a => a.Category)
                .WithMany(c => c.Articles)
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}