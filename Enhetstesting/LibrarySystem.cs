using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enhetstesting
{
    public class LibrarySystem
    {
        private List<Book> books;

        public LibrarySystem()
        {
            books = new List<Book>();
            // Add some initial books
            books.Add(new Book("1984", "George Orwell", "9780451524935", 1949));
            books.Add(new Book("To Kill a Mockingbird", "Harper Lee", "9780061120084", 1960));
            books.Add(new Book("The Great Gatsby", "F. Scott Fitzgerald", "9780743273565", 1925));
            books.Add(new Book("The Hobbit", "J.R.R. Tolkien", "9780547928227", 1937));
            books.Add(new Book("Pride and Prejudice", "Jane Austen", "9780141439518", 1813));
            books.Add(new Book("The Catcher in the Rye", "J.D. Salinger", "9780316769488", 1951));
            books.Add(new Book("Lord of the Flies", "William Golding", "9780399501487", 1954));
            books.Add(new Book("Brave New World", "Aldous Huxley", "9780060850524", 1932));
            // books.Add(new Book("Häst, det är Rimligt", "Petter Boström", "1234567890123", 2025));   // example book added
        }

        public bool AddBook(Book book)
        {
            if (SearchByISBN(book.ISBN) != null)
            {
                return false;                       // Book with the same ISBN already exists
            }
            books.Add(book);
            return true;
        }

        public bool RemoveBook(string isbn)
        {
            Book book = SearchByISBN(isbn);
            if (book != null)
            {
                books.Remove(book);
                return true;
            }
            return false;
        }

        public Book SearchByISBN(string isbn)
        {
            return books.FirstOrDefault(b => b.ISBN == isbn);
        }

        public List<Book> SearchByTitle(string title)
        {
            return books
                .Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
        .       ToList();
        }

        public List<Book> SearchByAuthor(string author)
        {
            return books.Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public bool BorrowBook(string isbn)
        {
            Book book = SearchByISBN(isbn);
            if (book != null && !book.IsBorrowed)
            {
                book.IsBorrowed = true;
                book.BorrowDate = DateTime.Now;
                return true;
            }
            return false;
        }

        public bool ReturnBook(string isbn)
        {
            Book book = SearchByISBN(isbn);
            if (book != null && book.IsBorrowed)
            {
                book.IsBorrowed = false;
                book.BorrowDate = null;                 // Reset borrow date
                return true;
            }
            return false;
        }

        public List<Book> GetAllBooks()
        {
            return books;
        }

        public decimal CalculateLateFee(string isbn, int loanPeriod, decimal dailyLateFee)
        {
            Book book = SearchByISBN(isbn);
            if (book == null || book.BorrowDate == null)
                return 0;

            // Count how many days the book has been borrowed
            int daysBorrowed = (DateTime.Today - book.BorrowDate.Value).Days;
            int daysLate = daysBorrowed - loanPeriod;

            if (daysLate <= 0)
                return 0;

            return daysLate * dailyLateFee;
        }

        public bool IsBookOverdue(string isbn, int loanPeriodDays)
        {
            Book book = SearchByISBN(isbn);
            if (book != null && book.IsBorrowed && book.BorrowDate.HasValue)
            {
                TimeSpan borrowedFor = DateTime.Now - book.BorrowDate.Value;
                return borrowedFor.Days > loanPeriodDays;
            }
            return false;
        }
    }
}
