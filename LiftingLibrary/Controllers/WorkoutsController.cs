using LiftingLibrary.DAO.Workouts;
using LiftingLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace LiftingLibrary.Controllers
{
    public class WorkoutsController : Controller
    {
        private readonly IWorkoutsDAO _workoutsDAO;
        private readonly GlobalUser _globalUser;

        public WorkoutsController(IWorkoutsDAO workoutDAO, GlobalUser globalUser) 
        {
            _workoutsDAO = workoutDAO;
            _globalUser = globalUser;
        }

        [HttpPost("CreateWorkout")]
        public IActionResult CreateWorkout([FromForm] CreatedWorkout createdWorkout)
        {
            try
            {
                int workoutId = _globalUser.Workouts.Count + 1;

                Workout tempWorkout = new Workout()
                {
                    WorkoutId = workoutId,
                    UserId = _globalUser.User.UserId,
                    Date = createdWorkout.Date,
                    Notes = createdWorkout.Notes
                };

                Workout returnWorkout = _workoutsDAO.CreateWorkout(tempWorkout);
                if (returnWorkout != null)
                {
                    _globalUser.Workouts.Add(returnWorkout);
                    TempData["WorkoutID"] = returnWorkout.WorkoutId;
                    return RedirectToAction("WorkoutDetailsCreator", "Library");
                }
                else
                {
                    TempData["Error"] = "An error occurred and the workout was not created.";
                    return RedirectToAction("WorkoutCreator", "Library");
                }
            }
            catch (Exception)
            {
                TempData["Error"] = "An error occurred and the workout was not created.";
                return RedirectToAction("WorkoutCreator", "Library");
            }
        }

        public IActionResult GetWorkoutDetails(int id)
        {
            var workoutDetails = _globalUser.WorkoutDetails
                .Where(wd => wd.WorkoutId == id)
                .ToList();

            if (workoutDetails == null || !workoutDetails.Any())
            {
                TempData["WorkoutID"] = id;
                PartialView("~/Views/Library/WorkoutDetails.cshtml", workoutDetails);
            }

            return PartialView("~/Views/Library/WorkoutDetails.cshtml", workoutDetails);
        }

        [HttpPost]
        public IActionResult DisplayWorkoutsByDate(DateTime searchDate)
        {
            TempData["SearchDate"] = searchDate;
            return RedirectToAction("WorkoutsWithSearchDate", "Library");
        }
    }
}
