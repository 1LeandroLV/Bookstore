using BookStore.Api.Models;

namespace BookStore.Api.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();

        Task<Author?> GetAuthorByIdAsync(int id);

        Task CreateAuthorAsync(Author author);

        Task UpdateAuthorAsync(Author author);

        Task DeleteAuthorAsync(int id);
    }
}