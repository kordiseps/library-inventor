namespace LibraryInventory.Api.Controllers
{
    public class GeneralResponse
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }
    public class GeneralRequest
    {
        public int Id { get; set; }
    }

}
