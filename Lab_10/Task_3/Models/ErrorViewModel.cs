namespace Task_1.Models
{
    public class ApplicationDbContext
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
