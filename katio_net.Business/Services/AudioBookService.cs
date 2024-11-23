﻿using katio.Business.Interfaces;
using katio.Data.Models;
using katio.Data.Dto;
using katio.Data;
using System.Net;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace katio.Business.Services;

public class AudioBookService : IAudioBookService
{
    // Lista de libros
    private readonly IUnitOfWork _unitOfWork;

    // Constructor
    public AudioBookService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    // Constructor

    // Traer todos los Audiolibros
    public async Task<BaseMessage<AudioBook>> Index()
    {
        try
        {
            var result = await _unitOfWork.AudioBookRepository.GetAllAsync();
            return result.Any() ? Utilities.BuildResponse<AudioBook>(HttpStatusCode.OK, BaseMessageStatus.OK_200, result) :
                Utilities.BuildResponse(HttpStatusCode.NotFound, BaseMessageStatus.AUDIOBOOK_NOT_FOUND, new List<AudioBook>());
        } catch (Exception ex) {
            return Utilities.BuildResponse<AudioBook>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        }
    }

    #region Crear → Actualizar → Eliminar
    // Crear un Audiolibro
    public async Task<BaseMessage<AudioBook>> CreateAudioBook(AudioBook audioBook)
    {
        var existingAudioBook = await _unitOfWork.AudioBookRepository.GetAllAsync(ab => ab.ISBN10 == audioBook.ISBN10 || ab.ISBN13 == audioBook.ISBN13);

        if (existingAudioBook.Any())
        {
            return Utilities.BuildResponse<AudioBook>(HttpStatusCode.Conflict, $"{BaseMessageStatus.BAD_REQUEST_400} |  Ya hay un audiolibro registrado con el mismo ISBN.");
        }

        var newAudioBook = new AudioBook()
        {
            Name = audioBook.Name,
            ISBN10 = audioBook.ISBN10,
            ISBN13 = audioBook.ISBN13,
            Published = audioBook.Published,
            Edition = audioBook.Edition,
            Genre = audioBook.Genre,
            LenghtInSeconds = audioBook.LenghtInSeconds,
            Path = audioBook.Path,
            NarratorId = audioBook.NarratorId
        };
        try
        {
            await _unitOfWork.AudioBookRepository.AddAsync(newAudioBook);
            await _unitOfWork.SaveAsync();

        } catch (Exception ex)
        {
            return Utilities.BuildResponse<AudioBook>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        }
        return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, new List<AudioBook> { newAudioBook });
    }
    
    // Actualizar un Audiolibro
    public async Task<BaseMessage<AudioBook>> UpdateAudioBook(AudioBook audioBook)
    {
        var result= await _unitOfWork.AudioBookRepository.FindAsync(audioBook.Id);
        if (result == null)
        {
            return Utilities.BuildResponse<AudioBook>(HttpStatusCode.NotFound, BaseMessageStatus.AUDIOBOOK_NOT_FOUND, new List<AudioBook>());
        }
        
        result.Name = audioBook.Name;
        result.ISBN10 = audioBook.ISBN10;
        result.ISBN13 = audioBook.ISBN13;
        result.Published = audioBook.Published;
        result.Edition = audioBook.Edition;
        result.Genre = audioBook.Genre;
        result.LenghtInSeconds = audioBook.LenghtInSeconds;
        result.Path = audioBook.Path;
        result.NarratorId = audioBook.NarratorId;
       
        try 
        {
            await _unitOfWork.AudioBookRepository.Update(result);
            await _unitOfWork.SaveAsync();
            
        } catch (Exception ex)
        {
            return Utilities.BuildResponse<AudioBook>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        }
        return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, new List<AudioBook> {result});
    }

    // Eliminar un Audiolibro
    public async Task<BaseMessage<AudioBook>> DeleteAudioBook(int Id)
    {
        var result = await _unitOfWork.AudioBookRepository.FindAsync(Id);
        if (result == null)
        {
            return Utilities.BuildResponse<AudioBook>(HttpStatusCode.NotFound, BaseMessageStatus.AUDIOBOOK_NOT_FOUND, new List<AudioBook>());
        }
        try
        {
            await _unitOfWork.AudioBookRepository.Delete(result);
        } catch (Exception ex)
        {
            return Utilities.BuildResponse<AudioBook>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        }
        return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, new List<AudioBook> { result });
    }
    #endregion

    #region Find By AudioBook
    // Buscar por id
    public async Task<BaseMessage<AudioBook>> GetAudioBookById(int id)
    {
        try
        {
            var result = await _unitOfWork.AudioBookRepository.FindAsync(id);

            return result != null ? Utilities.BuildResponse<AudioBook>
                (HttpStatusCode.OK, BaseMessageStatus.OK_200, new List<AudioBook> { result }) :
                Utilities.BuildResponse(HttpStatusCode.NotFound, BaseMessageStatus.AUDIOBOOK_NOT_FOUND, new List<AudioBook>());
        } catch (Exception ex)
        {
            return Utilities.BuildResponse<AudioBook>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        }
    } 
    // Buscar por Nombre
    public async Task<BaseMessage<AudioBook>> GetByAudioBookName(string name)
    {
       try
       {
            var result = await _unitOfWork.AudioBookRepository.GetAllAsync(a => a.Name.ToLower().Contains(name.ToLower()));
            return result.Any() ? Utilities.BuildResponse<AudioBook>(HttpStatusCode.OK, BaseMessageStatus.OK_200, result) :
                Utilities.BuildResponse(HttpStatusCode.NotFound, BaseMessageStatus.AUDIOBOOK_NOT_FOUND, new List<AudioBook>());
       } catch (Exception ex)
       {
            return Utilities.BuildResponse<AudioBook>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
       }
    }

    // Buscar por ISBN10
    public async Task<BaseMessage<AudioBook>> GetByAudioBookISBN10(string ISBN10)
    {
        try 
        {
        var result = await _unitOfWork.AudioBookRepository.GetAllAsync(a => a.ISBN10 == ISBN10);
        return result.Any() ? Utilities.BuildResponse<AudioBook>(HttpStatusCode.OK, BaseMessageStatus.OK_200, result) :
            Utilities.BuildResponse(HttpStatusCode.NotFound, BaseMessageStatus.AUDIOBOOK_NOT_FOUND, new List<AudioBook>());
        } catch (Exception ex)
        {
            return Utilities.BuildResponse<AudioBook>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        }
    }

    // Buscar por ISBN13
    public async Task<BaseMessage<AudioBook>> GetByAudioBookISBN13(string ISBN13)
    {
        try 
        {
            var result = await _unitOfWork.AudioBookRepository.GetAllAsync(a => a.ISBN13 == ISBN13);
            return result.Any() ? Utilities.BuildResponse<AudioBook>(HttpStatusCode.OK, BaseMessageStatus.OK_200, result) :
                Utilities.BuildResponse(HttpStatusCode.NotFound, BaseMessageStatus.AUDIOBOOK_NOT_FOUND, new List<AudioBook>());
        } catch (Exception ex)
        {
            return Utilities.BuildResponse<AudioBook>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        }
    }

    // Buscar por Rango de Fecha de Publicación
    public async Task<BaseMessage<AudioBook>> GetByAudioBookPublished(DateOnly startDate, DateOnly endDate)
    {
        try
        {
            var result = await _unitOfWork.AudioBookRepository.GetAllAsync(b => b.Published >= startDate && b.Published <= endDate);
            return result.Any() ? Utilities.BuildResponse<AudioBook>
                (HttpStatusCode.OK, BaseMessageStatus.OK_200, result) :
                Utilities.BuildResponse(HttpStatusCode.NotFound, BaseMessageStatus.AUDIOBOOK_NOT_FOUND, new List<AudioBook>());
        } catch (Exception ex)
        {
            return Utilities.BuildResponse<AudioBook>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        }
    }

    // Buscar por Edición
    public async Task<BaseMessage<AudioBook>> GetByAudioBookEdition(string edition)
    {
        try {
        var result = await _unitOfWork.AudioBookRepository.GetAllAsync(a => a.Edition.ToLower().Contains( edition.ToLower()) );
        return result.Any() ? Utilities.BuildResponse<AudioBook>
                (HttpStatusCode.OK, BaseMessageStatus.OK_200, result) : 
                Utilities.BuildResponse(HttpStatusCode.NotFound, BaseMessageStatus.AUDIOBOOK_NOT_FOUND, new List<AudioBook>());
        } catch (Exception ex)
        {
            return Utilities.BuildResponse<AudioBook>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        }
}

    // Buscar por Género
    public async Task<BaseMessage<AudioBook>> GetByAudioBookGenre(string genre)
    {
        try
        {
            var result = await _unitOfWork.AudioBookRepository.GetAllAsync(a => a.Genre.ToLower().Contains(genre.ToLower()));
            return result.Any()
                ? Utilities.BuildResponse<AudioBook>(HttpStatusCode.OK, BaseMessageStatus.OK_200, result)
                : Utilities.BuildResponse(HttpStatusCode.NotFound, BaseMessageStatus.AUDIOBOOK_NOT_FOUND, new List<AudioBook>());
        } catch (Exception ex)
        {
            return Utilities.BuildResponse<AudioBook>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        }
    }


    // Buscar por Duración en Segundos
    public async Task<BaseMessage<AudioBook>> GetByAudioBookLenghtInSeconds(int lenghtInSeconds)
    {
        try
        {
            var result = await _unitOfWork.AudioBookRepository.FindAsync(lenghtInSeconds);
            return (result != null)
                ? Utilities.BuildResponse<AudioBook>(HttpStatusCode.OK, BaseMessageStatus.OK_200, new List<AudioBook> { result })
                : Utilities.BuildResponse(HttpStatusCode.NotFound, BaseMessageStatus.AUDIOBOOK_NOT_FOUND, new List<AudioBook>());
        } catch (Exception ex)
        {
            return Utilities.BuildResponse<AudioBook>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        }
    }


    #endregion

    #region Find By Narrator

    // Buscar por Narrador
    public async Task<BaseMessage<AudioBook>> GetAudioBookByNarrator(int narratorId)
    {
        try 
        {
            var result = await _unitOfWork.AudioBookRepository.FindAsync(narratorId);
            return (result != null)
                ? Utilities.BuildResponse<AudioBook>(HttpStatusCode.OK, BaseMessageStatus.OK_200, new List<AudioBook> { result })
                : Utilities.BuildResponse(HttpStatusCode.NotFound, BaseMessageStatus.AUDIOBOOK_NOT_FOUND, new List<AudioBook>());
        } catch (Exception ex)
        {
            return Utilities.BuildResponse<AudioBook>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        }
    }


    // Buscar por Nombre de Narrador
    public async Task<BaseMessage<AudioBook>> GetAudioBookByNarratorName(string narratorName)
    {
        try
        {
            var result = await _unitOfWork.AudioBookRepository.GetAllAsync(a => a.Narrator.Name.ToLower().Contains(narratorName.ToLower()),
            includeProperties: "Narrator");
            return result.Any()
                ? Utilities.BuildResponse<AudioBook>(HttpStatusCode.OK, BaseMessageStatus.OK_200, result)
                : Utilities.BuildResponse(HttpStatusCode.NotFound, BaseMessageStatus.AUDIOBOOK_NOT_FOUND, new List<AudioBook>());
        } catch (Exception ex)
        {
            return Utilities.BuildResponse<AudioBook>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        }
    }

    // Buscar por Apellido del Narrador
    public async Task<BaseMessage<AudioBook>> GetAudioBookByNarratorLastName(string narratorLastName)
    {
        try
        {
            var result = await _unitOfWork.AudioBookRepository.GetAllAsync(b => b.Narrator.LastName.ToLower().Contains(narratorLastName.ToLower()),
            includeProperties: "Narrator");
            return result.Any()
                ? Utilities.BuildResponse<AudioBook>(HttpStatusCode.OK, BaseMessageStatus.OK_200, result)
                : Utilities.BuildResponse(HttpStatusCode.NotFound, BaseMessageStatus.AUDIOBOOK_NOT_FOUND, new List<AudioBook>());
        } catch (Exception ex) {
            return Utilities.BuildResponse<AudioBook>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        }
    }

    // Buscar por Nombre y Apellido de Narrador
    public async Task<BaseMessage<AudioBook>> GetAudioBookByNarratorFullName(string narratorName, string narratorLastName)
    {
        try
        {
            var result = await _unitOfWork.AudioBookRepository.GetAllAsync(a => a.Narrator.Name.ToLower().Contains(narratorName.ToLower()) &&
            a.Narrator.LastName.ToLower().Contains(narratorLastName.ToLower()), 
            includeProperties: "Narrator");
            return result.Any()
                ? Utilities.BuildResponse<AudioBook>(HttpStatusCode.OK, BaseMessageStatus.OK_200, result)
                : Utilities.BuildResponse(HttpStatusCode.NotFound, BaseMessageStatus.AUDIOBOOK_NOT_FOUND, new List<AudioBook>());
        } catch (Exception ex)
        {
            return Utilities.BuildResponse<AudioBook>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        }
    }


    public async Task<BaseMessage<AudioBook>> GetAudioBookByNarratorGenre(string genre)
    {
        try
        {
        var result = await _unitOfWork.AudioBookRepository.GetAllAsync(a => a.Narrator.Genre.ToLower().Contains(genre.ToLower()),
        includeProperties: "Narrator");
        return result.Any()
                ? Utilities.BuildResponse<AudioBook>(HttpStatusCode.OK, BaseMessageStatus.OK_200, result)
                : Utilities.BuildResponse(HttpStatusCode.NotFound, BaseMessageStatus.AUDIOBOOK_NOT_FOUND, new List<AudioBook>());
        } catch (Exception ex)
        {
            return Utilities.BuildResponse<AudioBook>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        }
    }
    #endregion



    #region  Buscador 

     public async Task<BaseMessage<Book>> SearchBookAsync(string searchTerm)
    {
        try
        {
            var parameter = Expression.Parameter(typeof(Book), "book");
            var searchExpressions = new List<Expression>();
            var lowerSearchTerm = Expression.Constant(searchTerm.ToLower(), typeof(string));
            
            var nameProperty = Expression.Property(parameter, nameof(Book.Name));
            var nameToLower = Expression.Call(nameProperty, "ToLower", null);
            var nameContains = Expression.Call(
                nameToLower,
                "Contains",
                null,
                lowerSearchTerm
            );
            searchExpressions.Add(nameContains);
            
            var isbn10Property = Expression.Property(parameter, nameof(Book.ISBN10));
            var isbn10ToLower = Expression.Call(isbn10Property, "ToLower", null);
            var isbn10Contains = Expression.Call(
                isbn10ToLower,
                "Contains",
                null,
                lowerSearchTerm
            );
            searchExpressions.Add(isbn10Contains);
            // Search in ISBN13
            var isbn13Property = Expression.Property(parameter, nameof(Book.ISBN13));
            var isbn13ToLower = Expression.Call(isbn13Property, "ToLower", null);
            var isbn13Contains = Expression.Call(
                isbn13ToLower,
                "Contains",
                null,
                lowerSearchTerm
            );
            searchExpressions.Add(isbn13Contains);
        
            var editionProperty = Expression.Property(parameter, nameof(Book.Edition));
            var editionToLower = Expression.Call(editionProperty, "ToLower", null);
            var editionContains = Expression.Call(
                editionToLower,
                "Contains",
                null,
                lowerSearchTerm
            );
            searchExpressions.Add(editionContains);
        
            var deweyIndexProperty = Expression.Property(parameter, nameof(Book.DeweyIndex));
            var deweyIndexToLower = Expression.Call(deweyIndexProperty, "ToLower", null);
            var deweyIndexContains = Expression.Call(
                deweyIndexToLower,
                "Contains",
                null,
                lowerSearchTerm
            );
            searchExpressions.Add(deweyIndexContains);
        
            if (DateOnly.TryParse(searchTerm, out var publishedDate))
            {
                var publishedProperty = Expression.Property(parameter, nameof(Book.Published));
                var publishedEquals = Expression.Equal(publishedProperty, Expression.Constant(publishedDate));
                searchExpressions.Add(publishedEquals);
            }
            
            var body = searchExpressions.Aggregate(Expression.OrElse);
            var lambda = Expression.Lambda<Func<Book, bool>>(body, parameter);
            var result = await _unitOfWork.BookRepository.GetAllAsync(lambda);
            return result.Any() ? Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, result) :
                Utilities.BuildResponse(HttpStatusCode.NotFound, BaseMessageStatus.BOOK_NOT_FOUND, new List<Book>());
        }
        catch (Exception ex)
        {
            return Utilities.BuildResponse<Book>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        }
    }

    public Task<BaseMessage<AudioBook>> SearchAudioBookAsync(string searchTerm)
    {
        throw new NotImplementedException();
    }
    #endregion
}
