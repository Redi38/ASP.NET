using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Task_6.Models;

public class TestController : Controller
{
    [HttpGet]
    public IActionResult Start()
    {
        var userName = HttpContext.Session.GetString("UserName");
        if (userName == null)
            return RedirectToAction("Login", "Home");

        var users = DataStorage.LoadUsers();
        var user = users.FirstOrDefault(u => u.UserName == userName);
        if (user == null)
            return RedirectToAction("Login", "Home");

        if (user.TestAttempts >= AdminStorage.MaxTestAttempts)
        {
            TempData["Error"] = "Ви вичерпали кількість спроб проходження тесту.";
            return RedirectToAction("Welcome", "Home");
        }

        user.TestAttempts++;
        DataStorage.SaveUsers(users);

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
        if (!TempData.ContainsKey("Questions") || !TempData.ContainsKey("Current"))
            return RedirectToAction("Welcome", "Home");

        var questions = JsonSerializer.Deserialize<List<TestQuestion>>(TempData["Questions"]!.ToString()!)!;
        int current = Convert.ToInt32(TempData["Current"]);

        if (current >= questions.Count)
            return RedirectToAction("Result");

        TempData.Keep(); // зберігаємо TempData для наступного запиту
        ViewBag.Question = questions[current];
        ViewBag.Index = current;
        return View();
    }

    [HttpPost]
    public IActionResult Question(IFormCollection form)
    {
        var questions = JsonSerializer.Deserialize<List<TestQuestion>>(TempData["Questions"]!.ToString()!)!;
        int current = Convert.ToInt32(TempData["Current"]);
        int score = Convert.ToInt32(TempData["Score"]);

        var q = questions[current];

        if (q.Type == "radio")
        {
            var selected = Convert.ToInt32(form["selectedOptions"]);
            if (q.CorrectIndexes.Contains(selected)) score += 10;
        }
        else if (q.Type == "checkbox")
        {
            var selected = form["selectedOptions"].Select(s => int.Parse(s!)).ToList();
            if (selected.Count == q.CorrectIndexes.Count && !selected.Except(q.CorrectIndexes).Any())
                score += 10;
        }
        else if (q.Type == "text")
        {
            var answer = form["textAnswer"].ToString().Trim().ToLower();
            if (answer == q.CorrectAnswerText?.Trim().ToLower())
                score += 10;
        }

        TempData["Score"] = score;
        TempData["Current"] = current + 1;
        TempData["Questions"] = JsonSerializer.Serialize(questions);

        return RedirectToAction("Question");
    }

    [HttpGet]
    public IActionResult Result()
    {
        int score = Convert.ToInt32(TempData["Score"]);
        int correctAnswers = score / 10;

        ViewBag.Score = score;
        ViewBag.CorrectAnswers = correctAnswers;

        var userName = HttpContext.Session.GetString("UserName");
        if (userName != null)
        {
            var users = DataStorage.LoadUsers();
            var user = users.FirstOrDefault(u => u.UserName == userName);

            if (user != null && user.TestScore == null)
            {
                user.TestScore = score;
                DataStorage.SaveUsers(users);
            }
        }

        return View();
    }
}
