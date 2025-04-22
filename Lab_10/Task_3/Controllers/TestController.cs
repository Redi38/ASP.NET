using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Task_3.Models;

public class TestController : Controller
{
    [HttpGet]
    public IActionResult Start()
    {
        if (HttpContext.Session.GetString("UserName") == null)
            return RedirectToAction("Login", "Home");

        var questions = DataStorage.LoadQuestions()
            .OrderBy(q => Guid.NewGuid())
            .ToList();

        TempData["Questions"] = JsonSerializer.Serialize(questions);
        TempData["Score"] = 0;
        TempData["Current"] = 0;

        return RedirectToAction("Question");
    }


    [HttpGet]
    public IActionResult Question()
    {
        TempData.Keep();

        var questions = JsonSerializer.Deserialize<List<TestQuestion>>(TempData.Peek("Questions") as string ?? "")!;
        int current = Convert.ToInt32(TempData.Peek("Current"));

        if (current >= questions.Count)
            return RedirectToAction("Result");

        ViewBag.Question = questions[current];
        ViewBag.Index = current;
        return View();
    }

    [HttpPost]
    public IActionResult Question(string[]? selectedOptions, string? textAnswer)
    {
        var questions = JsonSerializer.Deserialize<List<TestQuestion>>(TempData["Questions"] as string ?? "")!;
        int current = Convert.ToInt32(TempData["Current"]);
        int score = Convert.ToInt32(TempData["Score"]);

        var q = questions[current];
        bool isCorrect = false;

        if (q.Type == "radio" && selectedOptions?.Length == 1)
        {
            isCorrect = q.CorrectIndexes.Contains(int.Parse(selectedOptions[0]));
        }
        else if (q.Type == "checkbox" && selectedOptions != null)
        {
            var selected = selectedOptions.Select(int.Parse).OrderBy(i => i);
            var correct = q.CorrectIndexes.OrderBy(i => i);
            isCorrect = selected.SequenceEqual(correct);
        }
        else if (q.Type == "text" && textAnswer != null)
        {
            isCorrect = q.CorrectAnswerText?.Trim().ToLower() == textAnswer.Trim().ToLower();
        }

        if (isCorrect) score++;

        TempData["Score"] = score;
        TempData["Current"] = current + 1;
        TempData["Questions"] = JsonSerializer.Serialize(questions);

        return RedirectToAction("Question");
    }

    [HttpGet]
    public IActionResult Result()
    {
        int score = Convert.ToInt32(TempData["Score"]);
        ViewBag.Score = score;
        return View();
    }
}


