using Microsoft.AspNetCore.Mvc;
using katio.Business.Interfaces;
using katio.Data.Models;

namespace katio.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {


        #region Servicio y Constructor
        
        // Servicio de libros
        private readonly IBookService _bookService;

        // Constructor
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        #endregion

        #region Trae todo 

        // Trae todos los libros
        [HttpGet]
        [Route("GetBooks")]
        public async Task<IActionResult> Index()
        {
            var response = await _bookService.Index();
            return response.TotalElements > 0 ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        } 
          
          #endregion
    
        #region Crear Libro → Eliminar → Actualizar 

        // Crear un libro
        [HttpPost]
        [Route("CreateBook")]
        public async Task<IActionResult> CreateBook(Book book)
        {
            var response = await _bookService.CreateBook(book);
            return response.StatusCode == System.Net.HttpStatusCode.OK ? Ok(response) : StatusCode((int)response.StatusCode, response);
        }

          //Eliminar un libro
        [HttpDelete]
        [Route("DeleteBook")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var response = await _bookService.DeleteBook(id);
            return response.StatusCode == System.Net.HttpStatusCode.OK ? Ok(response) : StatusCode((int)response.StatusCode, response);
        }


        // Actualizar un libro
        [HttpPut]
        [Route("UpdateBook")]
        public async Task<IActionResult> UpdateBook(Book book)
        {
            var response = await _bookService.UpdateBook(book);
            return response != null ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }

      

        #endregion

        #region Busqueda libros por Nombre → Isbn → Edicion → Dewey → Fecha publicacion 

        //Traer un libro por su nombre
        [HttpGet]
        [Route("GetBookByName")]

        // Traer libro por Nombre
        public async Task<IActionResult> GetBookByName(string name)
        {
            var response = await _bookService.GetBooksByName(name);
            return response != null ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }

        // Traer un libro por su ISBN10
        [HttpGet]
        [Route("GetBookByISBN10")]
        public async Task<IActionResult> GetBookByISBN10(string isbn10)
        {
            var response = await _bookService.GetBooksByISBN10(isbn10);
            return response != null ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }

        // Traer un libro por su ISBN13
        [HttpGet]
        [Route("GetBookByISBN13")]
        public async Task<IActionResult> GetBookByISBN13(string isbn13)
        {
            var response = await _bookService.GetBooksByISBN13(isbn13);
            return response != null ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }

        // Traer un libro por su edición
        [HttpGet]
        [Route("GetBookByEdition")]
        public async Task<IActionResult> GetBookByEdition(string edition)
        {
            var response = await _bookService.GetBooksByEdition(edition);
            return response != null ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }

        // Traer un libro por su índice Dewey
        [HttpGet]
        [Route("GetBookByDeweyIndex")]
        public async Task<IActionResult> GetBookByDeweyIndex(string deweyIndex)
        {
            var response = await _bookService.GetBooksByDeweyIndex(deweyIndex);
            return response != null ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }

        
        // Traer libros por rango de fecha de publicación
        [HttpGet]
        [Route("GetBooksByPublished")]
        public async Task<IActionResult> GetBookByPublished(DateOnly StartDate, DateOnly EndDate)
        {
            var response = await _bookService.GetBooksByPublished(StartDate, EndDate);
            return response != null ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }


        #endregion
    
        #region Busqueda libros por Relacion Autores Nombre completo → Nombre → Libro por autor 

        // Traer un libro por nombre completo del autor
        [HttpGet]
        [Route("GetBookByAuthorFullName")]
        public async Task<IActionResult> GetBookByAuthorFullName(string authorName, string authorLastName)
        {
            var response = await _bookService.GetBookByAuthorFullNameAsync(authorName, authorLastName);
            return response != null ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }

        // Traer un libro por nombre del autor
        [HttpGet]
        [Route("GetBookByAuthorName")]
        public async Task<IActionResult> GetBookByAuthorName(string AuthorName)
        {
            var response = await _bookService.GetBookByAuthorNameAsync(AuthorName);
            return response != null ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }
      
        
        // Trae un libro por su autor
        [HttpGet]
        [Route("GetBookByAuthor")]
        public async Task<IActionResult> GetBookByAuthor(int AuthorId)
        {
            var response = await _bookService.GetBookByAuthorAsync(AuthorId);
            return response != null ? Ok(response) : StatusCode(StatusCodes.Status404NotFound, response);
        }
      

        #endregion
    }
}
