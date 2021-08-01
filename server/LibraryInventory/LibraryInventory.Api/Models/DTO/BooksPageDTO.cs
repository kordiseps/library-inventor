namespace LibraryInventory.Api.Models.DTO
{
    public class BooksPageDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string PublishedIn { get; set; }
        public string Lang { get; set; }
    }

}
