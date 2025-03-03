using LiftingLibrary.DAO.WorkoutDetails;
using LiftingLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace LiftingLibrary.Controllers
{
    public class WorkoutDetailsController : Controller
    {
        private readonly GlobalUser _globalUser;
        private readonly IWorkoutDetailsDAO _workoutDetailsDAO;

        public WorkoutDetailsController(GlobalUser globalUser, IWorkoutDetailsDAO workoutDetails) 
        {
            _globalUser = globalUser;
            _workoutDetailsDAO = workoutDetails;
        }

        [HttpPost("CreateWorkoutDetail")]
        public IActionResult CreateWorkoutDetail([FromForm] CreatedWorkoutDetail createdWorkoutDetail)
        {
            try
            {
                int workoutId = Convert.ToInt32(TempData["WorkoutID"]);
                int workoutDetailId = _globalUser.WorkoutDetails.Count + 1;
                WorkoutDetail tempWorkoutDetail = new WorkoutDetail()
                {
                    DetailId = workoutDetailId,
                    WorkoutId = workoutId,
                    UserId = _globalUser.User.UserId,
                    Name = createdWorkoutDetail.Name,
                    Category = createdWorkoutDetail.Category,
                    Sets = createdWorkoutDetail.SetNumber,
                    Reps = createdWorkoutDetail.RepNumber,
                    Weight = createdWorkoutDetail.Weight
                };

                WorkoutDetail returnWorkout = _workoutDetailsDAO.CreateWorkoutDetail(tempWorkoutDetail);
                if (returnWorkout != null)
                {
                    _globalUser.WorkoutDetails.Add(returnWorkout);
                    TempData["SuccessMessage"] = "Exercise successfully added! Feel free to add another exercise or return to the Workouts screen.";
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
    }
}
