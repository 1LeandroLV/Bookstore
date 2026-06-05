using BookStore.Api.DTOs;
using BookStore.Api.Interfaces;
using BookStore.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorResponseDto>>> GetAuthors()
        {
            var authors = await _authorService.GetAllAuthorsAsync();

            var response = authors.Select(author => new AuthorResponseDto
            {
                Id = author.Id,
                Name = author.Name
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorResponseDto>> GetAuthor(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            var response = new AuthorResponseDto
            {
                Id = author.Id,
                Name = author.Name
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAuthor(CreateAuthorDto dto)
        {
            var author = new Author
            {
                Name = dto.Name
            };

            await _authorService.CreateAuthorAsync(author);

            return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuthor(int id, UpdateAuthorDto dto)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            author.Name = dto.Name;

            await _authorService.UpdateAuthorAsync(author);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            await _authorService.DeleteAuthorAsync(id);

            return NoContent();
        }
    }
}