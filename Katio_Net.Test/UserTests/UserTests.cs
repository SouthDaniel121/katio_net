using NSubstitute;
using katio.Data;
using katio.Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using katio.Business.Interfaces;
using katio.Business.Services;
using System.Linq.Expressions;

namespace katio.Test.UserTests;

[TestClass]
public class UserTests
{
    private readonly IRepository<int, User> _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserService _userService;
    private List<User> _user;

    public UserTests()
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

            Identificacion = "10333658944"
            },
            new User
            { 
            Name = "Manuela",
            LastName = "Ruiz",
            Email = "manu@gmail.com",
            Telefono = "3115822149",
            Password = "12345",
            Identificacion = "10333658945"
            }
        };
    }

    // Test para crear usuario
    [TestMethod]
    public async Task CreateUser()
    {
        // Arrange
        var newUser = new User 
        { 
            Name = "Maria Camila",
            LastName = "Gil Rojas",
            Email = "cami@gmail.com",
            Telefono = "3015822126",
            Password = "1234",
            Identificacion = "10333658944",
        };
        _userRepository.GetAllAsync(Arg.Any<Expression<Func<User, bool>>>()).Returns(new List<User>());
        _userRepository.AddAsync(newUser).Returns(Task.CompletedTask);

        // Act
        var result = await _userService.CreateUser(newUser);

        // Assert
        Assert.IsTrue(result.ResponseElements.Any());
    }

    // Test para actualizar usuario 
  [TestMethod]
    public async Task UpdateUser()
    {
        // Arrange
        var userToUpdate = _user.First();
        _userRepository.FindAsync(userToUpdate.Id).Returns(userToUpdate);

        var updatedUser = new User
        {
            Name = "Maria Camila",
            LastName = "Gil Rojas",
            Email = "cami@gmail.com",
            Telefono = "3015822126",
            Password = "1234",
            Identificacion = "10333658944"
        };
        _userRepository.FindAsync(updatedUser.Id).Returns(updatedUser);
        _userRepository.Update(updatedUser).Returns(Task.CompletedTask);

        // Act
        var result = await _userService.UpdateUser(updatedUser);

        // Assert
        Assert.IsTrue(result.ResponseElements.Any());
    }
    // Test para traer todos los usuarios
    [TestMethod]
    public async Task GetAllUser() 
    {
        // Arrange
        _userRepository.GetAllAsync().Returns(_user);

        // Act
        var result = await _userService.Index();

        // Assert
        Assert.IsTrue(result.ResponseElements.Any());
    }

    // Test Para traer por ID
    [TestMethod]
    public async Task GetUserById()
    {
        // Arrange
        var user = _user.First();
        _userRepository.FindAsync(user.Id).Returns(user);

        // Act
        var result = await _userService.GetUserById(user.Id);

        // Assert
        Assert.IsTrue(result.ResponseElements.Any());
    }

    // Test para traer user por nombre 
    [TestMethod]
    public async Task GetUserByName()
    {
        // Arrange
        var user = _user.First();
        _userRepository.GetAllAsync(Arg.Any<Expression<Func<User, bool>>>()).Returns(new List<User> { user });

        // Act
        var result = await _userService.GetUserByName(user.Name);

        // Assert
        Assert.IsTrue(result.ResponseElements.Any());
    }

    // Test para traer usuario por APELLIDO
    [TestMethod]
    public async Task GetUserByLastName()
    {
        // Arrange
        var user = _user.First();
        _userRepository.GetAllAsync(Arg.Any<Expression<Func<User, bool>>>()).Returns(new List<User> { user });

        // Act
        var result = await _userService.GetUserByLastName(user.LastName);

        // Assert
        Assert.IsTrue(result.ResponseElements.Any());
    }

    // Test para traer usuario por EMAIL
    [TestMethod]
    public async Task GetUserByEmail()
    {
        // Arrange
        var user = _user.First();
        _userRepository.GetAllAsync(Arg.Any<Expression<Func<User, bool>>>()).Returns(new List<User> { user });

        // Act
        var result = await _userService.GetUserByEmail(user.Email);

        // Assert
        Assert.IsTrue(result.ResponseElements.Any());
    }

    // Test para traer usuario por identificacio CC,Dni u pasaporte entre otros...
    [TestMethod]
    public async Task GetUserByIdentificacion()
    {
        // Arrange
        var user = _user.First();
        _userRepository.GetAllAsync(Arg.Any<Expression<Func<User, bool>>>()).Returns(new List<User> { user });

        // Act
        var result = await _userService.GetUserByIdentificacion(user.Identificacion);

        // Assert
        Assert.IsTrue(result.ResponseElements.Any());
    }
    
    
}