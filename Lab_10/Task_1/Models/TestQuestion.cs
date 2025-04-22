namespace Task_1.Models
{
    public class TestQuestion
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Question { get; set; } = "";
        public List<string> Options { get; set; } = new();
        public int CorrectIndex { get; set; }
    }
}
