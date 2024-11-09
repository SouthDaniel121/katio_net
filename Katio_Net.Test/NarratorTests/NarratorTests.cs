using NSubstitute;
using katio.Data;
using katio.Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using katio.Business.Interfaces;
using katio.Business.Services;
using System.Linq.Expressions;

namespace katio.Test.NarratorTests;

[TestClass]
public class NarratorTests
{
    private readonly IRepository<int, Narrator> _narratorRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly INarratorService _narratorService;
    private List<Narrator> _narrators;

    public NarratorTests()
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

    // Test para crear narrador
    [TestMethod]
    public async Task CreateNarrator()
    {
        // Arrange
        var newNarrator = new Narrator 
        { 
            Name = "Maria Camila", 
            LastName = "Gil Rojas", 
            Genre = "Ficcion" 
        };
        _narratorRepository.GetAllAsync(Arg.Any<Expression<Func<Narrator, bool>>>()).Returns(new List<Narrator>());
        _narratorRepository.AddAsync(newNarrator).Returns(Task.CompletedTask);

        // Act
        var result = await _narratorService.CreateNarrator(newNarrator);

        // Assert
        Assert.IsTrue(result.ResponseElements.Any());
    }
    // Test para actualizar narrador
    [TestMethod]
    public async Task UpdateNarrator()
    {
        // Arrange
        var narratorToUpdate = _narrators.First();
        _narratorRepository.FindAsync(narratorToUpdate.Id).Returns(narratorToUpdate);

        var updatedNarrator = new Narrator
        {
            Id = narratorToUpdate.Id,
            Name = "Maria Updated",
            LastName = "Gil Updated",
            Genre = "Ficcion Updated"
        };
        _narratorRepository.FindAsync(updatedNarrator.Id).Returns(updatedNarrator);
        _narratorRepository.Update(updatedNarrator).Returns(Task.CompletedTask);

        // Act
        var result = await _narratorService.UpdateNarrator(updatedNarrator);

        // Assert
        Assert.IsTrue(result.ResponseElements.Any());
    }
    // Test para borrar narrador
    [TestMethod]
    public async Task DeleteNarrator()
    {
        // Arrange
        var narratorToDelete = _narrators.First();
        _narratorRepository.FindAsync(narratorToDelete.Id).Returns(narratorToDelete);
        _narratorRepository.Delete(narratorToDelete).Returns(Task.CompletedTask);

        // Act
        var result = await _narratorService.DeleteNarrator(narratorToDelete.Id);

        // Assert
        Assert.IsTrue(result.ResponseElements.Any());
    }
    // Test para traer todos los narradorres
    [TestMethod]
    public async Task GetAllNarrators() 
    {
        // Arrange
        _narratorRepository.GetAllAsync().Returns(_narrators);

        // Act
        var result = await _narratorService.Index();

        // Assert
        Assert.IsTrue(result.ResponseElements.Any());
    }

    // Test Para traer por ID
    [TestMethod]
    public async Task GetNarratorById()
    {
        // Arrange
        var narrator = _narrators.First();
        _narratorRepository.FindAsync(narrator.Id).Returns(narrator);

        // Act
        var result = await _narratorService.GetNarratorById(narrator.Id);

        // Assert
        Assert.IsTrue(result.ResponseElements.Any());
    }

    // Test para traer por nombre de narrador
    [TestMethod]
    public async Task GetNarratorsByName()
    {
        // Arrange
        var narrator = _narrators.First();
        _narratorRepository.GetAllAsync(Arg.Any<Expression<Func<Narrator, bool>>>()).Returns(new List<Narrator> { narrator });

        // Act
        var result = await _narratorService.GetNarratorsByName(narrator.Name);

        // Assert
        Assert.IsTrue(result.ResponseElements.Any());
    }

    // Test para traer narrador por apellido
    [TestMethod]
    public async Task GetNarratorsByLastName()
    {
        // Arrange
        var narrator = _narrators.First();
        _narratorRepository.GetAllAsync(Arg.Any<Expression<Func<Narrator, bool>>>()).Returns(new List<Narrator> { narrator });

        // Act
        var result = await _narratorService.GetNarratorsByLastName(narrator.LastName);

        // Assert
        Assert.IsTrue(result.ResponseElements.Any());
    }

    // Test para traer narrador por genero literario
    [TestMethod]
    public async Task GetNarratorsByGenre()
    {
        // Arrange
        var narrator = _narrators.First();
        _narratorRepository.GetAllAsync(Arg.Any<Expression<Func<Narrator, bool>>>()).Returns(new List<Narrator> { narrator });

        // Act
        var result = await _narratorService.GetNarratorsByGenre(narrator.Genre);

        // Assert
        Assert.IsTrue(result.ResponseElements.Any());
    }
}