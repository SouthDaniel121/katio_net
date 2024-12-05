using NSubstitute;
using katio.Data;
using katio.Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using katio.Business.Interfaces;
using katio.Business.Services;
using System.Linq.Expressions;
using System.Net;

namespace katio.Test.AuthorTests;

[TestClass]
public class AuthorTestsException
{
    private readonly IRepository<int, Author> _authorRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthorService _authorService;
    private List<Author> _authors;

    public AuthorTestsException()
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

    //Test Exception  | Authores

    // test para crear autores    
    [TestMethod]
    public async Task CreateAuthorRepositoryException()
    {
        // Arrange
        var newAuthor = new Author
        {
            Name = "Gabriel",
            LastName = "García Márquez",
            Country = "Colombia",
            BirthDate = new DateOnly(1940, 03, 03)
        };
        _authorRepository.GetAllAsync(Arg.Any<Expression<Func<Author, bool>>>()).Returns(new List<Author>());
        _authorRepository.When(x => x.AddAsync(Arg.Any<Author>())).Do(x => throw new Exception("Repository error"));

        // Act
        var result = await _authorService.CreateAuthor(newAuthor);

        // Assert
        Assert.AreEqual((int)result.StatusCode, 500);
    }


    // Test para actualizar author    
     // Test for updating author
    [TestMethod]
    public async Task UpdateAuthor()
    {
        // Arrange
        var existingAuthor = new Author
        {
            Id = 1,
            Name = "Gabriel",
            LastName = "García Márquez",
            Country = "Colombia",
            BirthDate = new DateOnly(1927, 03, 06)
        };

        var updatedAuthor = new Author
        {
            Id = 1,
            Name = "Gabriel Updated",
            LastName = "García Márquez Updated",
            Country = "Colombia Updated",
            BirthDate = new DateOnly(1950, 01, 01)
        };

        _unitOfWork.AuthorRepository.GetAllAsync(Arg.Any<Expression<Func<Author, bool>>>())
            .Returns(new List<Author> { existingAuthor });

        _unitOfWork.AuthorRepository.Update(updatedAuthor).Returns(Task.CompletedTask);
        _unitOfWork.SaveAsync().Returns(Task.CompletedTask);

        // Act
        var result = await _authorService.UpdateAuthor(updatedAuthor);

        // Assert
        Assert.AreEqual((int)result.StatusCode, 200); 
    }

    // Test para borrar autor
    [TestMethod]
    public async Task DeleteAuthor()
    {
                // Arrange
        var authorToDelete = _authors.First(); 

        _authorRepository.GetAllAsync(Arg.Any<Expression<Func<Author, bool>>>())
        .Returns(Task.FromResult(new List<Author> { authorToDelete }));

        _authorRepository.Delete(authorToDelete.Id).Returns(Task.CompletedTask);

        // Act
        var result = await _authorService.DeleteAuthor(authorToDelete.Id);

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode); 
    }

       // Test para traer autores por id
    [TestMethod]
    public async Task GetAuthorByIdRepositoryException()
    {
        // Arrange
        var author = _authors.First();
        _authorRepository.When(x => x.FindAsync(author.Id)).Do(x => throw new Exception("Repository error"));

        // Act
        var result = await _authorService.GetAuthorById(author.Id);

        // Assert
        Assert.AreEqual((int)result.StatusCode, 500);
    }
    
    // Test para traer authores por nombre
    [TestMethod]
    public async Task GetAuthorByNameRepositoryException()
    {
        // Arrange
        var author = _authors.First();
        _authorRepository.When(x => x.GetAllAsync(Arg.Any<Expression<Func<Author, bool>>>())).Do(x => throw new Exception("Repository error"));

        // Act
        var result = await _authorService.GetAuthorsByName(author.Name);

        // Assert
        Assert.AreEqual((int)result.StatusCode, 500);
    }

     // Test para traer authores por apellido    
    [TestMethod]
    public async Task GetAuthorByLastNameRepositoryException()
    {
        // Arrange
        var author = _authors.First();
        _authorRepository.When(x => x.GetAllAsync(Arg.Any<Expression<Func<Author, bool>>>())).Do(x => throw new Exception("Repository error"));

        // Act
        var result = await _authorService.GetAuthorsByLastName(author.LastName);

        // Assert
        Assert.AreEqual((int)result.StatusCode, 500);
    }
    // Test Para traer authores por fecha de nacimiento
    [TestMethod]
    public async Task GetAuthorsByBirthDateRepositoryException()
    {
        // Arrange
        var startDate = new DateOnly(1830, 01, 01);
        var endDate = new DateOnly(1950, 12, 31);
        _authorRepository.When(x => x.GetAllAsync(Arg.Any<Expression<Func<Author, bool>>>())).Do(x => throw new Exception("Repository error"));

        // Act
        var result = await _authorService.GetAuthorsByBirthDate(startDate, endDate);

        // Assert
        Assert.AreEqual((int)result.StatusCode, 500);
    }

     // Test para traer authores por pais
    [TestMethod]
    public async Task GetAuthorByCountryRepositoryException()
    {
        // Arrange
        var author = _authors.First();
        _authorRepository.When(x => x.GetAllAsync(Arg.Any<Expression<Func<Author, bool>>>())).Do(x => throw new Exception("Repository error"));

        // Act
        var result = await _authorService.GetAuthorsByCountry(author.Country);

        // Assert
        Assert.AreEqual((int)result.StatusCode, 500);
    }
}