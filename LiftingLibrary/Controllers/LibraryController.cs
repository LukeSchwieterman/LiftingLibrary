using LiftingLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;

namespace LiftingLibrary.Controllers
{
    public class LibraryController : Controller
    {
        private readonly GlobalUser _globalUser;
        public LibraryController(GlobalUser globalUser)
        {
            _globalUser = globalUser;
        }

        public IActionResult ExerciseSuggestions()
        {
            return View();
        }

        public IActionResult Progression()
        {
            return View();
        }

        public IActionResult Workouts()
        {
            return View();
        }

        public IActionResult WorkoutsWithSearchDate(DateTime searchDate)
        {
            ViewBag.SearchDate = TempData["SearchDate"];
            return View("Workouts");
        }

        public IActionResult WorkoutCreator()
        {
            return View();
        }

        public IActionResult WorkoutDetailsCreator()
        {
            return View();
        }

        public IActionResult OneRepMax()
        {
            return View();
        }
    }
}
