﻿using katio.Data.Dto;
using katio.Data.Models;

namespace katio.Business.Interfaces;

// Interfaz de Narrador con el servicio

public interface INarratorService
{
    Task<BaseMessage<Narrator>> Index();
    Task<BaseMessage<Narrator>> GetNarratorsByName(string Name);
    Task<BaseMessage<Narrator>> GetNarratorsByLastName(string LastName);
    Task<BaseMessage<Narrator>> GetNarratorsByGenre(string Genre);
    Task<BaseMessage<Narrator>> CreateNarrator(Narrator narrator);
    Task<Narrator> UpdateNarrator(Narrator narrator);
    Task<BaseMessage<Narrator>> DeleteNarrator(int id);
    
}
