namespace BookStore.Api.DTOs
{
    public class CreateBookDto
    {
        public string Title { get; set; } = string.Empty;

        public double Price { get; set; }

        public int AuthorId { get; set; }
    }
}