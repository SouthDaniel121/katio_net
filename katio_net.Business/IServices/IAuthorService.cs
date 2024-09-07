﻿using katio.Data.Dto;
using katio.Data.Models;

namespace katio.Business.Interfaces;

// Interfaz de Author con el servicio
public interface IAuthorService
{
    Task<BaseMessage<Author>> Index();
    Task<BaseMessage<Author>> GetAuthorsByName(string Name);
    Task<BaseMessage<Author>> GetAuthorsByLastName(string LastName);
    Task<BaseMessage<Author>> GetAuthorsByBirthDate(DateOnly StartDate, DateOnly EndDate);
    Task<BaseMessage<Author>> GetAuthorsByCountry(string Country);
    Task<BaseMessage<Author>> DeleteAuthor(int Id);
    Task<BaseMessage<Author>> CreateAuthor(Author author);
    Task<Author> UpdateAuthor(Author author);
}

