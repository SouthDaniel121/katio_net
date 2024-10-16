using katio.Business.Interfaces;
using katio.Business.Services;
using katio.Data.Dto;
using katio.Data.Models;
using katio.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Linq.Expressions;
using System.Net;

namespace katio.Test;


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

        _genres = new List<Genre>()
        {
            new Genre
            {
                Name = "Fantasia",
                Description = "La Fantasia es..."
            },
            new Genre
            {
                Name = "Ficcion",
                Description = "La Ciencia Ficcion es..."
            }
        };
    }

    // Test Traer todos lo generos
    [TestMethod]
    public async Task GetAllGenres()
    {
        // Arrange
        _genreRepository.GetAllAsync().Returns(_genres);

        // Act
        var result = await _genreService.Index();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.ResponseElements.Count());
    }
}