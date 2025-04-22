namespace Task_5.Models;

public class TestQuestion
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Question { get; set; } = "";
    public string Type { get; set; } = "radio"; // radio | checkbox | text
    public List<string> Options { get; set; } = new();      // Для radio, checkbox
    public List<int> CorrectIndexes { get; set; } = new();  // Мульти-варіант
    public string? CorrectAnswerText { get; set; }          // Для текстових
}
