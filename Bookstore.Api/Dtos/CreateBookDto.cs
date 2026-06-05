using System.ComponentModel.DataAnnotations;

namespace BookStore.Api.DTOs
{
    public class CreateBookDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Range(1, double.MaxValue)]
        public double Price { get; set; }

        [Range(1, int.MaxValue)]
        public int AuthorId { get; set; }
    }
}