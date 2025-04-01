using CleanArchitecture.Advanced.Api.Domain.Entities;
using CleanArchitecture.Advanced.Api.Infrastructure.Context;

namespace CleanArchitecture.Advanced.Api.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Seed(LibraryContext context)
        {
            if (!context.Libraries.Any())
            {
                var library1 = new Library
                {
                    Name = "Central Library",
                    Address = "123 Main St"
                };
                var library2 = new Library
                {
                    Name = "Westside Library",
                    Address = "456 West St"
                };

                context.Libraries.AddRange(library1, library2);
                context.SaveChanges();
            }

            if (!context.Authors.Any())
            {
                var author1 = new Author
                {
                    FirstName = "George",
                    LastName = "Orwell"
                };
                var author2 = new Author
                {
                    FirstName = "Harper",
                    LastName = "Lee"
                };
                var author3 = new Author
                {
                    FirstName = "J.K.",
                    LastName = "Rowling"
                };

                context.Authors.AddRange(author1, author2, author3);
                context.SaveChanges();
            }

            if (!context.Books.Any())
            {
                var centralLibraryId = context.Libraries.First(l => l.Name == "Central Library").Id;
                var westsideLibraryId = context.Libraries.First(l => l.Name == "Westside Library").Id;

                var georgeOrwellId = context.Authors.First(a => a.LastName == "Orwell").Id;

                var book1 = new Book
                {
                    Title = "1984",
                    PublishedYear = 1949,
                    LibraryId = centralLibraryId,
                    AuthorId = georgeOrwellId
                };
                var book2 = new Book
                {
                    Title = "Animal Farm",
                    PublishedYear = 1945,
                    LibraryId = centralLibraryId,
                    AuthorId = georgeOrwellId
                };
                var book3 = new Book
                {
                    Title = "To Kill a Mockingbird",
                    PublishedYear = 1960,
                    LibraryId = westsideLibraryId,
                    AuthorId = context.Authors.First(a => a.LastName == "Lee").Id
                };
                var book4 = new Book
                {
                    Title = "Harry Potter and the Sorcerer's Stone",
                    PublishedYear = 1997,
                    LibraryId = centralLibraryId,
                    AuthorId = context.Authors.First(a => a.LastName == "Rowling").Id
                };

                context.Books.AddRange(book1, book2, book3, book4);
                context.SaveChanges();
            }

            if (!context.Borrowers.Any())
            {
                var borrower1 = new Borrower
                {
                    FirstName = "John",
                    LastName = "Doe"
                };
                var borrower2 = new Borrower
                {
                    FirstName = "Jane",
                    LastName = "Smith"
                };
                var borrower3 = new Borrower
                {
                    FirstName = "Peter",
                    LastName = "Parker"
                };

                context.Borrowers.AddRange(borrower1, borrower2, borrower3);
                context.SaveChanges();
            }

            if (!context.Loans.Any())
            {
                var bookId = context.Books.First(b => b.Title == "1984").Id;
                var borrowerId = context.Borrowers.First(b => b.LastName == "Doe").Id;

                context.Loans.Add(new Loan
                {
                    BorrowDate = DateTime.Now,
                    BookId = bookId,
                    BorrowerId = borrowerId
                });

                var bookId2 = context.Books.First(b => b.Title == "To Kill a Mockingbird").Id;
                var borrowerId2 = context.Borrowers.First(b => b.LastName == "Smith").Id;

                context.Loans.Add(new Loan
                {
                    BorrowDate = DateTime.Now.AddDays(-3),
                    ReturnDate = DateTime.Now,
                    BookId = bookId2,
                    BorrowerId = borrowerId2
                });

                var bookId3 = context.Books.First(b => b.Title == "Harry Potter and the Sorcerer's Stone").Id;
                var borrowerId3 = context.Borrowers.First(b => b.LastName == "Parker").Id;

                context.Loans.Add(new Loan
                {
                    BorrowDate = DateTime.Now.AddDays(-5),
                    BookId = bookId3,
                    BorrowerId = borrowerId3
                });

                context.SaveChanges();
            }
        }
    }
}
