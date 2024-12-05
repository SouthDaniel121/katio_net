using NSubstitute;
using katio.Data;
using katio.Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using katio.Business.Interfaces;
using katio.Business.Services;
using System.Linq.Expressions;

namespace katio.Test.BookTests;

[TestClass]
public class BookTests
{
    // Variables
    private readonly IRepository<int, Book> _bookRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBookService _bookService;
    private List<Book> _books;

    // Constructor
    public BookTests()
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
    // Test para traer todos los libros

     [TestMethod]
    public async Task GetAllBooks()
    {
       // Arrange
        _bookRepository.GetAllAsync().Returns(_books);

        // Act
        var result = await _bookService.Index();

        // Assert
        Assert.IsTrue(result.ResponseElements.Any());
    }


    // Test para crear libros
    [TestMethod]
    public async Task CreateBook()
    {
        // Arrange
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
        _bookRepository.GetAllAsync(Arg.Any<Expression<Func<Book, bool>>>()).Returns(new List<Book>());
        _bookRepository.AddAsync(newBook).Returns(Task.CompletedTask);

        // Act
        var result = await _bookService.CreateBook(newBook);

        // Assert
        Assert.IsTrue(result.ResponseElements.Any());
    }
    // Test para actualizar libro
    [TestMethod]
    public async Task UpdateBook()
    {
        // Arrange
        var bookToUpdate = _books.First();
        _bookRepository.FindAsync(bookToUpdate.Id).Returns(bookToUpdate);

        var updatedBook = new Book
        {
            Id = bookToUpdate.Id,
            Name = "Cien años de soledad (Edición Actualizada)",
            ISBN10 = "8420471836",
            ISBN13 = "978-8420471839",
            Published = new DateOnly(1967, 06, 05),
            Edition = "Edición Académica Actualizada",
            DeweyIndex = "800",
            AuthorId = 1
        };
        _bookRepository.FindAsync(updatedBook.Id).Returns(updatedBook);
        _bookRepository.Update(updatedBook).Returns(Task.CompletedTask);

        // Act
        var result = await _bookService.UpdateBook(updatedBook);

        // Assert
        Assert.IsTrue(result.ResponseElements.Any());
    }


    // Test para eliminar libro
    [TestMethod]
    public async Task DeleteBook()
    {
        // Arrange
        var bookToDelete = _books.First();
        _bookRepository.FindAsync(bookToDelete.Id).Returns(bookToDelete);
        _bookRepository.Delete(bookToDelete).Returns(Task.CompletedTask);

        // Act
        var result = await _bookService.DeleteBook(bookToDelete.Id);

        // Assert
        Assert.IsTrue(result.ResponseElements.Any());
    }

    // Test para traer por Id libro
    [TestMethod]
    public async Task GetBookById()
    {
        // Arrange
        var book = _books.First();
        _bookRepository.FindAsync(book.Id).Returns(book);

        // Act
        var result = await _bookService.GetBookById(book.Id);

        // Assert
        Assert.IsTrue(result.ResponseElements.Any());
    }

    // Test para traer libro por nombre
    [TestMethod]
    public async Task GetBookByName()
    {
        // Arrange
        var book = _books.First();
        _bookRepository.GetAllAsync(Arg.Any<Expression<Func<Book, bool>>>()).Returns(new List<Book> { book });

        // Act
        var result = await _bookService.GetBooksByName(book.Name);

        // Assert
        Assert.IsTrue(result.ResponseElements.Any());
    }

    // Test para traer libro por isbn10
    [TestMethod]
    public async Task GetBooksByISBN10()
    {
        // Arrange
        var book = _books.First();
        _bookRepository.GetAllAsync(Arg.Any<Expression<Func<Book, bool>>>()).Returns(new List<Book> { book });

        // Act
        var result = await _bookService.GetBooksByISBN10(book.ISBN10);

        // Assert
        Assert.IsTrue(result.ResponseElements.Any());
    }

    // Test para traer libro por Isbn13
    [TestMethod]
    public async Task GetBooksByISBN13()
    {
        // Arrange
        var book = _books.First();
        _bookRepository.GetAllAsync(Arg.Any<Expression<Func<Book, bool>>>()).Returns(new List<Book> { book });

        // Act
        var result = await _bookService.GetBooksByISBN13(book.ISBN13);

        // Assert
        Assert.IsTrue(result.ResponseElements.Any());
    }

