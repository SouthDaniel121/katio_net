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
        } catch (Exception ex) 
        {
            return Utilities.BuildResponse<User>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        }
    }

    
   #region Usuarios || Crear → Actualizar → Borrar

   // Crear Usuarios
   public async Task<BaseMessage<User>> CreateUser(User user)
   {
       var existingUser = await _unitOfWork.UserRepository.GetAllAsync(n => n.Nombre == user.Nombre && n.Apellido == user.Apellido);

       if (existingUser.Any())
       {
           return Utilities.BuildResponse<User>(HttpStatusCode.Conflict, $"{BaseMessageStatus.BAD_REQUEST_400} | El Usuario es Invalido.");
       }
       var newUser = new User()
       {
           Nombre = user.Nombre,
           Apellido = user.Apellido,
           Email = user.Email,
           Telefono = user.Telefono,
           Identificacion = user.Identificacion,
           Password = user.Password,
           Username =  user.Username

       };
       try
       {
           await _unitOfWork.UserRepository.AddAsync(newUser);
           await _unitOfWork.SaveAsync();

       } catch (Exception ex)
       {
           return Utilities.BuildResponse<User>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
       }
       return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, new List<User> { newUser });
   }


   // Actualizar Usuarios
   public async Task<BaseMessage<User>> UpdateUser(User user)
   {
       var result= await _unitOfWork.UserRepository.FindAsync(user.Id);
       if (result == null)
       {
           return Utilities.BuildResponse<User>(HttpStatusCode.NotFound, BaseMessageStatus.USER_NOT_FOUND, new List<User>());
       }

           result.Nombre = user.Nombre;
           result.Apellido = user.Apellido;
           result.Email = user.Email;
           result.Telefono = user.Telefono;
           result.Identificacion = user.Identificacion;
           result.Password = user.Password;
           result.Username =  user.Username;


       try 
       {
           await _unitOfWork.UserRepository.AddAsync(result);
           await _unitOfWork.SaveAsync();

       } catch (Exception ex)
       {
           return Utilities.BuildResponse<User>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
       }
       return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, new List<User> { result });
   }
   // Eliminar Usuarios
   public async Task<BaseMessage<User>> DeleteUser(int id)
   {
       var result = await _unitOfWork.UserRepository.FindAsync(id);
       if (result == null)
       {
           return Utilities.BuildResponse<User>(HttpStatusCode.NotFound, BaseMessageStatus.NARRATOR_NOT_FOUND, new List<User>());
       }
       try
       {
           await _unitOfWork.UserRepository.Delete(result);

       } catch (Exception ex)
       {
           return Utilities.BuildResponse<User>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
       }
       return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, new List<User> { result });
   }
   #endregion

   #region  Busqueda 
   //Buscar usuario por Id
   public async Task<BaseMessage<User>> GetUserById(int id)
   {
       try
       {
           var result = await _unitOfWork.UserRepository.FindAsync(id);
           return result != null ? Utilities.BuildResponse<User>
               (HttpStatusCode.OK, BaseMessageStatus.OK_200, new List<User> { result }) :
               Utilities.BuildResponse(HttpStatusCode.NotFound, BaseMessageStatus.BOOK_NOT_FOUND, new List<User>());
       } catch (Exception ex)
       {
           return Utilities.BuildResponse<User>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
       }

   }

   // Buscar usuarios por Nombre
   public async Task<BaseMessage<User>> GetUserByName(string nombre)
   {
       try
       {
           var result = await _unitOfWork.UserRepository.GetAllAsync(a => a.Nombre.ToLower().Contains(nombre.ToLower()));
           return result.Any() ? Utilities.BuildResponse<User>
               (HttpStatusCode.OK, BaseMessageStatus.OK_200, result) :
               Utilities.BuildResponse(HttpStatusCode.NotFound, BaseMessageStatus.USER_NOT_FOUND, new List<User>());
       } catch (Exception ex)
       {
           return Utilities.BuildResponse<User>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
       }
   }

   // Buscar usuario por Apellido  
   public async Task<BaseMessage<User>> GetUserByLastName(string apellido)
   {
       try
       {
           var result = await _unitOfWork.UserRepository.GetAllAsync(b => b.Apellido.ToLower().Contains(apellido.ToLower()));
           return result.Any()
               ? Utilities.BuildResponse<User>(HttpStatusCode.OK, BaseMessageStatus.OK_200, result)
               : Utilities.BuildResponse(HttpStatusCode.NotFound, BaseMessageStatus.USER_NOT_FOUND, new List<User>());
       } catch (Exception ex)
       {
           return Utilities.BuildResponse<User>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
       }
   }

   // Buscar usuarios por Genero
   public async Task<BaseMessage<User>> GetUserByEmail(string email)
   {
       try
       {
           var result = await _unitOfWork.UserRepository.GetAllAsync(b => b.Email.ToLower().Contains(email.ToLower()));
           return result.Any()
               ? Utilities.BuildResponse<User>(HttpStatusCode.OK, BaseMessageStatus.OK_200, result)
               : Utilities.BuildResponse(HttpStatusCode.NotFound, BaseMessageStatus.USER_NOT_FOUND, new List<User>());
       } catch (Exception ex)
       {
           return Utilities.BuildResponse<User>(HttpStatusCode.InternalServerError,$"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500},{ex.Message}");
       }
   }

   #endregion

}

