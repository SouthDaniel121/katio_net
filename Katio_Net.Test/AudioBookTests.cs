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
                Path = "C:/Users/Usuario/Downloads/Cien a�os de soledad.mp3",
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
}