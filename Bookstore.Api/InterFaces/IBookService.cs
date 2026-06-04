using BookStore.Api.Models;

namespace BookStore.Api.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();

        Task<Book?> GetBookByIdAsync(int id);

        Task CreateBookAsync(Book book);

        Task UpdateBookAsync(Book book);

        Task DeleteBookAsync(int id);
    }
}