﻿using katio.Business.Interfaces;
using katio.Data.Models;
using katio.Data.Dto;
using katio.Data;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace katio.Business.Services;

public class AuthorService : IAuthorService
{
    // Lista de autores

    private readonly IUnitOfWork _unitOfWork;

    // Constructor
    public AuthorService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // Traer todos los Autores
    public async Task<BaseMessage<Author>> Index()
    {
        try
        {
            var result = await _unitOfWork.AuthorRepository.GetAllAsync();
            return result.Any() ? Utilities.BuildResponse<Author>
                (HttpStatusCode.OK, BaseMessageStatus.OK_200, result) :
                Utilities.BuildResponse(HttpStatusCode.NotFound, BaseMessageStatus.BOOK_NOT_FOUND, new List<Author>());
        } catch (Exception ex)
        {
            return Utilities.BuildResponse<Author>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        }
    }


    #region Create Update Delete

    // Crear Autores
    public async Task<BaseMessage<Author>> CreateAuthor(Author author)
    {
        var existingAuthor = await _unitOfWork.AuthorRepository.GetAllAsync(a => a.Name == author.Name && a.LastName == author.LastName);

        if (existingAuthor.Any())
        {
            return Utilities.BuildResponse<Author>(HttpStatusCode.Conflict, $"{BaseMessageStatus.BAD_REQUEST_400} | El autor ya está registrado en el sistema.");
        }

        var newAuthor = new Author()
        {
            Name = author.Name,
            LastName = author.LastName,
            Country = author.Country,
            BirthDate = author.BirthDate
        };

        try
        {
            await _unitOfWork.AuthorRepository.AddAsync(newAuthor);
            await _unitOfWork.SaveAsync();
        } 
        catch (Exception ex)
        {
            return Utilities.BuildResponse<Author>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        }

        return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, new List<Author> { newAuthor });
    }

    // Actualizar Autores
    public async Task<BaseMessage<Author>> UpdateAuthor(Author author)
    {
        var result = await _unitOfWork.AuthorRepository.FindAsync(author.Id);
        if (result == null)
        {
            return Utilities.BuildResponse<Author>(HttpStatusCode.NotFound, BaseMessageStatus.AUTHOR_NOT_FOUND, new List<Author>());
        }
        result.Name = author.Name;
        result.LastName = author.LastName;
        result.Country = author.Country;
        result.BirthDate = author.BirthDate;
        await _unitOfWork.SaveAsync();
        try 
        {
            await _unitOfWork.AuthorRepository.Update(result);
            await _unitOfWork.SaveAsync();
            
        } catch (Exception ex)
        {
            return Utilities.BuildResponse<Author>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        }
        return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, new List<Author> { result });
    }

    // Eliminar Autores
    public async Task<BaseMessage<Author>> DeleteAuthor(int id)
    {
        var result = await _unitOfWork.AuthorRepository.FindAsync(id);
        if (result == null)
        {
            return Utilities.BuildResponse<Author>(HttpStatusCode.NotFound, BaseMessageStatus.AUTHOR_NOT_FOUND, new List<Author>());
        }
        try
        {
            await _unitOfWork.AuthorRepository.Delete(result);

        } catch (Exception ex)
        {
            return Utilities.BuildResponse<Author>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        }
        return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, new List<Author> { result });
    }

    #endregion

    #region Find By Author
    //Traer autores por Id
    public async Task<BaseMessage<Author>> GetAuthorById(int id)
    {
        try
        {
            var author = await _unitOfWork.AuthorRepository.FindAsync(id);
            return author != null ? Utilities.BuildResponse<Author>
                (HttpStatusCode.OK, BaseMessageStatus.OK_200, new List<Author> { author }) :
                Utilities.BuildResponse(HttpStatusCode.NotFound, BaseMessageStatus.AUTHOR_NOT_FOUND, new List<Author>());
        } catch (Exception ex)
        {
            return Utilities.BuildResponse<Author>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        }
    }

    // Traer los autores por nombre
    public async Task<BaseMessage<Author>> GetAuthorsByName(string name)
    {
        try
        {
            var result = await _unitOfWork.AuthorRepository.GetAllAsync(b => b.Name.ToLower().Contains(name.ToLower()));
            return result.Any() ? Utilities.BuildResponse<Author>
                (HttpStatusCode.OK, BaseMessageStatus.OK_200, result) :
                Utilities.BuildResponse(HttpStatusCode.NotFound, BaseMessageStatus.AUTHOR_NOT_FOUND, new List<Author>());
        } catch (Exception ex)
        {
            return Utilities.BuildResponse<Author>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        }
    }
    // Traer los autores por apellido
    //este metodo
    public async Task<BaseMessage<Author>> GetAuthorsByLastName(string LastName)
    {
        try
        {
            var result = await _unitOfWork.AuthorRepository.GetAllAsync(b => b.LastName.ToLower().Contains(LastName.ToLower()));
            return result.Any() ? Utilities.BuildResponse<Author>
                (HttpStatusCode.OK, BaseMessageStatus.OK_200, result) :
                Utilities.BuildResponse(HttpStatusCode.NotFound, BaseMessageStatus.AUTHOR_NOT_FOUND, new List<Author>());
        } catch (Exception ex)
        {
            return Utilities.BuildResponse<Author>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        }
    }
    // Traer los autores por pais - region
    public async Task<BaseMessage<Author>> GetAuthorsByCountry(string Country)
    {
        try
        {
            var result = await _unitOfWork.AuthorRepository.GetAllAsync(b => b.Country.ToLower().Contains(Country.ToLower()));
            return result.Any() ? Utilities.BuildResponse<Author>
                (HttpStatusCode.OK, BaseMessageStatus.OK_200, result) :
                Utilities.BuildResponse(HttpStatusCode.NotFound, BaseMessageStatus.AUTHOR_NOT_FOUND, new List<Author>());
        } catch (Exception ex)
        {
            return Utilities.BuildResponse<Author>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        }
    }
    // Traer los autores por rango de fecha de nacimiento
    public async Task<BaseMessage<Author>> GetAuthorsByBirthDate(DateOnly StartDate, DateOnly EndDate)
    {
        try
        {
            var result = await _unitOfWork.AuthorRepository.GetAllAsync(b => b.BirthDate >= StartDate && b.BirthDate <= EndDate);
            return result.Any() ? Utilities.BuildResponse<Author>
                (HttpStatusCode.OK, BaseMessageStatus.OK_200, result) :
                Utilities.BuildResponse(HttpStatusCode.NotFound, BaseMessageStatus.AUTHOR_NOT_FOUND, new List<Author>());
        } catch (Exception ex)
        {
            return Utilities.BuildResponse<Author>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        }
    }
    #endregion
}