    // Test para traer libro por rango fecha de publicacion
    [TestMethod]
    public async Task GetBooksByPublishedDateRange()
    {
        // Arrange
        var startDate = new DateOnly(1960, 01, 01);
        var endDate = new DateOnly(1970, 12, 31);
        var expectedBooks = _books.Where(b => b.Published >= startDate && b.Published <= endDate).ToList();
        _bookRepository.GetAllAsync(Arg.Any<Expression<Func<Book, bool>>>()).Returns(expectedBooks);

        // Act
        var result = await _bookService.GetBooksByPublished(startDate, endDate);

        // Assert
        Assert.IsTrue(result.ResponseElements.Any());
    }

    // Test para traer libros por edicion
    [TestMethod]
    public async Task GetBooksByEdition()
    {
        // Arrange
        var book = _books.First();
        _bookRepository.GetAllAsync(Arg.Any<Expression<Func<Book, bool>>>()).Returns(new List<Book> { book });

        // Act
        var result = await _bookService.GetBooksByEdition(book.Edition);

        // Assert
        Assert.IsTrue(result.ResponseElements.Any());
    }

    // Test para traer libros por DeweyIndex
    [TestMethod]
    public async Task GetBooksByDeweyIndex()
    {
        // Arrange
        var book = _books.First();
        _bookRepository.GetAllAsync(Arg.Any<Expression<Func<Book, bool>>>()).Returns(new List<Book> { book });

        // Act
        var result = await _bookService.GetBooksByDeweyIndex(book.DeweyIndex);

        // Assert
        Assert.IsTrue(result.ResponseElements.Any());
    }

    // Test para traer libro por id del author
    [TestMethod]
    public async Task GetBooksByAuthorId()
    {
        // Arrange
        var book = _books.First();
        _bookRepository.GetAllAsync(Arg.Any<Expression<Func<Book, bool>>>(), includeProperties: "Author").Returns(new List<Book> { book });

        // Act
        var result = await _bookService.GetBookByAuthorAsync(book.AuthorId);

        // Assert
        Assert.IsTrue(result.ResponseElements.Any());
    }

    // Test para traer libro por nombre de autor
    [TestMethod]
    public async Task GetBooksByAuthorName()
    {
        // Arrange
        var book = _books.First();
        _bookRepository.GetAllAsync(Arg.Any<Expression<Func<Book, bool>>>(), includeProperties: "Author").Returns(new List<Book> { book });

        // Act
        var result = await _bookService.GetBookByAuthorNameAsync(book.Name);

        // Assert
        Assert.IsTrue(result.ResponseElements.Any());
    }
    //  Test para traer por apellido de author
    [TestMethod]
    public async Task GetBooksByAuthorLastName()
    {
        // Arrange
        var book = _books.First();
        _bookRepository.GetAllAsync(Arg.Any<Expression<Func<Book, bool>>>(), includeProperties: "Author").Returns(new List<Book> { book });

        // Act
        var result = await _bookService.GetBookByAuthorLastNameAsync("Garcia Marquez");

        // Assert
        Assert.IsTrue(result.ResponseElements.Any());
    }

    //Test para traer por pais de author
    [TestMethod]
    public async Task GetBooksByAuthorCountry()
    {
        // Arrange
        var book = _books.First();
        _bookRepository.GetAllAsync(Arg.Any<Expression<Func<Book, bool>>>(), includeProperties: "Author").Returns(new List<Book> { book });

        // Act
        var result = await _bookService.GetBookByAuthorCountryAsync("Colombia");

        // Assert
        Assert.IsTrue(result.ResponseElements.Any());
    }

    // Test para traer por nombre completo de author
    [TestMethod]
    public async Task GetBooksByAuthorFullName()
    {
        // Arrange
        var book = _books.First();
        _bookRepository.GetAllAsync(Arg.Any<Expression<Func<Book, bool>>>(), includeProperties: "Author").Returns(new List<Book> { book });

        // Act
        var result = await _bookService.GetBookByAuthorFullNameAsync("Gabriel", "García Márquez");

        // Assert
        Assert.IsTrue(result.ResponseElements.Any());
    }

    // Test para traer todos los authores en un rango de nacimiento
    [TestMethod]
    public async Task GetBooksByAuthorBirthDateRange()
    {
        // Arrange
        var startDate = new DateOnly(1800, 01, 01);
        var endDate = new DateOnly(2000, 12, 31);
        _bookRepository.GetAllAsync(Arg.Any<Expression<Func<Book, bool>>>(), includeProperties: "Author").Returns(_books);

        // Act
        var result = await _bookService.GetBookByAuthorBirthDateRange(startDate, endDate);

        // Assert
        Assert.IsTrue(result.ResponseElements.Any());
    }
}