using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Task_1.Models;

public class TestController : Controller
{
    private readonly string QuestionsPath = Path.Combine(Directory.GetCurrentDirectory(), "App_Data", "math_questions.json");

    [HttpGet]
    public IActionResult Start()
    {
        if (HttpContext.Session.GetString("UserName") == null)
            return RedirectToAction("Login", "Home");

        var questions = LoadQuestions();
        HttpContext.Session.SetString("Questions", JsonSerializer.Serialize(questions));
        HttpContext.Session.SetInt32("Score", 0);
        HttpContext.Session.SetInt32("Current", 0);

        return RedirectToAction("Question");
    }

    [HttpGet]
    public IActionResult Question()
    {
        var json = HttpContext.Session.GetString("Questions");
        var questions = JsonSerializer.Deserialize<List<TestQuestion>>(json!)!;
        int current = HttpContext.Session.GetInt32("Current") ?? 0;

        if (current >= questions.Count)
            return RedirectToAction("Result");

        ViewBag.Question = questions[current];
        ViewBag.Index = current;
        return View();
    }

    [HttpPost]
    public IActionResult Question(int selectedOption)
    {
        var json = HttpContext.Session.GetString("Questions");
        var questions = JsonSerializer.Deserialize<List<TestQuestion>>(json!)!;
        int current = HttpContext.Session.GetInt32("Current") ?? 0;
        int score = HttpContext.Session.GetInt32("Score") ?? 0;

        if (questions[current].CorrectIndex == selectedOption)
            score++;

        HttpContext.Session.SetInt32("Current", current + 1);
        HttpContext.Session.SetInt32("Score", score);

        return RedirectToAction("Question");
    }

    [HttpGet]
    public IActionResult Result()
    {
        int score = HttpContext.Session.GetInt32("Score") ?? 0;
        ViewBag.Score = score;
        return View();
    }

    private List<TestQuestion> LoadQuestions()
    {
        if (!System.IO.File.Exists(QuestionsPath))
            return new List<TestQuestion>();

        var json = System.IO.File.ReadAllText(QuestionsPath);
        return JsonSerializer.Deserialize<List<TestQuestion>>(json)!;
    }
}
