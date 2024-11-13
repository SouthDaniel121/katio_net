using katio.Business.Interfaces;
using katio.Business.Services;
using katio.Data.Dto;
using katio.Data.Models;
using katio.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Linq.Expressions;
using System.Net;

namespace katio.Test.GenreTests;

[TestClass]
public class GenreTestsException
{
    private readonly IRepository<int, Genre> _genreRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenreService _genreService;
    private List<Genre> _genres;

    public GenreTestsException()
    {
        _genreRepository = Substitute.For<IRepository<int, Genre>>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _unitOfWork.GenreRepository.Returns(_genreRepository);
        _genreService = new GenreService(_unitOfWork);
        _genres = new List<Genre>
        {
            new Genre 
            { 
                Name = "Fantasy", 
                Description = "La Fantasia es..." 
            },
            new Genre 
            { 
                Name = "Science Fiction", 
                Description = "La Ciencia Ficcion es..." 
            }
        };
    }
 //TEST PARA EXCEPCIONES | GENEROS LITERARIOS

    // Test Para crear genero literios
    [TestMethod]
    public async Task CreateGenreRepositoryException()
    {
        // Arrange
        var newGenre = new Genre 
        { 
            Name = "Fantasy", 
            Description = "La Fantasia es..." 
        };
        _genreRepository.GetAllAsync(Arg.Any<Expression<Func<Genre, bool>>>()).Returns(new List<Genre>());
        _genreRepository.When(x => x.AddAsync(Arg.Any<Genre>())).Do(x => throw new Exception("Repository error"));

        // Act
        var result = await _genreService.CreateGenre(newGenre);

        // Assert
        Assert.AreEqual((int)result.StatusCode, 500);
    }

    // Test Para actualizar genero literario
    [TestMethod]
    public async Task UpdateGenreRepositoryException()
    {
        // Arrange
        var genreToUpdate = _genres.First();
        _genreRepository.FindAsync(genreToUpdate.Id).Returns(genreToUpdate);

        var updatedGenre = new Genre 
        { 
            Id = genreToUpdate.Id, 
            Name = "Fantasy", 
            Description = "La Fantasia es..." 
        };

        _genreRepository.FindAsync(genreToUpdate.Id).Returns(genreToUpdate);
        _genreRepository.When(x => x.Update(Arg.Any<Genre>())).Do(x => throw new Exception("Repository error"));


        // Act
        var result = await _genreService.UpdateGenre(updatedGenre);

        // Assert
        Assert.AreEqual((int)result.StatusCode, 500);
    }

    // Test para borrar genero literario
    [TestMethod]
    public async Task DeleteGenreRepositoryException()
    {
        // Arrange
        var genreToDelete = _genres.First();
        _genreRepository.FindAsync(genreToDelete.Id).Returns(genreToDelete);
        _genreRepository.When(x => x.Delete(Arg.Any<Genre>())).Do(x => throw new Exception("Repository error"));

        // Act
        var result = await _genreService.DeleteGenre(genreToDelete.Id);

        // Assert
        Assert.AreEqual((int)result.StatusCode, 500);
    }

   // Test para traer todos los generos literarios
    [TestMethod]
    public async Task GetAllGenresRepositoryException()
    {
        // Arrange
        _genreRepository.When(x => x.GetAllAsync()).Do(x => throw new Exception("Repository error"));

        // Act
        var result = await _genreService.Index();
        
        // Assert
        Assert.AreEqual((int)result.StatusCode, 500);
    }

    // Test para traer generos literarios por Id
    [TestMethod]
    public async Task GetGenreByIdRepositoryException()
    {
        // Arrange
        var genre = _genres.First();
        _genreRepository.When(x => x.FindAsync(genre.Id)).Do(x => throw new Exception("Repository error"));

        // Act
        var result = await _genreService.GetByGenreId(genre.Id);

        // Assert
        Assert.AreEqual((int)result.StatusCode, 500);
    }
    // Test para traer Genero literario por nombre
    [TestMethod]
    public async Task GetGenreByNameRepositoryException()
    {
        // Arrange
        var genre = _genres.First();
        _genreRepository.When(x => x.GetAllAsync(Arg.Any<Expression<Func<Genre, bool>>>())).Do(x => throw new Exception("Repository error"));

        // Act
        var result = await _genreService.GetGenresByName(genre.Name);

        // Assert
        Assert.AreEqual((int)result.StatusCode, 500);
    }
    // Test traer genero literario descripcion 
    [TestMethod]
    public async Task GetGenreByDescriptionRepositoryException()
    {
        // Arrange
        var genre = _genres.First();
        _genreRepository.When(x => x.GetAllAsync(Arg.Any<Expression<Func<Genre, bool>>>())).Do(x => throw new Exception("Repository error"));

        // Act
        var result = await _genreService.GetGenresByDescription(genre.Description);

        // Assert
        Assert.AreEqual((int)result.StatusCode, 500);
    }
}