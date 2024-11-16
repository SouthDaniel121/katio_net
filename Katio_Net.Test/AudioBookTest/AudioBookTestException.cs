using NSubstitute;
using katio.Data;
using katio.Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using katio.Business.Interfaces;
using katio.Business.Services;
using System.Linq.Expressions;
using NSubstitute.ExceptionExtensions;

namespace katio.Test.AudioBookFailTests;

[TestClass]
public class AudioBookExceptiontest
{
    private readonly IRepository<int, AudioBook> _audioBookRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAudioBookService _audioBookService;
    private List<AudioBook> _audioBooks;


    public AudioBookExceptiontest()
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
            }
        };
    }
    
    // TEST PARA REPOSITORY EXCEPTIONS



    // Test para traer todos los audioLibros | repository exceptions

    [TestMethod]

    public async Task GetAllAudioBooksRepositoryException()

    {

        // Arrange  

     _audioBookRepository.GetAllAsync(Arg.Any<Expression<Func<AudioBook, bool>>>()).ThrowsForAnyArgs(new Exception());



        // Act

        var result = await _audioBookService.Index();



        // Assert

        Assert.AreEqual((int)result.StatusCode, 500);

    }




    // Test para traer audioBook por id repository exceptions

    [TestMethod]

    public async Task GetAudioBookByIdRepositoryException()

    {

        // Arrange

     var audioBook = _audioBooks.First();

        _audioBookRepository.When(x => x.FindAsync(audioBook.Id)).Do(x => throw new Exception());



        // Act

        var result = await _audioBookService.GetAudioBookById(audioBook.Id);



        // Assert

        Assert.AreEqual((int)result.StatusCode, 500);

    }




    // Test para traer audioBook por nombre  repository exceptions

    [TestMethod]

    public async Task GetAudioBookByNameRepositoryException()

    {

        // Arrange

     _audioBookRepository.GetAllAsync(Arg.Any<Expression<Func<AudioBook, bool>>>()).ThrowsForAnyArgs(new Exception());

        var result = await _audioBookService.GetByAudioBookName(Arg.Any<string>());



        Assert.AreEqual((int)result.StatusCode, 500);

    }



    // Test para traer audioBook por ISBN10  repository exceptions

    [TestMethod]

    public async Task GetByAudioBookISBN10RepositoryException()

    {

        // Arrange

     _audioBookRepository.GetAllAsync(Arg.Any<Expression<Func<AudioBook, bool>>>()).ThrowsForAnyArgs(new Exception());

        var result = await _audioBookService.GetByAudioBookISBN10(Arg.Any<string>());



        Assert.AreEqual((int)result.StatusCode, 500);

    }



    // Test para traer audioBook por ISBN13 with repository exceptions

    [TestMethod]

    public async Task GetByAudioBookISBN13RepositoryException()

    {

        // Arrange

        _audioBookRepository.GetAllAsync(Arg.Any<Expression<Func<AudioBook, bool>>>()).ThrowsForAnyArgs(new Exception());

        var result = await _audioBookService.GetByAudioBookISBN13(Arg.Any<string>());



        Assert.AreEqual((int)result.StatusCode, 500);

    }



    // Test para traer audioBook por published  repository exceptions

    [TestMethod]

    public async Task GetByAudioBookPublishedRepositoryException()

    {

        // Arrange

     _audioBookRepository.GetAllAsync(Arg.Any<Expression<Func<AudioBook, bool>>>()).ThrowsForAnyArgs(new Exception());

        // var result = await _audioBookService.GetByAudioBookPublished(Arg.Any<DateOnly>());

        //Assert.AreEqual((int)result.StatusCode, 500);

    }



    // Test para traer audioBook por Edition  repository exceptions

    [TestMethod]

    public async Task GetByAudioBookEditionRepositoryException()

    {

        // Arrange

     _audioBookRepository.GetAllAsync(Arg.Any<Expression<Func<AudioBook, bool>>>()).ThrowsForAnyArgs(new Exception());

        var result = await _audioBookService.GetByAudioBookEdition(Arg.Any<string>());



        Assert.AreEqual((int)result.StatusCode, 500);

    }



    // Test para traer audioBook por Genre  repository exceptions

    [TestMethod]

    public async Task GetByAudioBookGenreRepositoryException()

    {

        // Arrange

     _audioBookRepository.GetAllAsync(Arg.Any<Expression<Func<AudioBook, bool>>>()).ThrowsForAnyArgs(new Exception());

        var result = await _audioBookService.GetByAudioBookGenre(Arg.Any<string>());



        Assert.AreEqual((int)result.StatusCode, 500);

    }



    // Test para traer audioBook por LenghtInSeconds repository exceptions

    [TestMethod]

    public async Task GetByAudioBookLenghtInSecondsRepositoryException()

    {

        // Arrange

     var audioBook = _audioBooks.First();

        _audioBookRepository.When(x => x.FindAsync(audioBook.LenghtInSeconds)).Do(x => throw new Exception());

        var result = await _audioBookService.GetAudioBookById(audioBook.Id);



        Assert.AreEqual((int)result.StatusCode, 500);

    }

    // Test para traer narrator por Name exeption
    [TestMethod]

    public async Task GetAudioBookByNarratorName_Exeption()

    {

        // Arrange

        _audioBookRepository.GetAllAsync(Arg.Any<Expression<Func<AudioBook, bool>>>()).ThrowsForAnyArgs(new Exception());

        var result = await _audioBookService.GetByAudioBookName(Arg.Any<string>());


        Assert.AreEqual((int)result.StatusCode, 500);

    }

    // Test para traer narrator por id exeption
    [TestMethod]
    public async Task GetAudioBookByNarratorId_Exeption()
    {
        
        // Arrange

        var audioBook = _audioBooks.First();

        _audioBookRepository.When(x => x.FindAsync(audioBook.NarratorId)).Do(x => throw new Exception());

        var result = await _audioBookService.GetAudioBookByNarrator(audioBook.NarratorId);



        Assert.AreEqual((int)result.StatusCode, 500);
    }

    // Test para traer narrator por last name exeption
    [TestMethod]

    public async Task GetAudioBookByNarratorLastName_Exeption()

    {

        // Arrange

        _audioBookRepository.GetAllAsync(Arg.Any<Expression<Func<AudioBook, bool>>>()).ThrowsForAnyArgs(new Exception());

        var result = await _audioBookService.GetAudioBookByNarratorLastName(Arg.Any<string>());


        Assert.AreEqual((int)result.StatusCode, 500);


    }

    // Test para traer narrator por full name exeption
    [TestMethod]

    public async Task GetAudioBookByNarratorFullNameExeption()

    {

        // Arrange

        _audioBookRepository.GetAllAsync(Arg.Any<Expression<Func<AudioBook, bool>>>()).ThrowsForAnyArgs(new Exception());

        var result = await _audioBookService.GetAudioBookByNarratorFullName(Arg.Any<string>(),Arg.Any<string>());

        Assert.AreEqual((int)result.StatusCode, 500);


    }

    // Test para traer narrator por genre exeption
    [TestMethod]

    public async Task GetAudioBookByNarratorGenreExeption()

    {
        // Arrange

        _audioBookRepository.GetAllAsync(Arg.Any<Expression<Func<AudioBook, bool>>>()).ThrowsForAnyArgs(new Exception());

        var result = await _audioBookService.GetAudioBookByNarratorGenre(Arg.Any<string>());

        Assert.AreEqual((int)result.StatusCode, 500);


    }

    // Test para crear audio book  repository exceptions
    [TestMethod]
    public async Task CreateAudioBookRepositoryException()
    {
        // Arrange
        var newAudioBook = new AudioBook
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
        };
        _audioBookRepository.GetAllAsync(Arg.Any<Expression<Func<AudioBook, bool>>>()).Returns(new List<AudioBook>());
        _audioBookRepository.When(x => x.AddAsync(Arg.Any<AudioBook>())).Do(x => throw new Exception("Repository error"));

        // Act
        var result = await _audioBookService.CreateAudioBook(newAudioBook);

        // Assert
        Assert.AreEqual((int)result.StatusCode, 500);
    }

    // Test para actualizar audio book  repository exceptions
    [TestMethod]
    public async Task UpdateAudioBookRepositoryException()
    {
        // Arrange
        var audioBookToUpdate = _audioBooks.First();
        _audioBookRepository.FindAsync(audioBookToUpdate.Id).Returns(audioBookToUpdate);
        var updatedAudioBook = new AudioBook
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
        };
        _audioBookRepository.FindAsync(audioBookToUpdate.Id).Returns(audioBookToUpdate);
        _audioBookRepository.When(x => x.Update(Arg.Any<AudioBook>())).Do(x => throw new Exception("Repository error"));

        // Act
        var result = await _audioBookService.UpdateAudioBook(updatedAudioBook);

        // Assert
        Assert.AreEqual((int)result.StatusCode, 500);
    }



};







