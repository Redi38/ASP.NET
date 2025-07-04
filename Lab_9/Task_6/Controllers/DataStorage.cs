using System.Text.Json;
using Task_6.Models;

public static class DataStorage
{
    private static readonly string AppDataPath = Path.Combine(Directory.GetCurrentDirectory(), "App_Data");

    private static string UsersPath => Path.Combine(AppDataPath, "users.json");
    private static string MessagesPath => Path.Combine(AppDataPath, "messages.json");

    public static List<User> LoadUsers()
    {
        EnsureFileExists(UsersPath);
        var json = File.ReadAllText(UsersPath);
        return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
    }

    public static void SaveUsers(List<User> users)
    {
        EnsureDirectoryExists(AppDataPath);
        var json = JsonSerializer.Serialize(users);
        File.WriteAllText(UsersPath, json);
    }

    public static List<MessageModel> LoadMessages()
    {
        EnsureFileExists(MessagesPath);
        var json = File.ReadAllText(MessagesPath);
        return JsonSerializer.Deserialize<List<MessageModel>>(json) ?? new List<MessageModel>();
    }

    public static void SaveMessages(List<MessageModel> messages)
    {
        EnsureDirectoryExists(AppDataPath);
        var json = JsonSerializer.Serialize(messages);
        File.WriteAllText(MessagesPath, json);
    }

    private static void EnsureDirectoryExists(string path)
    {
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
    }

    private static void EnsureFileExists(string filePath)
    {
        EnsureDirectoryExists(Path.GetDirectoryName(filePath)!);
        if (!File.Exists(filePath))
            File.WriteAllText(filePath, "[]");
    }
}
