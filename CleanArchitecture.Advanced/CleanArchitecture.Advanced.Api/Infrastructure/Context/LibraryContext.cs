using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Advanced.Api.Domain.Entities;

namespace CleanArchitecture.Advanced.Api.Infrastructure.Context
{
    public partial class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {

        }

        public DbSet<Library> Libraries { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<EventLog> EventLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
            {
                return;
            }

            modelBuilder.Entity<Library>(entity =>
            {
                entity.Navigation(l => l.Books)
                      .AutoInclude();

                // One-to-many relationship: Library -> Books
                entity.HasMany(l => l.Books)
                      .WithOne(b => b.Library)
                      .HasForeignKey(b => b.LibraryId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasIndex(b => b.Title)
                      .HasDatabaseName("Index_BookTitle");

                entity.Navigation(b => b.Author)
                      .AutoInclude();

                entity.Navigation(b => b.Loans)
                      .AutoInclude();

                // One-to-many relationship: Book -> Loans
                entity.HasMany(b => b.Loans)
                      .WithOne(l => l.Book)
                      .HasForeignKey(l => l.BookId);
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasIndex(a => new { a.FirstName, a.LastName })
                      .HasDatabaseName("Index_AuthorFullName");

                // One-to-many relationship: Author -> Books
                entity.HasMany(a => a.Books)
                      .WithOne(b => b.Author)
                      .HasForeignKey(b => b.AuthorId);
            });

            modelBuilder.Entity<Borrower>(entity =>
            {
                // One-to-many relationship: Borrower -> Loans
                entity.HasMany(b => b.Loans)
                      .WithOne(l => l.Borrower)
                      .HasForeignKey(l => l.BorrowerId);
            });

            modelBuilder.Entity<Loan>(entity =>
            {
                entity.Navigation(l => l.Borrower)
                      .AutoInclude();
            });
        }
    }
}
