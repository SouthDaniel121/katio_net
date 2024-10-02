using katio.Data.Models;
using katio.Data;

namespace katio.Data;
public interface IUnitOfWork
{
    
    IRepository<int, Genre> GenreRepository { get; }
    IRepository<int, Narrator> NarratorRepository { get; } 
    IRepository<int, Book> BookRepository { get; }
    IRepository<int, Author> AuthorRepository { get; }
    IRepository<int, AudioBook> AudioBookRepository { get; }
 
    Task SaveAsync();

}