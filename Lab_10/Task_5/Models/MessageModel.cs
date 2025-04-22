namespace Task_5.Models
{
    public class MessageModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string From { get; set; } = "";
        public string To { get; set; } = "";
        public string Theme { get; set; } = "";
        public string Text { get; set; } = "";
        public DateTime SentAt { get; set; }
    }
}
