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

        // Servicio de narradores
        private readonly IUserService _userService;

        // Constructor
        public UserController(IUserService userService)
        
        {
            _userService = userService;
        }


        #endregion


        #region Todos los narradores 


        // Trae todos los Usuarios
        [HttpGet]
        [Route("GetUsers")]
        public async Task<IActionResult> Index()
        {
            var response = await _userService.Index();
            return response.TotalElements > 0 ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }


        #endregion

        #region Narradores | Crear →  Eliminar → Actualizar  

        // Crear Usuario
        [HttpPut]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateNarrator(User user)
        {
            var response = await _userService.CreateUser(user);
            return response.StatusCode == System.Net.HttpStatusCode.OK ? Ok(response) : StatusCode((int)response.StatusCode, response);
        }

        // Actualizar Narradores
        [HttpPost]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateNarrator(User user)
        {
            var response = await _userService.UpdateUser(user);
            return response != null ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }

        // Elimina un Narrador
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
        public async Task<IActionResult> GetUserByGenre(string Email)
        {
            var response = await _userService.GetUserByEmail(Email);
            return response.TotalElements > 0 ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }

        #endregion 
    }
}
