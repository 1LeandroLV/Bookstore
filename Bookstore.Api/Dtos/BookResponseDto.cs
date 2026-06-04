namespace BookStore.Api.DTOs
{
    public class BookResponseDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public double Price { get; set; }

        public string AuthorName { get; set; } = string.Empty;
    }
}