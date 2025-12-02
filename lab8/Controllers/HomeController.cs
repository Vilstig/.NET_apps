using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using lab8.Models;

namespace lab8.Controllers;

public class GameController : Controller
{
    static int MinValue = Int32.MinValue;
    static int MaxValue = -1;
    static int RandValue = -1;
    static int NumOfGuesses = 0;
    public IActionResult Set(int minValue, int maxValue)
    {
        MinValue = minValue;
        MaxValue = maxValue;
        ViewBag.Message = $"Range has been set to {MinValue} … {MaxValue - 1}";
        ViewBag.cssClass = "set";
        return View("Result");
    }

    public IActionResult Draw()
    {
        if (MaxValue <= 0)
        {
            ViewBag.Message = "Set the range first!";
            ViewBag.cssClass = "error";
            return View("Result");
        }

        Random rand = new Random();
        RandValue = rand.Next(MinValue, MaxValue);  // MinValue to MaxValue-1

        NumOfGuesses = 0;

        ViewBag.Message = $"New value has been drawn! Try guessing!";
        ViewBag.cssClass = "draw";

        return View("Result");
    }

    public IActionResult Guess(int userValue)
    {
        if (MaxValue <= 0)
        {
            ViewBag.Message = "Set the range first!";
            ViewBag.cssClass = "error";
            return View("Result");
        }

        if (NumOfGuesses == 0 && RandValue == 0)
        {
            ViewBag.Message = "Draw a number first!";
            ViewBag.cssClass = "error";
            return View("Result");
        }

        NumOfGuesses++;

        if (userValue < MinValue || userValue >= MaxValue)
        {
            ViewBag.Message = $"Enter a number between {MinValue} and {MaxValue - 1}.";
            ViewBag.cssClass = "error";
        }
        else if (userValue < RandValue)
        {
            ViewBag.Message = $"Too low! Attempt #{NumOfGuesses}";
            ViewBag.cssClass = "low";
        }
        else if (userValue > RandValue)
        {
            ViewBag.Message = $"Too high! Attempt #{NumOfGuesses}";
            ViewBag.cssClass = "high";
        }
        else
        {
            ViewBag.Message = $"Correct! You guessed in {NumOfGuesses} tries!";
            ViewBag.cssClass = "correct";
        }

        return View("Result");
    }
}

public class ToolController : Controller
{
    public IActionResult Solve(double a, double b, double c)
    {
        string message;
        string cssClass;
        if (a == 0)
        {
            if (b == 0)
            {
                message = (c == 0 ? "Infinite solutions." : "No solution.");
                cssClass = (c == 0 ? "identical" : "none");
            }
            else
            {
                double x = -c / b;
                message = ($"One real root: x = {x}");
                cssClass = "one";
            }
        }
        double delta = b * b - 4 * a * c;

        if (delta < 0)
        {
            message = ("No real roots.");
            cssClass = "none";
        }
        else if (delta == 0)
        {
            double x = -b / (2 * a);
            message = ($"One real root: x = {x}");
            cssClass = "one";
        }
        else
        {
            double x1 = (-b - Math.Sqrt(delta)) / (2 * a);
            double x2 = (-b + Math.Sqrt(delta)) / (2 * a);
            message = ($"Two real roots: x1 = {x1}, x2 = {x2}");
            cssClass = "two";
        }
        ViewBag.Message = message;
        ViewBag.cssClass = cssClass;

        return View();
    }
}
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
