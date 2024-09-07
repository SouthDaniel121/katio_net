using Microsoft.AspNetCore.Mvc;
using katio.Business.Interfaces;
using katio.Data.Models;

namespace katio.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {

        #region  Servicio y Contructor

        // Servicio de autor
        private readonly IAuthorService _authorService;

        // Constructor de autor
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        #endregion


        #region  Traer todos 

        // Trae todos los autores
        [HttpGet]
        [Route("GetAllAuthors")]
        public async Task<IActionResult> Index()
        {
            var response = await _authorService.Index();
            return response.TotalElements > 0 ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }

        #endregion

        #region Crear → Eliminar → Actualizar 

        // Crear Autores
        [HttpPost]
        [Route("CreateAuthor")]
        public async Task<IActionResult> CreateAuthor(Author author)
        {
            var response = await _authorService.CreateAuthor(author);
            return response.StatusCode == System.Net.HttpStatusCode.OK ? Ok(response) : StatusCode((int)response.StatusCode, response);
        }

         // Elimina un Autor
        [HttpDelete]
        [Route("DeleteAuthor")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var response = await _authorService.DeleteAuthor(id);
            return response.StatusCode == System.Net.HttpStatusCode.OK ? Ok(response) : StatusCode((int)response.StatusCode, response);
        }

        // Actualizar Autores
        [HttpPut]
        [Route("UpdateAuthor")]
        public async Task<IActionResult> UpdateAuthor(Author author)
        {
            var response = await _authorService.UpdateAuthor(author);
            return response != null ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }

        #endregion


        #region Busqueda por Fecha (Rango) → Nombre → Apellido → Pais  


          // Trae un autor por rango de fecha
        [HttpGet]
        [Route("GetAuthorByBirthDate")]
        public async Task<IActionResult> GetAuthorByBirthDate(DateOnly startDate, DateOnly endDate)
        {
            var response = await _authorService.GetAuthorsByBirthDate(startDate, endDate);
            return response != null ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }

        // Trae un autor por  nombre
        [HttpGet]
        [Route("GetAuthorByName")]
        public async Task<IActionResult> GetAuthorByName(string name)
        {
            var response = await _authorService.GetAuthorsByName(name);
            return response != null ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }

        // Trae un autor por  apellido
        [HttpGet]
        [Route("GetAuthorByLastName")]
        public async Task<IActionResult> GetAuthorByLastName(string lastName)
        {
            var response = await _authorService.GetAuthorsByLastName(lastName);
            return response != null ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }

        // Trae un autor por  pais 
        [HttpGet]
        [Route("GetAuthorByCountry")]
        public async Task<IActionResult> GetAuthorByCountry(string country)
        {
            var response = await _authorService.GetAuthorsByCountry(country);
            return response != null ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }

        #endregion
        
    }
}
