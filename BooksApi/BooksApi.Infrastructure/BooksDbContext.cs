using Microsoft.EntityFrameworkCore;

namespace BooksApi.Infrastructure
{
    public class BooksDbContext : DbContext
    {
        // empty constructor for mocking
        public BooksDbContext()
        { }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserBookOpinion> UsersBookOpinions { get; set; }

        protected void ConfigureModels(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Book>()
                .HasMany(s => s.UsersBookOpinions);

            modelBuilder.Entity<Book>().HasIndex(p => p.DeletionDate)
                .HasDatabaseName($"IX_{nameof(Book)}_{nameof(Book.DeletionDate)}");

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=BooksApiDb;");
        }
    }
}
