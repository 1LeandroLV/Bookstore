using BookStore.Api.Data;
using BookStore.Api.Interfaces;
using BookStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetBooksWithAuthorsAsync()
        {
            return await _context.Books
                .Include(b => b.Author)
                .ToListAsync();
        }
    }
}