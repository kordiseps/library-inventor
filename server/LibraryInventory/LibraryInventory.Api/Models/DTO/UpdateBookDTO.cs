namespace LibraryInventory.Api.Models.DTO
{
    public class UpdateBookDTO
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public string PublishedIn { get; set; }
        public string Lang { get; set; }
    }

}
