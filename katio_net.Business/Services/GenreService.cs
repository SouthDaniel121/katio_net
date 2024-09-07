﻿using katio.Business.Interfaces;
using katio.Data.Models;
using katio.Data.Dto;
using katio.Data;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace katio.Business.Services;

public class GenreService : IGenreService
{
    // Lista de géneros
    private readonly katioContext _context;

    // Constructor
    public GenreService(katioContext context)
    {
        _context = context;
    }

    // Traer todos los géneros
    public async Task<BaseMessage<Genre>> Index()
    {
        var result = await _context.Genres.ToListAsync();
        return result.Any() ? Utilities.BuildResponse<Genre>
            (HttpStatusCode.OK, BaseMessageStatus.OK_200, result) :
            Utilities.BuildResponse(HttpStatusCode.NotFound, BaseMessageStatus.BOOK_NOT_FOUND, new List<Genre>());
    }

    #region Crear Actualizar Eliminar

    // Crear géneros
    public async Task<BaseMessage<Genre>> CreateGenre(Genre genre)
    {
        var newGenre = new Genre()
        {
            Name = genre.Name,
            Description = genre.Description
        };
        try
        {
            await _context.Genres.AddAsync(newGenre);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return Utilities.BuildResponse<Genre>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
        }
        return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, new List<Genre> { newGenre });
    }

    // Actualizar géneros
    public async Task<Genre> UpdateGenre(Genre genre)
    {
        var result = _context.Genres.FirstOrDefault(b => b.Id == genre.Id);
        if (result != null)
        {
            result.Name = genre.Name;
            result.Description = genre.Description;
            await _context.SaveChangesAsync();
        }
        return result;
    }

    // Eliminar géneros
    public async Task<BaseMessage<Genre>> DeleteGenre(int id)
    {
        var result = _context.Genres.FirstOrDefault(b => b.Id == id);
        if (result != null)
        {
            _context.Genres.Remove(result);
            await _context.SaveChangesAsync();
            return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, new List<Genre> { result });
        }
        return Utilities.BuildResponse(HttpStatusCode.NotFound, BaseMessageStatus.BOOK_NOT_FOUND, new List<Genre>());
    }

    #endregion

    #region Busqueda en genero LITERARIO

    // Buscar género por Nombre
    public async Task<BaseMessage<Genre>> GetGenresByName(string name)
    {
        var result = await _context.Genres.Where(b => b.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase)).ToListAsync();
        return result.Any() ? Utilities.BuildResponse<Genre>
            (HttpStatusCode.OK, BaseMessageStatus.OK_200, result) :
            Utilities.BuildResponse(HttpStatusCode.NotFound, BaseMessageStatus.BOOK_NOT_FOUND, new List<Genre>());
    }
    public async Task<BaseMessage<Genre>> GetGenresByDescription(string description)
    {
        var result = await _context.Genres.Where(b => b.Description.Contains(description, StringComparison.InvariantCultureIgnoreCase)).ToListAsync();
        return result.Any() ? Utilities.BuildResponse<Genre>
            (HttpStatusCode.OK, BaseMessageStatus.OK_200, result) :
            Utilities.BuildResponse(HttpStatusCode.NotFound, BaseMessageStatus.BOOK_NOT_FOUND, new List<Genre>());
    }

    #endregion
}
