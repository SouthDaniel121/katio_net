using katio.Data.Models;
using katio.Data.Dto;
using katio.Data;
using System.Net;
using Microsoft.EntityFrameworkCore;
using katio.Business.Interfaces;


namespace katio.Business.Services;

public class UserService : IUserService
{
    // Lista de usuarios
    private readonly IUnitOfWork _unitOfWork;

    // Constructor
    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    // Traer todos los Usuarios
    public async Task<BaseMessage<User>> Index()
    {
        try
        {
            var result = await _unitOfWork.UserRepository.GetAllAsync();
            return result.Any() ? Utilities.BuildResponse<User>(HttpStatusCode.OK, BaseMessageStatus.OK_200, result) :
                Utilities.BuildResponse(HttpStatusCode.NotFound, BaseMessageStatus.BOOK_NOT_FOUND, new List<User>());
        }
        catch (Exception ex)
        {
            return Utilities.BuildResponse<User>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        }
    }


    #region Usuarios || Crear → Actualizar → Borrar

    // Crear Usuarios
    public async Task<BaseMessage<User>> CreateUser(User user)
    {
        var existingUser = await _unitOfWork.UserRepository.GetAllAsync(n => n.Name == user.Name && n.LastName == user.LastName);

        if (existingUser.Any())
        {
            return Utilities.BuildResponse<User>(HttpStatusCode.Conflict, $"{BaseMessageStatus.BAD_REQUEST_400} | El Usuario es Invalido.");
        }
        var newUser = new User()
        {
            Name = user.Name,
            LastName = user.LastName,
            Email = user.Email,
            Telefono = user.Telefono,
            Identificacion = user.Identificacion,
            Password = user.Password

        };
        try
        {
            await _unitOfWork.UserRepository.AddAsync(newUser);
            await _unitOfWork.SaveAsync();

        }
        catch (Exception ex)
        {
            return Utilities.BuildResponse<User>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        }
        return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, new List<User> { newUser });
    }


    // Actualizar Usuarios
public async Task<BaseMessage<User>> UpdateUser(User user)
{
    if (user.Name == null || user.LastName == null)
    {
        return Utilities.BuildResponse<User>(HttpStatusCode.BadRequest, BaseMessageStatus.USER_NOT_FOUND);
    }

    var existingUser = await _unitOfWork.UserRepository.GetAllAsync(a => a.Name == user.Name && a.LastName == user.LastName);

    if (!existingUser.Any())
    {
        return Utilities.BuildResponse<User>(HttpStatusCode.NotFound, BaseMessageStatus.USER_NOT_FOUND);
    }

    await _unitOfWork.UserRepository.Update(user);
    await _unitOfWork.SaveAsync();

  
    return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, new List<User> { user });
}

    // Eliminar Usuarios
    public async Task<BaseMessage<User>> DeleteUser(int id)
    {
        var result = await _unitOfWork.UserRepository.FindAsync(id);
        if (result == null)
        {
            return Utilities.BuildResponse<User>(HttpStatusCode.NotFound, BaseMessageStatus.USER_NOT_FOUND, new List<User>());
        }
        try
        {
            await _unitOfWork.UserRepository.Delete(result);

        }
        catch (Exception ex)
        {
            return Utilities.BuildResponse<User>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        }
        return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, new List<User> { result });
    }
    #endregion

    #region  Busquedas

    //Buscar usuario por Id
    public async Task<BaseMessage<User>> GetUserById(int id)
    {
        try
        {
            var result = await _unitOfWork.UserRepository.FindAsync(id);
            return result != null ? Utilities.BuildResponse<User>
                (HttpStatusCode.OK, BaseMessageStatus.OK_200, new List<User> { result }) :
                Utilities.BuildResponse(HttpStatusCode.NotFound, BaseMessageStatus.USER_DELETE_OF_NO_EXIST, new List<User>());
        }
        catch (Exception ex)
        {
            return Utilities.BuildResponse<User>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        }

    }

    // Buscar usuarios por Nombre
    public async Task<BaseMessage<User>> GetUserByName(string name)
    {
        try
        {
            var result = await _unitOfWork.UserRepository.GetAllAsync(a => a.Name.ToLower().Contains(name.ToLower()));
            return result.Any() ? Utilities.BuildResponse<User>
                (HttpStatusCode.OK, BaseMessageStatus.OK_200, result) :
                Utilities.BuildResponse(HttpStatusCode.NotFound, BaseMessageStatus.USER_NOT_FOUND, new List<User>());
        }
        catch (Exception ex)
        {
            return Utilities.BuildResponse<User>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        }
    }

    // Buscar usuario por Apellido  
    public async Task<BaseMessage<User>> GetUserByLastName(string lastname)
    {
        try
        {
            var result = await _unitOfWork.UserRepository.GetAllAsync(b => b.LastName.ToLower().Contains(lastname.ToLower()));
            return result.Any()
                ? Utilities.BuildResponse<User>(HttpStatusCode.OK, BaseMessageStatus.OK_200, result)
                : Utilities.BuildResponse(HttpStatusCode.NotFound, BaseMessageStatus.USER_NOT_FOUND, new List<User>());
        }
        catch (Exception ex)
        {
            return Utilities.BuildResponse<User>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        }
    }

    // Buscar usuarios por EMAIL
    public async Task<BaseMessage<User>> GetUserByEmail(string email)
    {
        try
        {
            var result = await _unitOfWork.UserRepository.GetAllAsync(b => b.Email.ToLower().Contains(email.ToLower()));
            return result.Any()
                ? Utilities.BuildResponse<User>(HttpStatusCode.OK, BaseMessageStatus.OK_200, result)
                : Utilities.BuildResponse(HttpStatusCode.NotFound, BaseMessageStatus.USER_NOT_FOUND, new List<User>());
        }
        catch (Exception ex)
        {
            return Utilities.BuildResponse<User>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500},{ex.Message}");
        }
    }

    // Buscar usuarios por IDENTIFICACION
    public async Task<BaseMessage<User>> GetUserByIdentificacion(string identificacion)
    {
        try
        {
            var result = await _unitOfWork.UserRepository.GetAllAsync(b => b.Identificacion.ToLower().Contains(identificacion.ToLower()));
            return result.Any()
                ? Utilities.BuildResponse<User>(HttpStatusCode.OK, BaseMessageStatus.OK_200, result)
                : Utilities.BuildResponse(HttpStatusCode.NotFound, BaseMessageStatus.USER_NOT_FOUND, new List<User>());
        }
        catch (Exception ex)
        {
            return Utilities.BuildResponse<User>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500},{ex.Message}");
        }
    }

    public Task<BaseMessage<User>> UpdateUser()
    {
        throw new NotImplementedException();
    }

    public Task<BaseMessage<User>> UpdateUser(User user, User newUser)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUser(User user1, object user2)
    {
        throw new NotImplementedException();
    }

    #endregion

}

