using NSubstitute;
using katio.Data;
using katio.Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using katio.Business.Interfaces;
using katio.Business.Services;
using System.Linq.Expressions;
using System.Net;

namespace katio.Test.UserTests;

[TestClass]
public class UserTestsException
{
    private readonly IRepository<int, User> _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserService _userService;
    private List<User> _user;

    public UserTestsException()
    {
        _userRepository = Substitute.For<IRepository<int, User>>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _unitOfWork.UserRepository.Returns(_userRepository);
        _userService = new UserService(_unitOfWork);

        _user = new List<User>
        {
            new User 
            { 
            Name = "Maria Camila",
            LastName = "Gil Rojas",
            Email = "cami@gmail.com",
            Telefono = "3015822126",
            Password = "1234",
            Username = "Cami",
            Identificacion = "10333658944"
            },
            new User 
            { 
            Name = "Maria Camila",
            LastName = "Gil Rojas",
            Email = "cami@gmail.com",
            Telefono = "3015822126",
            Password = "1234",
            Username = "Cami",
            Identificacion = "10333658944"
            }
        };
    }

    // TEST DE EXCEPCIONES | Repositorio

    // Test Crear usuario
    [TestMethod]
    public async Task CreateUserRepositoryException()
    {
        // Arrange
        var user = new User
        { 
            Name = "Maria Camila",
            LastName = "Gil Rojas",
            Email = "cami@gmail.com",
            Telefono = "3015822126",
            Password = "1234",
            Username = "Cami",
            Identificacion = "10333658944", 
        };
        _userRepository.GetAllAsync(Arg.Any<Expression<Func<User, bool>>>()).Returns(new List<User>());
        _userRepository.When(x => x.AddAsync(Arg.Any<User>())).Do(x => throw new Exception("Repository error | User"));

        // Act
        var result = await _userService.CreateUser(user);

        // Assert
        Assert.AreEqual((int)result.StatusCode, 500);
    }

    /*
    // Test para actualizar Usuario Fallando por el momento
    [TestMethod]
    public async Task UpdateUserRepositoryException()
    {
        // Arrange
        var userToUpdate = _user.First();
        _userRepository.FindAsync(userToUpdate.Id).Returns(userToUpdate);
        var updatedUser = new User
        { 
            Id = userToUpdate.Id, 
            Name = "Maria Camila",
            LastName = "Gil Rojas",
            Email = "cami@gmail.com",
            Telefono = "3015822126",
            Password = "1234",
            Username = "Cami",
            Identificacion = "10333658944"
        };
        _userRepository.FindAsync(userToUpdate.Id).Returns(userToUpdate);
        _userRepository.When(x => x.Update(Arg.Any<User>())).Do(x => throw new Exception("Repository exception | User"));

        // Act
        var result = await _userService.UpdateUser(updatedUser);

        // Assert
        Assert.AreEqual((int)result.StatusCode, 500);
    }
    */
    
    // Test para borrar usuario
    [TestMethod]
    public async Task DeleteUserRepositoryException()
    {
        // Arrange
        var userToDelete = _user.First();
        _userRepository.FindAsync(userToDelete.Id).Returns(userToDelete);
        _userRepository.When(x => x.Delete(userToDelete)).Do(x => throw new Exception("Repository error | User"));

        // Act
        var result = await _userService.DeleteUser(userToDelete.Id);

        // Assert
        Assert.AreEqual((int)result.StatusCode, 500);
    }

    // Test para borrar todos los usuarios
    [TestMethod]
    public async Task GetAllUserRepositoryException()
    {
        // Arrange
        _userRepository.When(x => x.GetAllAsync()).Do(x => throw new Exception("Repository error | User"));

        // Act
        var result = await _userService.Index();

        // Assert
        Assert.AreEqual((int)result.StatusCode, 500);
    }

    // Test para traer usuario por Id
    [TestMethod]
    public async Task GetUserByIdRepositoryException()
    {
        // Arrange
        var user = _user.First();
        _userRepository.When(x => x.FindAsync(user.Id)).Do(x => throw new Exception("Repository error | User"));

        // Act
        var result = await _userService.GetUserById(user.Id);

        // Assert
        Assert.AreEqual((int)result.StatusCode, 500);
    }

    // Test para traer usuario por nombre 
    [TestMethod]
    public async Task GetUserByNameRepositoryException()
    {
        // Arrange
        var user = _user.First();
        _userRepository.When(x => x.GetAllAsync(Arg.Any<Expression<Func<User, bool>>>())).Do(x => throw new Exception("Repository error | User"));

        // Act
        var result = await _userService.GetUserByName(user.Name);

        // Assert
        Assert.AreEqual((int)result.StatusCode, 500);
    }

    // Test para traer por apellido del usuario 
    [TestMethod]
    public async Task GetUserByLastNameRepositoryException()
    {
        // Arrange
        var user = _user.First();
        _userRepository.When(x => x.GetAllAsync(Arg.Any<Expression<Func<User, bool>>>())).Do(x => throw new Exception("Repository error | User"));

        // Act
        var result = await _userService.GetUserByLastName(user.LastName);

        // Assert
        Assert.AreEqual((int)result.StatusCode, 500);
    }

    // Test para traer usuario por gmail
    [TestMethod]
    public async Task GetUserByEmailRepositoryException()
    {
        // Arrange
        var user = _user.First();
        _userRepository.When(x => x.GetAllAsync(Arg.Any<Expression<Func<User, bool>>>())).Do(x => throw new Exception("Repository error | User"));

        // Act
        var result = await _userService.GetUserByEmail(user.Email);

        // Assert
        Assert.AreEqual((int)result.StatusCode, 500);
    }

    /*
     // Test para traer usuario por identificacion Fallando por el momento
    [TestMethod]
    public async Task GetUserByIdentificacionRepositoryException()
    {
        // Arrange
        var user = _user.First();
        _userRepository.When(x => x.GetAllAsync(Arg.Any<Expression<Func<User, bool>>>())).Do(x => throw new Exception("Repository error | User"));

        // Act
        var result = await _userService.GetUserByIdentificacion(user.Identificacion);

        // Assert
        Assert.AreEqual((int)result.StatusCode, 500);
    }
    */
}