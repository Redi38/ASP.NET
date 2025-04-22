using Microsoft.AspNetCore.Mvc;
using Task_6.Models;

public class AdminController : Controller
{
    [HttpGet]
    public IActionResult Panel()
    {
        var userName = HttpContext.Session.GetString("UserName");
        var user = DataStorage.LoadUsers().FirstOrDefault(u => u.UserName == userName);

        if (user == null || !user.IsAdmin)
            return RedirectToAction("Login", "Home");

        ViewBag.Users = DataStorage.LoadUsers();
        ViewBag.Questions = DataStorage.LoadQuestions();
        ViewBag.MaxAttempts = AdminStorage.MaxTestAttempts;

        return View();
    }

    [HttpGet]
    public IActionResult Edit(string id)
    {
        var questions = DataStorage.LoadQuestions();
        var q = questions.FirstOrDefault(q => q.Id == id);
        if (q == null) return RedirectToAction("Panel");

        return View(q);
    }

    [HttpPost]
    public IActionResult UpdateAttempts(int newLimit)
    {
        AdminStorage.MaxTestAttempts = newLimit;
        return RedirectToAction("Panel");
    }

    [HttpPost]
    public IActionResult DeleteQuestion(string id)
    {
        var list = DataStorage.LoadQuestions();
        list.RemoveAll(q => q.Id == id);
        DataStorage.SaveQuestions(list);
        return RedirectToAction("Panel");
    }

    [HttpPost]
    public IActionResult EditQuestion(IFormCollection form)
    {
        var questions = DataStorage.LoadQuestions();
        var id = form["Id"];
        var q = questions.FirstOrDefault(q => q.Id == id);
        if (q == null) return RedirectToAction("Panel");

        q.Question = form["Question"].ToString();
        q.Type = form["Type"].ToString();
        q.Options = form["OptionsString"].ToString().Split('|', StringSplitOptions.RemoveEmptyEntries)
            .Select(o => o.Trim()).ToList();
        q.CorrectIndexes = form["CorrectIndexesString"].ToString().Split(',')
            .Where(x => int.TryParse(x, out _)).Select(int.Parse).ToList();
        q.CorrectAnswerText = form["CorrectAnswerText"];

        DataStorage.SaveQuestions(questions);
        return RedirectToAction("Panel");
    }

    [HttpPost]
    public IActionResult AddQuestion(IFormCollection form)
    {
        var questions = DataStorage.LoadQuestions();
        var q = new TestQuestion
        {
            Id = Guid.NewGuid().ToString(),
            Question = form["Question"].ToString(),
            Type = form["Type"].ToString(),
            Options = form["OptionsString"].ToString().Split('|', StringSplitOptions.RemoveEmptyEntries)
                .Select(o => o.Trim()).ToList(),
            CorrectIndexes = form["CorrectIndexesString"].ToString().Split(',')
                .Where(x => int.TryParse(x, out _)).Select(int.Parse).ToList(),
            CorrectAnswerText = form["CorrectAnswerText"]
        };

        questions.Add(q);
        DataStorage.SaveQuestions(questions);
        return RedirectToAction("Panel");
    }
}
