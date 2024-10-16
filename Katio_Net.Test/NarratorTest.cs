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

        _narrators = new List<Narrator>()
        {
            new Narrator
            {
            Id = 1,
            Name = "Maria Camila",
            LastName = "Gil Rojas",
            Genre = "Ficcion"
            }
        };
    }



    // Test Traer todos Narrador 
    [TestMethod]
    public async Task GetAllNarrators() 
    {
        // Arrange
        _narratorRepository.GetAllAsync().Returns(_narrators);

        // Act
        var result = await _narratorService.Index();

        // Assert
        Assert.AreEqual(2, result.ResponseElements.Count());
    }
}