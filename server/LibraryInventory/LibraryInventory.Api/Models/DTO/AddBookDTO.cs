namespace LibraryInventory.Api.Models.DTO
{
    public class AddBookDTO
    {
        public string Name { get; set; }
        public int AuthorId { get; set; }
        public string PublishedIn { get; set; }
        public string Lang { get; set; }
    }

}
