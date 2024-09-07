using Microsoft.AspNetCore.Mvc;
using katio.Business.Interfaces;
using katio.Data.Models;

namespace katio.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : ControllerBase
    {

        #region  Servicio y Constructor

        // Servicio de genero
        private readonly IGenreService _genreService;

        // Constructor
        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        #endregion

        #region  Trae todo 

        // Trae todos los generos
        [HttpGet]
        [Route("GetGenres")]
        public async Task<IActionResult> Index()
        {
            var response = await _genreService.Index();
            return response.TotalElements > 0 ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }

        #endregion

        #region Crear Genero → Eliminar → Actualizar 

        [HttpPost]
        [Route("CreateGenre")]
        public async Task<IActionResult> CreateGenre(Genre genre)
        {
            var response = await _genreService.CreateGenre(genre);
            return response.StatusCode == System.Net.HttpStatusCode.OK ? Ok(response) : StatusCode((int)response.StatusCode, response);
        }

        [HttpDelete]
        [Route("DeleteGenre")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var response = await _genreService.DeleteGenre(id);
            return response.StatusCode == System.Net.HttpStatusCode.OK ? Ok(response) : StatusCode((int)response.StatusCode, response);
        }

        [HttpPut]
        [Route("UpdateGenre")]
        public async Task<IActionResult> UpdateGenre(Genre genre)
        {
            var response = await _genreService.UpdateGenre(genre);
            return response != null ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }

        

        #endregion

        #region Busqueda por genero → Nombre → Descripcion 

        [HttpGet]
        [Route("GetGenresByName")]
        public async Task<IActionResult> GetGenresByName(string Name)
        {
            var response = await _genreService.GetGenresByName(Name);
            return response.TotalElements > 0 ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }

        [HttpGet]
        [Route("GetGenresByDescription")]
        public async Task<IActionResult> GetGenresByDescription(string Description)
        {
            var response = await _genreService.GetGenresByDescription(Description);
            return response.TotalElements > 0 ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }

        #endregion
    }
}
