using NSubstitute;
using katio.Data;
using katio.Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using katio.Business.Interfaces;
using katio.Business.Services;
using System.Linq.Expressions;
using System.Net;
using NSubstitute.ExceptionExtensions;

namespace katio.Test.BookTests;

[TestClass]
public class BookTestsFail
{
    // Variables
    private readonly IRepository<int, Book> _bookRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBookService _bookService;
    private List<Book> _books;

    // Constructor
    public BookTestsFail()
    {
        _bookRepository = Substitute.For<IRepository<int, Book>>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _unitOfWork.BookRepository.Returns(_bookRepository);
        _bookService = new BookService(_unitOfWork);

        _books = new List<Book>()
        {
            new Book
            {
                Name = "Cien años de soledad",
                ISBN10 = "8420471836",
                ISBN13 = "978-8420471839",
                Published = new DateOnly(1967, 06, 05),
                Edition = "RAE Obra Académica",
                DeweyIndex = "800",
                AuthorId = 1
            },
            new Book
            {
                Name = "Huellas",
                ISBN10 = "9584277278",
                ISBN13 = "978-958427275",
                Published = new DateOnly(2019, 01, 01),
                Edition = "1ra Edicion",
                DeweyIndex = "800",
                AuthorId = 3
            }
        };
    }

     //TEST FALLAR | LIBROS

    // Test para crear libro
    [TestMethod]
    public async Task CreateBookFail()
    {
        // Arrange
        var existingBook = new Book
        {
            Name = "El Otoño del Patriarca",
            ISBN10 = "1234567890",
            ISBN13 = "978-1234567897",
            Published = new DateOnly(1975, 03, 01),
            Edition = "Primera",
            DeweyIndex = "800",
            AuthorId = 1
        };
        var newBook = new Book
        {
            Name = "El Otoño del Patriarca",
            ISBN10 = "1234567890",
            ISBN13 = "978-1234567897",
            Published = new DateOnly(1975, 03, 01),
            Edition = "Primera",
            DeweyIndex = "800",
            AuthorId = 1
        };
        _bookRepository.GetAllAsync(Arg.Any<Expression<Func<Book, bool>>>()).ReturnsForAnyArgs(new List<Book> { existingBook });

        // Act
        var result = await _bookService.CreateBook(newBook);

        // Assert
        Assert.IsFalse(result.ResponseElements.Any());
    }

    // Test para actualizar libro 
    [TestMethod]
    public async Task UpdateBookFail()
    {
        // Arrange
        _bookRepository.Update(Arg.Any<Book>()).ThrowsAsyncForAnyArgs(new Exception());
        _unitOfWork.BookRepository.Returns(_bookRepository);

        // Act
        var result = await _bookService.UpdateBook(new Book());

        // Assert
        Assert.IsFalse(result.ResponseElements.Any());
    }

     // Test para borrar libro
    [TestMethod]
    public async Task DeleteBookFail()
    {
        // Arrange
        var bookToDelete = _books.First();
        _bookRepository.FindAsync(bookToDelete.Id).ReturnsForAnyArgs(Task.FromResult<Book>(null!));

        // Act
        var result = await _bookService.DeleteBook(bookToDelete.Id);

        // Assert
        Assert.IsFalse(result.ResponseElements.Any());
    }

    // Test para traer todos los libros
    [TestMethod]
    public async Task GetAllBooksFail()
    {
        // Arrange
        _bookRepository.GetAllAsync().ReturnsForAnyArgs(new List<Book>());

        // Act
        var result = await _bookService.Index();

        // Assert
        Assert.IsFalse(result.ResponseElements.Any());
    }

    // Test para traer libro por id 
    [TestMethod]
    public async Task GetBookByIdFail()
    {
        // Arrange
        var book = _books.First();
        _bookRepository.FindAsync(book.Id).ReturnsForAnyArgs(Task.FromResult<Book>(null!));

        // Act
        var result = await _bookService.GetBookById(book.Id);

        // Assert
        Assert.IsFalse(result.ResponseElements.Any());
    }

    // Test para traer libro por nombre
    [TestMethod]
    public async Task GetBookByNameFail()
    {
        // Arrange
        var book = _books.First();
        _bookRepository.GetAllAsync(Arg.Any<Expression<Func<Book, bool>>>()).Returns(new List<Book>());

        // Act
        var result = await _bookService.GetBooksByName(book.Name);

        // Assert
        Assert.IsFalse(result.ResponseElements.Any());
    }

   // Test para traer libro por isbn10
    [TestMethod]
    public async Task GetBooksByISBN10Fail()
    {
        // Arrange
        var book = _books.First();
        _bookRepository.GetAllAsync(Arg.Any<Expression<Func<Book, bool>>>()).Returns(new List<Book>());
        // Act
        var result = await _bookService.GetBooksByISBN10(book.ISBN10);

        // Assert
        Assert.IsFalse(result.ResponseElements.Any());
    }

