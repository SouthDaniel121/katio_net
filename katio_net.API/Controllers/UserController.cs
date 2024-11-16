using Microsoft.AspNetCore.Mvc;
using katio.Business.Interfaces;
using katio.Data.Models;

namespace katio.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        #region Servicio y Constructor  

        // Servicio de usuarios
        private readonly IUserService _userService;

        // Constructor
        public UserController(IUserService userService)
        
        {
            _userService = userService;
        }


        #endregion


        #region Todos los usuarios


        // Trae todos los Usuarios
        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> Index()
        {
            var response = await _userService.Index();
            return response.TotalElements > 0 ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }


        #endregion

        #region Usuarios | Crear →  Eliminar → Actualizar  

        // Crear Usuario
        [HttpPut]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser(User user)
        {
            var response = await _userService.CreateUser(user);
            return response.StatusCode == System.Net.HttpStatusCode.OK ? Ok(response) : StatusCode((int)response.StatusCode, response);
        }


        // Actualizar Usuarios fallando
        [HttpPost]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser(User user)
        {
            var response = await _userService.UpdateUser(user);
            return response != null ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }

        // Elimina un Usuario
        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = await _userService.DeleteUser(id);
            return response.StatusCode == System.Net.HttpStatusCode.OK ? Ok(response) : StatusCode((int)response.StatusCode, response);
        }

        #endregion

        #region Busqueda por usuario | Nombre → Apellido → Id → Email 

        // Trae un usuario por su id
        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetUserById(int Id)
        {
            var response = await _userService.GetUserById(Id);
            return response != null ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }

        // Trae un usuario por su Nombre
        [HttpGet]
        [Route("GetUserByName")]
        public async Task<IActionResult> GetUserByName(string name)
        {
            var response = await _userService.GetUserByName(name);
            return response.TotalElements > 0 ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }

        // Trae un usuario por su Apellido
        [HttpGet]
        [Route("GetUserByLastName")]
        public async Task<IActionResult> GetUserByLastName(string lastName)
        {
            var response = await _userService.GetUserByLastName(lastName);
            return response.TotalElements > 0 ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }

        // Trae un usuario por su email
        [HttpGet]
        [Route("GetUserByEmail")]
        public async Task<IActionResult> GetUserByEmail(string Email)
        {
            var response = await _userService.GetUserByEmail(Email);
            return response.TotalElements > 0 ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }

        // Trae un usuario por su identificacion
        [HttpGet]
        [Route("GetUserByIdentificacion")]
        public async Task<IActionResult> GetUserByIdentificacion(string Identificacion)
        {
            var response = await _userService.GetUserByIdentificacion(Identificacion);
            return response.TotalElements > 0 ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }

        #endregion 
    }
}
