using NSubstitute;
using katio.Data;
using katio.Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using katio.Business.Interfaces;
using katio.Business.Services;
using System.Linq.Expressions;
using System.Net;
using NSubstitute.ExceptionExtensions;

namespace katio.Test.NarratorTests;

[TestClass]
public class NarratorTestsFail
{
    private readonly IRepository<int, Narrator> _narratorRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly INarratorService _narratorService;
    private List<Narrator> _narrators;

    public NarratorTestsFail()
    {
        _narratorRepository = Substitute.For<IRepository<int, Narrator>>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _unitOfWork.NarratorRepository.Returns(_narratorRepository);
        _narratorService = new NarratorService(_unitOfWork);

        _narrators = new List<Narrator>
        {
            new Narrator 
            { 
                Id = 1, 
                Name = "Maria Camila", 
                LastName = "Gil Rojas", 
                Genre = "Ficcion" 
            },
            new Narrator 
            { 
                Id = 2, 
                Name = "Juan", 
                LastName = "Perez", 
                Genre = "Ficcion" 
            }
        };
    }
    //TEST PARA FALLAR

    // Test Para crear narrador
    [TestMethod]
    public async Task CreateNarratorFail()
    {
        // Arrange
        var existingNarrator = new Narrator 
        { 
            Name = "Maria Camila", 
            LastName = "Gil Rojas", 
            Genre = "Ficcion" 
        };
        var newNarrator = new Narrator 
        { 
            Name = "Maria Camila", 
            LastName = "Gil Rojas", 
            Genre = "Ficcion" 
        };
        _narratorRepository.GetAllAsync(Arg.Any<Expression<Func<Narrator, bool>>>()).ReturnsForAnyArgs(new List<Narrator> { existingNarrator });

        // Act
        var result = await _narratorService.CreateNarrator(newNarrator);

        // Assert
        Assert.IsFalse(result.ResponseElements.Any());
    }

    // Test para actualizar Narrador
    [TestMethod]
    public async Task UpdateNarratorFail()
    {
        // Arrange
        _narratorRepository.Update(Arg.Any<Narrator>()).ThrowsAsyncForAnyArgs(new Exception());
        _unitOfWork.NarratorRepository.Returns(_narratorRepository);
        // Act
        var result = await _narratorService.UpdateNarrator(new Narrator());

        // Assert
        Assert.IsFalse(result.ResponseElements.Any());
    }

    // Test para borrar narrador
    [TestMethod]
    public async Task DeleteNarratorFail()
    {
        // Arrange
        var narratorToDelete = _narrators.First();
        _narratorRepository.FindAsync(narratorToDelete.Id).ReturnsForAnyArgs(Task.FromResult<Narrator>(null));

        // Act
        var result = await _narratorService.DeleteNarrator(narratorToDelete.Id);

        // Assert
        Assert.IsFalse(result.ResponseElements.Any());
    }

     // Test para traer todos los narradores
    [TestMethod]
    public async Task GetAllNarratorsFail()
    {
        // Arrange
        _narratorRepository.GetAllAsync().Returns(new List<Narrator>());

        // Act
        var result = await _narratorService.Index();

        // Assert
        Assert.IsFalse(result.ResponseElements.Any());
    }

    // Test para traer por el ID
    [TestMethod]
    public async Task GetNarratorByIdFail()
    {
        // Arrange
        var narrator = _narrators.First();
        _narratorRepository.FindAsync(narrator.Id).ReturnsForAnyArgs(Task.FromResult<Narrator>(null));

        // Act
        var result = await _narratorService.GetNarratorById(narrator.Id);

        // Assert
        Assert.IsFalse(result.ResponseElements.Any());
    }

    // Test para traer por el nombre
    [TestMethod]
    public async Task GetNarratorsByNameFail()
    {
        // Arrange
        var narrator = _narrators.First();
        _narratorRepository.GetAllAsync(Arg.Any<Expression<Func<Narrator, bool>>>()).ReturnsForAnyArgs(new List<Narrator>());

        // Act
        var result = await _narratorService.GetNarratorsByName(narrator.Name);

        // Assert
        Assert.IsFalse(result.ResponseElements.Any());
    }

     // Test para traer por el apellido
    [TestMethod]
    public async Task GetNarratorsByLastNameFail()
    {
        // Arrange
        var narrator = _narrators.First();
        _narratorRepository.GetAllAsync(Arg.Any<Expression<Func<Narrator, bool>>>()).ReturnsForAnyArgs(new List<Narrator>());

        // Act
        var result = await _narratorService.GetNarratorsByLastName(narrator.LastName);

        // Assert
        Assert.IsFalse(result.ResponseElements.Any());
    }

    // Test para traer por genero literario
    [TestMethod]
    public async Task GetNarratorsByGenreFail()
    {
        // Arrange
        var narrator = _narrators.First();
        _narratorRepository.GetAllAsync(Arg.Any<Expression<Func<Narrator, bool>>>()).ReturnsForAnyArgs(new List<Narrator>());

        // Act
        var result = await _narratorService.GetNarratorsByGenre(narrator.Genre);

        // Assert
        Assert.IsFalse(result.ResponseElements.Any());
    }
}
