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

        [TestMethod]
        public void TestAddBook()
        {
            // Arrange
            Book newBook = new Book("New Book", "New Author", "1234567890", 2023);
            // Act
            bool result = librarySystem.AddBook(newBook);
            // Assert
            Assert.IsTrue(result, "Failed to add a new book.");
        }
    }
}
