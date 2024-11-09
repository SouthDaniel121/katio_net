﻿using katio.Business.Interfaces;
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
public class GenreTests
{
    private readonly IRepository<int, Genre> _genreRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenreService _genreService;
    private List<Genre> _genres;

    public GenreTests()
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

    // Test para crear genero literario
    [TestMethod]
    public async Task CreateGenre()
    {
        // Arange
        var newGenre = new Genre 
        { 
            Name = "Fantasy", 
            Description = "La Fantasia es..." 
        };
        _genreRepository.GetAllAsync(Arg.Any<Expression<Func<Genre, bool>>>()).Returns(new List<Genre>());
        _genreRepository.AddAsync(newGenre).Returns(Task.CompletedTask);

        // Act
        var result = await _genreService.CreateGenre(newGenre);

        // Assert
        Assert.IsTrue(result.ResponseElements.Any());
    }
    // Test para actualizar Genero literario
    [TestMethod]
    public async Task UpdateGenre()
    {
        // Arrange
        var GenreToUpdate = _genres.First();
        _genreRepository.FindAsync(GenreToUpdate.Id).Returns(GenreToUpdate);

        var updatedGenre = new Genre 
        { 
            Id = GenreToUpdate.Id,
            Name = "Fantasy", 
            Description = "La Fantasia es..." 
        };
        _genreRepository.FindAsync(updatedGenre.Id).Returns(updatedGenre);
        _genreRepository.Update(updatedGenre).Returns(Task.CompletedTask);

        // Act
        var result = await _genreService.UpdateGenre(updatedGenre);

        // Assert
        Assert.IsTrue(result.ResponseElements.Any());
    }
    // Test para eliminar genero literarios
    [TestMethod]
    public async Task DeleteGenre()
    {
        // Arrange
        var genreToDelete = _genres.First();
        _genreRepository.FindAsync(genreToDelete.Id).Returns(genreToDelete);

        _genreRepository.Delete(genreToDelete).Returns(Task.CompletedTask);

        // Act
        var result = await _genreService.DeleteGenre(genreToDelete.Id);

        // Assert
        Assert.IsTrue(result.ResponseElements.Any());
    }    
    // Test para traer todos los generos literarios
    [TestMethod]
    public async Task GetAllGenres()
    {
        // Arrange
        _genreRepository.GetAllAsync().Returns(_genres);

        // Act
        var result = await _genreService.Index();

        // Assert
        Assert.IsTrue(result.ResponseElements.Any());
    }
    // Test para traer por Id 
    [TestMethod]
    public async Task GetGenreById()
    {
        // Arrange
        var genre = _genres.First();
        _genreRepository.FindAsync(genre.Id).Returns(genre);

        // Act
        var result = await _genreService.GetByGenreId(genre.Id);

        // Assert
        Assert.IsTrue(result.ResponseElements.Any());
    }
    // Test para traer por el genero literario nombre.
    [TestMethod]
    public async Task GetGenreByName()
    {
        // Arrange
        var genre = _genres.First();
        _genreRepository.GetAllAsync(Arg.Any<Expression<Func<Genre, bool>>>()).Returns(new List<Genre> { genre });

        // Act
        var result = await _genreService.GetGenresByName(genre.Name);

        // Assert
        Assert.IsTrue(result.ResponseElements.Any());
    }
    // Test para traer descripcion del genero literario
    [TestMethod]
    public async Task GetGenresByDescription()
    {
        // Arrange
        var genre = _genres.First();
        _genreRepository.GetAllAsync(Arg.Any<Expression<Func<Genre, bool>>>()).Returns(new List<Genre> { genre });

        // Act
        var result = await _genreService.GetGenresByDescription(genre.Description);

        // Assert
        Assert.IsTrue(result.ResponseElements.Any());
    }
}
