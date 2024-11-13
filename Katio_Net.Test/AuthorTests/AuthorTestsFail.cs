using NSubstitute;
using katio.Data;
using katio.Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using katio.Business.Interfaces;
using katio.Business.Services;
using System.Linq.Expressions;
using NSubstitute.ExceptionExtensions;

namespace katio.Test.AuthorTests;

[TestClass]
public class AuthorTestsFail
{
    private readonly IRepository<int, Author> _authorRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthorService _authorService;
    private List<Author> _authors;


    public AuthorTestsFail()
    {
        _authorRepository = Substitute.For<IRepository<int, Author>>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _unitOfWork.AuthorRepository.Returns(_authorRepository);
        _authorService = new AuthorService(_unitOfWork);

        _authors = new List<Author>()
        {
            new Author 
            {
                Name = "Gabriel",
                LastName = "García Márquez",
                Country = "Colombia",
                BirthDate = new DateOnly(1940, 03, 03)
            },
            new Author 
            {
                Name = "Jorge",
                LastName = "Isaacs",
                Country = "Colombia",
                BirthDate = new DateOnly(1836, 04, 01)            
            }
        };
    }
    //TEST FALLAR | AUTHORES

    // Test para crear authores
    [TestMethod]
    public async Task CreateAuthorFail()
    {
        // Arrange
        var existingAuthor = new Author
        {
            Name = "Gabriel",
            LastName = "García Márquez",
            Country = "Colombia",
            BirthDate = new DateOnly(1940, 03, 03)
        };
        var newAuthor = new Author
        {
            Name = "Gabriel",
            LastName = "García Márquez",
            Country = "Colombia",
            BirthDate = new DateOnly(1940, 03, 03)
        };
        _authorRepository.GetAllAsync(Arg.Any<Expression<Func<Author, bool>>>()).ReturnsForAnyArgs(new List<Author> { existingAuthor });

        // Act
        var result = await _authorService.CreateAuthor(newAuthor);

        // Assert
        Assert.IsFalse(result.ResponseElements.Any());
    }

     // Test para actualizar author
    [TestMethod]
    public async Task UpdateAuthorFail()
    {
        // Arrange
        _authorRepository.Update(Arg.Any<Author>()).ThrowsAsyncForAnyArgs(new Exception());
        _unitOfWork.AuthorRepository.Returns(_authorRepository);

        // Act
        var result = await _authorService.UpdateAuthor(new Author());

        // Assert
        Assert.IsFalse(result.ResponseElements.Any());
    }

     // Test para eliminar author
    [TestMethod]
    public async Task DeleteAuthorFail()
    {
        // Arrange
        var authorToDelete = _authors.First();
        _authorRepository.FindAsync(authorToDelete.Id).ReturnsForAnyArgs(Task.FromResult<Author>(null));

        // Act
        var result = await _authorService.DeleteAuthor(authorToDelete.Id);

        // Assert
        Assert.IsFalse(result.ResponseElements.Any());
    }


   // Test para traer todos los authores
    [TestMethod]
    public async Task GetAllAuthorsFail()
    {
        // Arrange
        _authorRepository.GetAllAsync().ReturnsForAnyArgs(new List<Author>());

        // Act
        var result = await _authorService.Index();

        // Assert
        Assert.IsFalse(result.ResponseElements.Any());
    }

     // Test para traer author por id 
    [TestMethod]
    public async Task GetAuthorByIdFail()
    {
        // Arrange
        var author = _authors.First();
        _authorRepository.FindAsync(author.Id).ReturnsForAnyArgs(Task.FromResult<Author>(null));

        // Act
        var result = await _authorService.GetAuthorById(author.Id);

        // Assert
        Assert.IsFalse(result.ResponseElements.Any());
    }

     // Test para traer author por nombre
    [TestMethod]
    public async Task GetAuthorByNameFail()
    {
        // Arrange
        var author = _authors.First();
        _authorRepository.GetAllAsync(Arg.Any<Expression<Func<Author, bool>>>()).ReturnsForAnyArgs(new List<Author>());

        // Act
        var result = await _authorService.GetAuthorsByName(author.Name);

        // Assert
        Assert.IsFalse(result.ResponseElements.Any());
    }

    // Test para traer author por apellido 
    [TestMethod]
    public async Task GetAuthorByLastNameFail()
    {
        // Arrange
        var author = _authors.First();
        _authorRepository.GetAllAsync(Arg.Any<Expression<Func<Author, bool>>>()).ReturnsForAnyArgs(new List<Author>());

        // Act
        var result = await _authorService.GetAuthorsByLastName(author.LastName);

        // Assert
        Assert.IsFalse(result.ResponseElements.Any());
    }

    // Test para traer author por fecha de nacimiento 
    [TestMethod]
    public async Task GetAuthorsByBirthDateFail()
    {
        // Arrange
        var startDate = new DateOnly(1830, 01, 01);
        var endDate = new DateOnly(1950, 12, 31);
        _authorRepository.GetAllAsync(Arg.Any<Expression<Func<Author, bool>>>()).ReturnsForAnyArgs(new List<Author>());

        // Act
        var result = await _authorService.GetAuthorsByBirthDate(startDate, endDate);

        // Assert
        Assert.IsFalse(result.ResponseElements.Any());
    }
    
     // Test para traer author por pais
    [TestMethod]
    public async Task GetAuthorByCountryFail()
    {
        // Arrange
        var author = _authors.First();
        _authorRepository.GetAllAsync(Arg.Any<Expression<Func<Author, bool>>>()).ReturnsForAnyArgs(new List<Author>());

        // Act
        var result = await _authorService.GetAuthorsByCountry(author.Country);

        // Assert
        Assert.IsFalse(result.ResponseElements.Any());
    }
}