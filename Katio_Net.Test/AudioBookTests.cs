using NSubstitute;
using katio.Data;
using katio.Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using katio.Business.Interfaces;
using katio.Business.Services;
using System.Linq.Expressions;
using katio.Data.Dto;
using System.Net;

namespace katio.Test;
    
    // Test para traer todos los audioLibros
    [TestMethod]
    public async Task GetAllAudioBooks() 
    {
        // Arreglo
        _audioBookRepository.GetAllAsync().Returns(_audioBooks);

        // Act
        var result = await _audioBookService.Index();

        // Afirmar - Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.ResponseElements.Count());
    }
