public class User
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    public DateTime BirthDate { get; set; }
    public int? TestScore { get; set; }
    public int TestAttempts { get; set; } = 0;
}
