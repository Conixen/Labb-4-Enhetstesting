namespace Enhetstesting.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Enhetstesting;
    /// <summary>
    /// This is a test class for LibrarySystem and is intended
    /// to contain all LibrarySystem Unit Tests.
    /// </summary>
    [TestClass]
    public class LibrarySystemTests
    {
        private LibrarySystem librarySystem;
     

        [TestInitialize]
        public void Setup()
        {
            librarySystem = new LibrarySystem();
        }

        // --------------- - Adding Books ---------------
        [TestMethod]
        public void Add_NewBook_Tobooks()
        {
            // Arrange
            Book newBook = new Book("New Book", "New Author", "1234567890", 2023);
            // Act
            bool result = librarySystem.AddBook(newBook);
            // Assert
            Assert.IsTrue(result, "Failed to add a new book.");
        }

        // --------------- - Removing Books ---------------
        [TestMethod]
        public void Delete_HästRimligBook_byISBN_Frombooks()
        {
            // Given book to remove
            string isbnToRemove = "1234567890123"; // ISBN of "Häst, det är Rimligt - Petter Boström"
            
            // When removing the book
            bool result = librarySystem.RemoveBook(isbnToRemove);
            
            // Then the book should be removed successfully
            Assert.IsFalse(result, "Failed to remove the book.");
        }

        [TestMethod]
        public void RemoveBorrowedBook()
        {
            // Given a borrowed book to delete
            string isbnToRemove = "1234567890123"; // ISBN of "Häst, det är Rimligt - Petter Boström"
            librarySystem.BorrowBook(isbnToRemove); // Borrow the book first

            // When removing the book
            bool result = librarySystem.RemoveBook(isbnToRemove);

            // Then the book should not be removed
            Assert.IsFalse(result, "Book can not be removed from the system as its already borrowed.");
        }

        // --------------- - Searching Books ---------------
        [TestMethod]
        public void Search_BookByTitle_ReturnsCorrectBook()
        {
            // Arrange
            Book book = new Book("Häst, det är Rimligt", "Petter Boström", "1234567890123", 2021);
            librarySystem.AddBook(book);

            // Act
            var result = librarySystem.SearchByTitle("Häst, det är Rimligt");

            // Assert
            Assert.IsTrue(result.Contains(book), "Book should be found with title");
        }

        [TestMethod]
        public void Search_BookByAuthor_ReturnsCorrectBook()
        {
            // Arrange
            Book book = new Book("Häst, det är Rimligt", "Petter Boström", "1234567890123", 2021);
            librarySystem.AddBook(book);

            // Act
            var result = librarySystem.SearchByAuthor("Petter Boström");

            // Assert
            Assert.IsTrue(result.Contains(book), "Book should be found with author");
        }
        [TestMethod]
        public void Search_BookByISBN_ReturnsCorrectBook()
        {
            // Arrange
            Book book = new Book("Häst, det är Rimligt", "Petter Boström", "1234567890123", 2021);
            librarySystem.AddBook(book);

            // Act
            var result = librarySystem.SearchByISBN("1234567890123");

            // Assert
            Assert.AreEqual(book, result, "Book should be found with ISBN");
        }

        [DataTestMethod]
        [DataRow("Häst, det är Rimligt")]   // Search by title
        [DataRow("Petter Boström")]         // Search by author
        [DataRow("1234567890123")]          // Search by ISBN
        public void Search_BookByVariousCriteria_ReturnsCorrectBook(string searchTerm)
        {
            // Arrange
            Book book = new Book("Häst, det är Rimligt", "Petter Boström", "1234567890123", 2021);
            librarySystem.AddBook(book);

            // Act
            var resultByTitle = librarySystem.SearchByTitle(searchTerm);
            var resultByAuthor = librarySystem.SearchByAuthor(searchTerm);
            var resultByIsbn = librarySystem.SearchByISBN(searchTerm);

            // Assert
            bool found = resultByTitle.Any(b => b.ISBN == book.ISBN)
                      || resultByAuthor.Any(b => b.ISBN == book.ISBN)
                      || (resultByIsbn != null && resultByIsbn.ISBN == book.ISBN);

            Assert.IsTrue(found, $"Book not found with search term '{searchTerm}'.");
        }

        [DataTestMethod]
        [DataRow("1985")]                   // Wrong title
        [DataRow("Qeorge Orwel")]           // Misspelled author
        [DataRow("9780451524934")]          // Wrong ISBN
        public void Search_BookByVariousCriteria_ReturnsNoBook(string searchTerm)
        {
            // Act
            var resultByTitle = librarySystem.SearchByTitle(searchTerm);
            var resultByAuthor = librarySystem.SearchByAuthor(searchTerm);
            var resultByIsbn = librarySystem.SearchByISBN(searchTerm);

            // Assert
            Assert.IsTrue(
                resultByTitle.Count == 0 &&
                resultByAuthor.Count == 0 &&
                resultByIsbn == null,
                $"Book was found with invalid search term '{searchTerm}'."
            );
        }


        // --------------- - Borrowing Books ---------------
        [TestMethod]
        public void Borrow_Book_Success()
        {
            // Given a book to borrow
            Book book = new Book("Häst, det är Rimligt", "Petter Boström", "1234567890123", 2021);
            librarySystem.AddBook(book);

            // When borrowing the book
            bool result = librarySystem.BorrowBook(book.ISBN);

            // Then the book should be marked as borrowed
            Assert.IsTrue(result, "Failed to borrow the book.");
        }
        [TestMethod]
        public void Borrow_BorrowedBook_Fail() 
        {
            // Given a book to borrow
            Book book = new Book("Häst, det är Rimligt", "Petter Boström", "1234567890123", 2021);
            librarySystem.AddBook(book);
            librarySystem.BorrowBook(book.ISBN); // Borrow the book first
            
            // When trying to borrow the same book again
            bool result = librarySystem.BorrowBook(book.ISBN);
            
            // Then the book should not be borrowed again
            Assert.IsFalse(result, "Book can not be borrowed again as its already borrowed.");
        }
        [TestMethod]
        public void Borrow_Book_BorrowedDate_Sucess() 
        {
            // Given a book to borrow
            Book book = new Book("Häst, det är Rimligt", "Petter Boström", "1234567890123", 2021);
            librarySystem.AddBook(book);

            // When borrowing the book
            bool result = librarySystem.BorrowBook(book.ISBN);

            // Then the book should be marked as borrowed and set date
            Book borrowedBook = librarySystem.SearchByISBN(book.ISBN);
            
            Assert.IsTrue(borrowedBook.IsBorrowed, "Book should be marked as borrowed.");

            Assert.IsNotNull(borrowedBook.BorrowDate, "Borrow date should be set.");
            Assert.AreEqual(DateTime.Now.Date, borrowedBook.BorrowDate.Value.Date, "Borrow date should be today.");

        }
        // --------------- Returning books ---------------
        [TestMethod]
        public void Return_Book_Success()
        {
            // Given a book to return
            Book book = new Book("Häst, det är Rimligt", "Petter Boström", "1234567890123", 2021);
            librarySystem.AddBook(book);
            
            librarySystem.BorrowBook(book.ISBN); // Borrow the book first
            
            // When returning the book
            bool result = librarySystem.ReturnBook(book.ISBN);
            
            // Then the book should be marked as not borrowed
            Assert.IsTrue(result, "Book has been returened.");

            Assert.IsFalse(book.IsBorrowed, "Book should be marked as not borrowed.");
            Assert.IsNull(book.BorrowDate, "Borrow date should be null after return.");
        }
        [TestMethod]
        public void Return_NotBorrowedBook_Fail()
        {
            // Given a book to return
            Book book = new Book("Häst, det är Rimligt", "Petter Boström", "1234567890123", 2021);
            librarySystem.AddBook(book);

            // When trying to return the book without borrowing it
            bool result = librarySystem.ReturnBook(book.ISBN);

            // Then the book should not be returned
            Assert.IsFalse(result, "Book can not be returned as its not borrowed.");
        }
        // -------------------- Delay management of books --------------------
        [TestMethod]
        public void BookDeleyed_DateOverdue()
        {
            // Arrange
            Book book = new Book("Häst, det är Rimligt", "Petter Boström", "1234567890123", 2021);
            librarySystem.AddBook(book);
            librarySystem.BorrowBook(book.ISBN);
            book.BorrowDate = DateTime.Today.AddDays(-10); // Borrowed 10 days ago
            int loanPeriod = 7;

            // Act
            bool result = librarySystem.IsBookOverdue(book.ISBN, loanPeriod);

            // Assert
            Assert.IsTrue(result, "Book should be marked as overdue.");
        }
        [TestMethod]
        public void LateFee_IsCalculatedCorrectly()
        {
            // Arrange
            Book book = new Book("Häst, det är Rimligt", "Petter Boström", "1234567890123", 2021);
            librarySystem.AddBook(book);
            librarySystem.BorrowBook(book.ISBN);
            book.BorrowDate = DateTime.Today.AddDays(-10); // Borrowed 10 days ago
            int loanPeriod = 7;           // Allowed loan period in days
            decimal dailyLateFee = 5m;    // Late fee charged per overdue day

            // Act
            decimal fee = librarySystem.CalculateLateFee(book.ISBN, loanPeriod, dailyLateFee);

            // Assert
            // 3 days overdue (10 - 7), fee should be 3 * 5 = 15
            Assert.AreEqual(15m, fee, "Late fee should be 3 days × 5kr = 15kr.");
        }

        [TestMethod]
        public void LateFee_IsZero_WhenNotOverdue()
        {
            // Arrange
            Book book = new Book("Häst, det är Rimligt", "Petter Boström", "1234567890123", 2021);
            librarySystem.AddBook(book);
            librarySystem.BorrowBook(book.ISBN);
            book.BorrowDate = DateTime.Today.AddDays(-3); // Borrowed 3 days ago
            int loanPeriod = 7;           // Allowed loan period in days
            decimal dailyLateFee = 5m;    // Late fee charged per overdue day

            // Act
            decimal fee = librarySystem.CalculateLateFee(book.ISBN, loanPeriod, dailyLateFee);

            // Assert
            // Not overdue, fee should be zero
            Assert.AreEqual(0m, fee, "No fee should be charged when the book is not overdue.");
        }

        // -------------------- Other tests --------------------
        [TestMethod]
        public void No_Duplications_ISBN() 
        {
            // Arrange
            var librarySystem = new LibrarySystem();
            Book original = new Book("Häst, det är Rimligt", "Petter Boström", "1234567890123", 2021);
            Book duplicate = new Book("New Book", "Author", "1234567890123", 2023); // Duplicate ISBN

            // Act
            bool firstAdd = librarySystem.AddBook(original);
            bool secondAdd = librarySystem.AddBook(duplicate); // Should fail

            // Assert
            //Assert.IsTrue(firstAdd);
            Assert.IsFalse(secondAdd); // ISBN already exists
        }

    }




}//## 2. Utlåningssystem

//### 2.1 Låna ut böcker

//Systemet ska hantera utlåning av böcker.

//- En bok som lånas ut ska markeras som utlånad i systemet
//- Redan utlånade böcker ska inte kunna lånas ut
//- När en bok lånas ska rätt utlåningsdatum sättas

//### 2.2 Återlämning

//Systemet ska hantera återlämning av böcker.

//- Vid återlämning ska bokens utlåningsdatum nollställas
//- Endast utlånade böcker ska kunna återlämnas

//### 2.3 Förseningshantering

//Systemet ska kunna identifiera och hantera försenade böcker.

//- Korrekt beräkning av om en bok är försenad ska implementeras
//- Förseningsavgifter ska beräknas enligt specificerad formel (förseningsavgift * antal dagar försenad)