    // Test para traer libro por ISBN13
    [TestMethod]
    public async Task GetBooksByISBN13Fail()
    {
        // Arrange
        var book = _books.First();
        _bookRepository.GetAllAsync(Arg.Any<Expression<Func<Book, bool>>>()).Returns(new List<Book>());

        // Act
        var result = await _bookService.GetBooksByISBN13(book.ISBN13);

        // Assert
        Assert.IsFalse(result.ResponseElements.Any());
    }

    // Test para traer libro por edicion
    [TestMethod]
    public async Task GetBooksByEditionFail()
    {
        // Arrange
        var book = _books.First();
        _bookRepository.GetAllAsync(Arg.Any<Expression<Func<Book, bool>>>()).Returns(new List<Book>());

        // Act
        var result = await _bookService.GetBooksByEdition(book.Edition);

        // Assert
        Assert.IsFalse(result.ResponseElements.Any());
    }

     // Test para traer libro por rango de publicacion 
    [TestMethod]
    public async Task GetBooksByPublishedDateRangeFail()
    {
        // Arrange
        var startDate = new DateOnly(1960, 01, 01);
        var endDate = new DateOnly(1970, 12, 31);
        _bookRepository.GetAllAsync(Arg.Any<Expression<Func<Book, bool>>>()).Returns(new List<Book>());

        // Act
        var result = await _bookService.GetBooksByPublished(startDate, endDate);

        // Assert
        Assert.IsFalse(result.ResponseElements.Any());
    }

    // Test para traer libro por DeweyIndex
    [TestMethod]
    public async Task GetBooksByDeweyIndexFail()
    {
        // Arrange
        var book = _books.First();
        _bookRepository.GetAllAsync(Arg.Any<Expression<Func<Book, bool>>>()).Returns(new List<Book>());

        // Act
        var result = await _bookService.GetBooksByDeweyIndex(book.DeweyIndex);

        // Assert
        Assert.IsFalse(result.ResponseElements.Any());
    }

    // Test para traer libros por author id
    [TestMethod]
    public async Task GetBooksByAuthorIdFail()
    {
        // Arrange
        var book = _books.First();
        _bookRepository.GetAllAsync(Arg.Any<Expression<Func<Book, bool>>>(), includeProperties: "Author").Returns(new List<Book>());

        // Act
        var result = await _bookService.GetBookByAuthorAsync(book.AuthorId);

        // Assert
        Assert.IsFalse(result.ResponseElements.Any());
    }
    
    // Test para traer libros por el nombre completo del author
    [TestMethod]
    public async Task GetBooksByAuthorNameFail()
    {
        // Arrange
        var book = _books.First();
        _bookRepository.GetAllAsync(Arg.Any<Expression<Func<Book, bool>>>(), includeProperties: "Author").Returns(new List<Book>());

        // Act
        var result = await _bookService.GetBookByAuthorNameAsync(book.Author?.Name ?? string.Empty);

        // Assert
        Assert.IsFalse(result.ResponseElements.Any());
    }

     // Test para traer libro por apellido del author
     [TestMethod]
     public async Task GetBooksByAuthorLastNameFail()
     {
         // Arrange
         var book = _books.First();
         _bookRepository.GetAllAsync(Arg.Any<Expression<Func<Book, bool>>>(), includeProperties: "Author").Returns(new List<Book>());

         // Act
         var result = await _bookService.GetBookByAuthorLastNameAsync("Garcia Marquez");

         // Assert
         Assert.IsFalse(result.ResponseElements.Any());
     }

    // Test para traer libro por pais de author
    [TestMethod]
    public async Task GetBooksByAuthorCountryFail()
    {
        // Arrange
        var book = _books.First();
        _bookRepository.GetAllAsync(Arg.Any<Expression<Func<Book, bool>>>(), includeProperties: "Author").Returns(new List<Book>());

        // Act
        var result = await _bookService.GetBookByAuthorCountryAsync(book.Author?.Country ?? string.Empty);

        // Assert
        Assert.IsFalse(result.ResponseElements.Any());
    }

     // Test para traer libros por nombre completo de author
    [TestMethod]
    public async Task GetBooksByAuthorFullNameFail()
    {
        // Arrange
        var book = _books.First();
        _bookRepository.GetAllAsync(Arg.Any<Expression<Func<Book, bool>>>(), includeProperties: "Author").Returns(new List<Book>());

        // Act
        var result = await _bookService.GetBookByAuthorFullNameAsync(book.Author?.Name ?? string.Empty, book.Author?.LastName ?? string.Empty);

        // Assert
        Assert.IsFalse(result.ResponseElements.Any());
    }

    // Test para traer libros por rango de fecha de nacimiento del author
    [TestMethod]
    public async Task GetBooksByAuthorBirthDateRangeFail()
    {
        // Arrange
        var startDate = new DateOnly(1800, 01, 01);
        var endDate = new DateOnly(2000, 12, 31);
        _bookRepository.GetAllAsync(Arg.Any<Expression<Func<Book, bool>>>(), includeProperties: "Author").Returns(new List<Book>());

        // Act
        var result = await _bookService.GetBookByAuthorBirthDateRange(startDate, endDate);

        // Assert
        Assert.IsFalse(result.ResponseElements.Any());
    }
}