namespace LibraryInventory.Api.Models.Entity
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AuthorId { get; set; }
        public string PublishedIn { get; set; }
        public string Lang { get; set; }
    }
}
