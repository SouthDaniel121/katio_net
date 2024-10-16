using NSubstitute;

using katio.Data;
using katio.Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using katio.Business.Interfaces;
using katio.Business.Services;
using System.Linq.Expressions;
using katio.Data.Dto;
using System.Net;

namespace katio.Test;



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
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.ResponseElements.Count());
    }
}
