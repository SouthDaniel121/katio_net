﻿using NSubstitute;
using katio.Data;
using katio.Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using katio.Business.Interfaces;
using katio.Business.Services;
using System.Linq.Expressions;
using katio.Data.Dto;
using System.Net;

namespace katio.Test;
//Crear El test con la clase
[TestClass]
public class AudioBookTests
{
    private readonly IRepository<int, AudioBook> _audioBookRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAudioBookService _audioBookService;
    private List<AudioBook> _audioBooks;

    public AudioBookTests()
    {
        _audioBookRepository = Substitute.For<IRepository<int, AudioBook>>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _unitOfWork.AudioBookRepository.Returns(_audioBookRepository);
        _audioBookService = new AudioBookService(_unitOfWork);

        _audioBooks = new List<AudioBook>()
        {
            new AudioBook
            {
                Id = 1,
                Name = "Cien años de soledad",
                ISBN10 = "8420471836",
                ISBN13 = "978-8420471839",
                Published = new DateOnly(1967, 06, 05),
                Edition = "RAE Obra Académica",
                Genre = "Ficcion",
                LenghtInSeconds = 1,
                Path = "C:/Users/Downloads/Cien años de soledad.mp3",
                NarratorId = 1
            },
            new AudioBook
            {
                Id = 2,
                Name = "Huellas",
                ISBN10 = "9584277278",
                ISBN13 = "978-958427275",
                Published = new DateOnly(2019, 01, 01),
                Edition = "1ra Edicion",
                Genre = "Ficcion",
                LenghtInSeconds = 1,
                Path = "C:/Users/Usuario/Downloads/Huellas.mp3",
                NarratorId = 3
            }
        };
    }


    // Test Traer todos los Audiolibros
    [TestMethod]
    public async Task GetAllAudioBooks() 
    {
        // Arrange 
        _audioBookRepository.GetAllAsync().Returns(_audioBooks);

        // Act
        var result = await _audioBookService.Index();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.ResponseElements.Count());
    }


    // Test para traer audiolibros por id 
    [TestMethod]
    public async Task GetAudioBookById()
    {
        // Arrange
        var audioBook = _audioBooks.First();
        _audioBookRepository.FindAsync(audioBook.Id).Returns(audioBook);

        // Act
        var result = await _audioBookService.GetAudioBookById(audioBook.Id);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(audioBook.Name, result.ResponseElements.First().Name);
    }


    // Test para traer AudioLibro por nombre  
    [TestMethod]
    public async Task GetAudioBookByName()
    {
        // Arrange
        var audioBook = _audioBooks.First();
        _audioBookRepository.GetAllAsync(Arg.Any<Expression<Func<AudioBook, bool>>>()).Returns(new List<AudioBook> { audioBook });

        // Act
        var result = await _audioBookService.GetByAudioBookName(audioBook.Name);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(audioBook.Name, result.ResponseElements.First().Name);
    }



    // Repositorio excepción 
    // Test para traer todos los audiolibros excepción
    [TestMethod]
    public async Task GetAllAudioBooksRepositoryException()
    {
        // Arrange
        _audioBookRepository.When(x => x.GetAllAsync()).Do(x => throw new Exception());

        // Act
        var result = await _audioBookService.Index();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);
    }


    // Test para traer audioLibro por id repositorio  repositorio excepción
    [TestMethod]
    public async Task GetAudioBookByIdRepositoryException()
    {
        // Arrange
        var audioBook = _audioBooks.First();
        _audioBookRepository.When(x => x.FindAsync(audioBook.Id)).Do(x => throw new Exception());

        // Act
        var result = await _audioBookService.GetAudioBookById(audioBook.Id);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);
    }


    // Test traer Audiolibro por nombre  excepción-
    [TestMethod]
    public async Task GetAudioBookByNameRepositoryException()
    {
        // Arrange
        var audioBook = _audioBooks.First();
        _audioBookRepository.When(x => x.GetAllAsync(Arg.Any<Expression<Func<AudioBook, bool>>>())).Do(x => throw new Exception());

        // Act
        var result = await _audioBookService.GetByAudioBookName(audioBook.Name);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);
    }
}
