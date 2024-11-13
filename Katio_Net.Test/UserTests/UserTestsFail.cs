using NSubstitute;
using katio.Data;
using katio.Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using katio.Business.Interfaces;
using katio.Business.Services;
using System.Linq.Expressions;
using System.Net;
using NSubstitute.ExceptionExtensions;

namespace katio.Test.UserTests;

[TestClass]
public class UserTestsFail
{
    private readonly IRepository<int, User> _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserService _userService;
    private List<User> _user;

    public UserTestsFail()
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
            Name = "Manuela",
            LastName = "Ruiz",
            Email = "manu@gmail.com",
            Telefono = "3115822149",
            Password = "12345",
            Username = "Manu",
            Identificacion = "10333658945"
            }
        };
    }

    //TEST PARA FALLAR

    // Test Para crear usuario
    [TestMethod]
    public async Task CreateUserFail()
    {
        // Arrange
        var existingUser = new User 
        { 
             Name = "Maria Camila",
            LastName = "Gil Rojas",
            Email = "cami@gmail.com",
            Telefono = "3015822126",
            Password = "1234",
            Username = "Cami",
            Identificacion = "10333658944"
        };
        var newUser = new User
        { 
            Name = "Manuela",
            LastName = "Ruiz",
            Email = "manu@gmail.com",
            Telefono = "3115822149",
            Password = "12345",
            Username = "Manu",
            Identificacion = "10333658945"
        };
        _userRepository.GetAllAsync(Arg.Any<Expression<Func<User, bool>>>()).ReturnsForAnyArgs(new List<User> { existingUser });

        // Act
        var result = await _userService.CreateUser(newUser);

        // Assert
        Assert.IsFalse(result.ResponseElements.Any());
    }

    // Test para actualizar Usuario
    [TestMethod]
    public async Task UpdateUserFail()
    {
        // Arrange
        _userRepository.Update(Arg.Any<User>()).ThrowsAsyncForAnyArgs(new Exception());
        _unitOfWork.UserRepository.Returns(_userRepository);
        // Act
        var result = await _userService.UpdateUser(new User());

        // Assert
        Assert.IsFalse(result.ResponseElements.Any());
    }
    // Test para borrar Usuario
    [TestMethod]
    public async Task DeleteUserFail()
    {
        // Arrange
        var userToDelete = _user.First();
        _userRepository.FindAsync(userToDelete.Id).ReturnsForAnyArgs(Task.FromResult<User>(null));

        // Act
        var result = await _userService.DeleteUser(userToDelete.Id);

        // Assert
        Assert.IsFalse(result.ResponseElements.Any());
    }

    // Test para traer todos los usuarios
    [TestMethod]
    public async Task GetAllUserFail()
    {
        // Arrange
        _userRepository.GetAllAsync().Returns(new List<User>());

        // Act
        var result = await _userService.Index();

        // Assert
        Assert.IsFalse(result.ResponseElements.Any());
    }


    // Test para traer por el ID
    [TestMethod]
    public async Task GetUserByIdFail()
    {
        // Arrange
        var user = _user.First();
        _userRepository.FindAsync(user.Id).ReturnsForAnyArgs(Task.FromResult<User>(null));

        // Act
        var result = await _userService.GetUserById(user.Id);

        // Assert
        Assert.IsFalse(result.ResponseElements.Any());
    }

    // Test para traer por el nombre
    [TestMethod]
    public async Task GetUserByNameFail()
    {
        // Arrange
        var user = _user.First();
        _userRepository.GetAllAsync(Arg.Any<Expression<Func<User, bool>>>()).ReturnsForAnyArgs(new List<User>());

        // Act
        var result = await _userService.GetUserByName(user.Name);

        // Assert
        Assert.IsFalse(result.ResponseElements.Any());
    }


    // Test para traer por el apellido
    [TestMethod]
    public async Task GetUserByLastNameFail()
    {
        // Arrange
        var user = _user.First();
        _userRepository.GetAllAsync(Arg.Any<Expression<Func<User, bool>>>()).ReturnsForAnyArgs(new List<User>());

        // Act
        var result = await _userService.GetUserByLastName(user.LastName);

        // Assert
        Assert.IsFalse(result.ResponseElements.Any());
    }

    // Test para traer por Email
    [TestMethod]
    public async Task GetUserByEmailFail()
    {
        // Arrange
        var user = _user.First();
        _userRepository.GetAllAsync(Arg.Any<Expression<Func<User, bool>>>()).ReturnsForAnyArgs(new List<User>());

        // Act
        var result = await _userService.GetUserByEmail(user.Email);

        // Assert
        Assert.IsFalse(result.ResponseElements.Any());
    }
}
