namespace Task_2.Models
{
    public class MessageModel
    {
        public string From { get; set; } = "";
        public string To { get; set; } = "";
        public string Theme { get; set; } = "";
        public string Text { get; set; } = "";
        public DateTime SentAt { get; set; }
    }
}
