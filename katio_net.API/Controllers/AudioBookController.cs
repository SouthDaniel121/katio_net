using Microsoft.AspNetCore.Mvc;
using katio.Business.Interfaces;
using katio.Data.Models;

namespace katio.API.Controllerss
{
    [ApiController]
    [Route("api/[controller]")]
    public class AudioBookController : ControllerBase
    {

        #region Servicio y Contructor
    
        // Servicio de Audiolibros conectado con la interfaz

        private readonly IAudioBookService _audioBookService;


        // Constructor
        public AudioBookController(IAudioBookService audioBookService)
        {
            _audioBookService = audioBookService;
        }

        #endregion

        #region  Todos los audiolibros 
        
        // Trae todos los Audiolibros
        [HttpGet]
        [Route("GetAudioBooks")]
        public async Task<IActionResult> Index()
        {
            var response = await _audioBookService.Index();
            return response.TotalElements > 0 ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }

        #endregion

        #region AudioLibros | Crear →  Eliminar → Actualizar. 

        // Crear un Audiolibro ↓
        [HttpPost]
        [Route("CreateAudioBook")]
        public async Task<IActionResult> CreateAudioBook(AudioBook audioBook)
        {
            var response = await _audioBookService.CreateAudioBook(audioBook);
            return response.StatusCode == System.Net.HttpStatusCode.OK ? Ok(response) : StatusCode((int)response.StatusCode, response);
        }

         // Elimina un Audiolibro ↓
        [HttpDelete]
        [Route("DeleteAudioBook")]
        public async Task<IActionResult> DeleteAudioBook(int id)
        {
            var response = await _audioBookService.DeleteAudioBook(id);
            return response.StatusCode == System.Net.HttpStatusCode.OK ? Ok(response) : StatusCode((int)response.StatusCode, response);
        }

        // Actualiza un Audiolibro ↓
        [HttpPut]
        [Route("UpdateAudioBook")]
        public async Task<IActionResult> UpdateAudioBook(AudioBook audioBook)
        {
            var response = await _audioBookService.UpdateAudioBook(audioBook);
            return response != null ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }

       

        #endregion

        #region Busquedas por audiolibro | Nombre →  ISBN → Duracion → Edicion → Id → Rango Publicacion → Genero Literario. 

    
        // Buscar un Audiolibro por su Nombre
        [HttpGet]
        [Route("FindAudioBookByName")]
        public async Task<IActionResult> GetByAudioBookName(string name)
        {
            var response = await _audioBookService.GetByAudioBookName(name);
            return response != null ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }


           // Buscar un Audiolibro por ISBN10
        [HttpGet]
        [Route("FindAudioBookByISBN10")]
        public async Task<IActionResult> GetByAudioBookISBN10(string isbn10)
        {
            var response = await _audioBookService.GetByAudioBookISBN10(isbn10);
            return response != null ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }

        // Buscar un Audiolibro por ISBN13
        [HttpGet]
        [Route("FindAudioBookByISBN13")]
        public async Task<IActionResult> GetAudioBookByISBN13(string isbn13)
        {
            var response = await _audioBookService.GetByAudioBookISBN13(isbn13);
            return response != null ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }

        // Buscar AudioLibro por su Id
        [HttpGet]
        [Route("FindAudioBookById")]
        public async Task<IActionResult> GetByAudioBookId(int id)
        {
            var response = await _audioBookService.GetAudioBookById(id);
            return response != null ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }

          // Buscar un Audiolibro por su Duracion en Segundos
        [HttpGet]
        [Route("FindAudioBookByLenghtInSeconds")]
        public async Task<IActionResult> GetAudioBookByLenghtInSeconds(int lenghtInSeconds)
        {
            var response = await _audioBookService.GetByAudioBookLenghtInSeconds(lenghtInSeconds);
            return response != null ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }
      
           // Buscar un Audiolibro por su Edicion
        [HttpGet]
        [Route("FindAudioBookByEdition")]
        public async Task<IActionResult> GetByAudioBookEdition(string edition)
        {
            var response = await _audioBookService.GetByAudioBookEdition(edition);
            return response != null ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }
     

        // Buscar un Audiolibro por Rango de Publicacion
        [HttpGet]
        [Route("FindAudioBookByPublishedRange")]
        public async Task<IActionResult> GetAudioBookByPublishedRange(DateOnly startDate, DateOnly endDate)
        {
            var response = await _audioBookService.GetByAudioBookPublished(startDate, endDate);
            return response != null ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }

     

        // Buscar un Audiolibro por su Genero Literario
        [HttpGet]
        [Route("FindAudioBookByGenre")]
        public async Task<IActionResult> GetAudioBookByGenre(string genre)
        {
            var response = await _audioBookService.GetByAudioBookGenre(genre);
            return response != null ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }

        #endregion

        #region Busqueda por Audiolibro relacion con autor | Nombre completo → Nombre → Apellido → Id → Pais → Fecha De Nacimiento. 

         // Buscar un Audiolibro por nombre completo de Autor
        [HttpGet]
        [Route("AudioBookByAuthorFullName")]
        public async Task<IActionResult> GetAudioBookByAuthorFullName(string authorName, string authorLastName)
        {
            var response = await _audioBookService.GetAudioBookByAuthorFullName(authorName, authorLastName);
            return response != null ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }

        // Buscar un Audiolibro por nombre de su Autor
        [HttpGet]
        [Route("AudioBookByAuthorName")]
        public async Task<IActionResult> GetAudioBookByAuthorName(string authorName)
        {
            var response = await _audioBookService.GetAudioBookByAuthorName(authorName);
            return response != null ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }


         // Buscar un Audiolibro por apellido del autor
        [HttpGet]
        [Route("AudioBookByAuthorLastName")]
        public async Task<IActionResult> GetAudioBookByAuthorLastName(string authorLastName)
        {
            var response = await _audioBookService.GetAudioBookByAuthorLastName(authorLastName);
            return response != null ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }

        // Buscar un Audiolibro por su Autor ID
        [HttpGet]
        [Route("AudioBookByAuthor")]
        public async Task<IActionResult> GetAudioBookByAuthor(int authorId)
        {
            var response = await _audioBookService.GetAudioBookByAuthor(authorId);
            return response != null ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }

      
        // Buscar un Audiolibro por pais del autor
        [HttpGet]
        [Route("AudioBookByAuthorCountry")]
        public async Task<IActionResult> GetAudioBookByAuthorCountry(string authorCountry)
        {
            var response = await _audioBookService.GetAudioBookByAuthorCountry(authorCountry);
            return response != null ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }

        // Buscar un Audiolibro por rango de fecha de nacimiento de su Autor
        [HttpGet]
        [Route("AudioBookByAuthorBirthDate")]
        public async Task<IActionResult> GetAudioBookByAuthorBirthDateRange(DateOnly startDate, DateOnly endDate)
        {
            var response = await _audioBookService.GetAudioBookByAuthorBirthDateRange(startDate, endDate);
            return response != null ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }

       

        #endregion

    }
}
