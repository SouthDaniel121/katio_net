using NSubstitute;
using katio.Data;
using katio.Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using katio.Business.Interfaces;
using katio.Business.Services;
using System.Linq.Expressions;



namespace katio.Test.AudioBookFailTests;

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
            Path = "C:/Users/Usuario/Downloads/Cien años de soledad.mp3",
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
            LenghtInSeconds = 10,
            Path = "C:/Users/Usuario/Downloads/Huellas.mp3",
            NarratorId = 3       
            }
        };
    }
    
    //Test metodos simples

    // Test para traer todos los audio books

    [TestMethod]

    public async Task GetAllAudioBooks()

    {

        // Arrange

     _audioBookRepository.GetAllAsync().Returns(_audioBooks);



        // Act

        var result = await _audioBookService.Index();



        // Assert

        Assert.IsTrue(result.ResponseElements.Any());

    }



    // Test para traer audiobook por id

    [TestMethod]

    public async Task GetAudioBookById()

    {

        // Arrange

    var audioBook = _audioBooks.First();

        _audioBookRepository.FindAsync(audioBook.Id).Returns(audioBook);



        // Act

        var result = await _audioBookService.GetAudioBookById(audioBook.Id);



        // Assert

        Assert.IsTrue(result.ResponseElements.Any());

    }




    // Test para traer audioBook por Name

    [TestMethod]

    public async Task GetAudioBookByName()

    {

        // Arrange

     var audioBook = _audioBooks.First();

        _audioBookRepository.GetAllAsync(Arg.Any<Expression<Func<AudioBook, bool>>>()).Returns(new List<AudioBook> { audioBook });



        // Act

        var result = await _audioBookService.GetByAudioBookName(audioBook.Name);



        // Assert

        Assert.IsTrue(result.ResponseElements.Any());

    }



    // Test para traer  audiobook por ISBN10

    [TestMethod]

    public async Task GetByAudioBookISBN10()

    {

        //Arrange

     var audioBook = _audioBooks.First();

        _audioBookRepository.GetAllAsync(Arg.Any<Expression<Func<AudioBook, bool>>>()).Returns(new List<AudioBook> { audioBook });



        // Act

        var result = await _audioBookService.GetByAudioBookISBN10(audioBook.ISBN10);



        // Assert

        Assert.IsTrue(result.ResponseElements.Any());




    }



    // Test para traer audiobook por ISBN13

    [TestMethod]

    public async Task GetByAudioBookISBN13()

    {

        //Arrange

     var audioBook = _audioBooks.First();

        _audioBookRepository.GetAllAsync(Arg.Any<Expression<Func<AudioBook, bool>>>()).Returns(new List<AudioBook> { audioBook });

        // Act
        var result = await _audioBookService.GetByAudioBookISBN13(audioBook.ISBN13);
        // Assert

        Assert.IsTrue(result.ResponseElements.Any());




    }



    // Test para traer audiobooks por published

    [TestMethod]

    public async Task GetByAudioBookPublished()

    {

        // Arrange

    var startDate = new DateOnly(1966, 06, 05);

        var endDate = new DateOnly(2020, 06, 06);

        var expectedAudioBook = _audioBooks.Where(a => a.Published >= startDate && a.Published <= endDate).ToList();

        _audioBookRepository.GetAllAsync(Arg.Any<Expression<Func<AudioBook, bool>>>()).Returns(expectedAudioBook);

        // Act

     var result = await _audioBookService.GetByAudioBookPublished(startDate, endDate);



        // Assert

        Assert.IsTrue(result.ResponseElements.Any());



    }



    //Test para traer audiobook por edition

    [TestMethod]

    public async Task GetByAudioBookEdition()

    {

        //Arrange

    var audioBook = _audioBooks.First();

        _audioBookRepository.GetAllAsync(Arg.Any<Expression<Func<AudioBook, bool>>>()).Returns(new List<AudioBook> { audioBook });



        // Act

        var result = await _audioBookService.GetByAudioBookEdition(audioBook.Edition);



        // Assert

        Assert.IsTrue(result.ResponseElements.Any());




    }



    //Test para traer audiobook por genre

    [TestMethod]

    public async Task GetByAudioBookGenre()

    {

        //Arrange

     var audioBook = _audioBooks.First();

        _audioBookRepository.GetAllAsync(Arg.Any<Expression<Func<AudioBook, bool>>>()).Returns(new List<AudioBook> { audioBook });



        // Act

        var result = await _audioBookService.GetByAudioBookGenre(audioBook.Genre);



        // Assert

        Assert.IsTrue(result.ResponseElements.Any());




    }



    //Test para traer audiobook por lenght in seconds

    [TestMethod]

    public async Task GetByAudioBookLenghtInSeconds()

    {

        //Arrange

     var audioBook = _audioBooks.First();

        _audioBookRepository.FindAsync(audioBook.LenghtInSeconds).Returns(audioBook);



        // Act

        var result = await _audioBookService.GetByAudioBookLenghtInSeconds(audioBook.LenghtInSeconds);



        // Assert

        Assert.IsTrue(result.ResponseElements.Any());



    }

    // Test para crear Audiobook

     [TestMethod]

    public async Task CreateAudioBook()

    {

        // Arrange

    var newAudioBook = new AudioBook

        {

            Name = "Cien años de soledad",

            ISBN10 = "8420471836",

            ISBN13 = "978-8420471839",

            Published = new DateOnly(1967, 06, 05),

            Edition = "RAE Obra Académica",

            Genre = "Ficcion",

            LenghtInSeconds = 1,

            Path = "C:/Users/Usuario/Downloads/Cien años de soledad.mp3",

            NarratorId = 1

        };

        _audioBookRepository.GetAllAsync(Arg.Any<Expression<Func<AudioBook, bool>>>()).Returns(new List<AudioBook>());

        _audioBookRepository.AddAsync(newAudioBook).Returns(Task.CompletedTask);



        // Act

        var result = await _audioBookService.CreateAudioBook(newAudioBook);



        // Assert

        Assert.IsTrue(result.ResponseElements.Any());

    }



    // Test para actualizar Audio book

    [TestMethod]

    public async Task UpdateAudioBook()

    {

        // Arrange

     var audioBookToUpdate = _audioBooks.First();

        _audioBookRepository.FindAsync(audioBookToUpdate.Id).Returns(audioBookToUpdate);



        var updateAudioBook = new AudioBook

        {

            Name = "Cien años de soledad",

            ISBN10 = "8420471836",

            ISBN13 = "978-8420471839",

            Published = new DateOnly(1967, 06, 05),

            Edition = "RAE Obra Académica",

            Genre = "Ficcion",

            LenghtInSeconds = 1,

            Path = "C:/Users/Usuario/Downloads/Cien años de soledad.mp3",

            NarratorId = 1

        };

        _audioBookRepository.FindAsync(updateAudioBook.Id).Returns(updateAudioBook);



        _audioBookRepository.Update(updateAudioBook).Returns(Task.CompletedTask);



        // Act

        var result = await _audioBookService.UpdateAudioBook(updateAudioBook);



        // Assert

        Assert.AreEqual((int)result.StatusCode, 200);

    }



    // Test para borrar Audio book

    [TestMethod]

    public async Task DeleteAudioBook()

    {

        // Arrange

     var AudioBookToDelete = _audioBooks.First();

        _audioBookRepository.FindAsync(AudioBookToDelete.Id).Returns(AudioBookToDelete);



        _audioBookRepository.Delete(AudioBookToDelete).Returns(Task.CompletedTask);



        // Act

        var result = await _audioBookService.DeleteAudioBook(AudioBookToDelete.Id);



        // Assert

        Assert.IsTrue(result.ResponseElements.Any());

    }



  

}