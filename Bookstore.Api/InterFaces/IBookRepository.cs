using BookStore.Api.Models;

namespace BookStore.Api.Interfaces
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<IEnumerable<Book>> GetBooksWithAuthorsAsync();
    }
}