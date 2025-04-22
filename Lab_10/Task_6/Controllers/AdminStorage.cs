using System.Text.Json;

public static class AdminStorage
{
    private static string ConfigPath => Path.Combine(Directory.GetCurrentDirectory(), "App_Data", "config.json");

    public static int MaxTestAttempts
    {
        get
        {
            if (!File.Exists(ConfigPath))
                return 2;

            var json = File.ReadAllText(ConfigPath);
            var dict = JsonSerializer.Deserialize<Dictionary<string, int>>(json);
            return dict != null && dict.ContainsKey("MaxAttempts") ? dict["MaxAttempts"] : 2;
        }

        set
        {
            var dict = new Dictionary<string, int> { { "MaxAttempts", value } };
            var json = JsonSerializer.Serialize(dict, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(ConfigPath, json);
        }
    }
}
