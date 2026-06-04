using BookStore.Api.DTOs;
using BookStore.Api.Interfaces;
using BookStore.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookResponseDto>>> GetBooks()
        {
            var books = await _bookService.GetAllBooksAsync();

            var response = books.Select(book => new BookResponseDto
            {
                Id = book.Id,
                Title = book.Title,
                Price = book.Price,
                AuthorName = book.Author?.Name ?? ""
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult> CreateBook(CreateBookDto dto)
        {
            var book = new Book
            {
                Title = dto.Title,
                Price = dto.Price,
                AuthorId = dto.AuthorId
            };

            await _bookService.CreateBookAsync(book);

            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBook(int id, UpdateBookDto dto)
        {
            var book = await _bookService.GetBookByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            book.Title = dto.Title;
            book.Price = dto.Price;
            book.AuthorId = dto.AuthorId;

            await _bookService.UpdateBookAsync(book);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            await _bookService.DeleteBookAsync(id);

            return NoContent();
        }
    }
}