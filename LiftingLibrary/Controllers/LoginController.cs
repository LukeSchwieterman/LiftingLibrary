using LiftingLibrary.DAO.User;
using LiftingLibrary.DAO.WorkoutDetails;
using LiftingLibrary.DAO.Workouts;
using LiftingLibrary.Models;
using LiftingLibrary.Security;
using Microsoft.AspNetCore.Mvc;

namespace LiftingLibrary.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserDAO _userDAO;
        private readonly GlobalUser _globalUser;
        private readonly IWorkoutsDAO _workoutsDAO;
        private readonly IWorkoutDetailsDAO _workoutDetailsDAO;

        public LoginController(IUserDAO userDAO, IWorkoutsDAO workoutsDAO, IWorkoutDetailsDAO workoutDetailsDAO, IPasswordHasher passwordHasher, GlobalUser globalUser)
        {
            _userDAO = userDAO;
            _passwordHasher = passwordHasher;
            _workoutsDAO = workoutsDAO;
            _globalUser = globalUser;
            _workoutDetailsDAO = workoutDetailsDAO;
        }

        [HttpPost]
        public IActionResult Authenticate([FromForm] LoginUser loginUser)
        {
            // Get the user by username
            User user = _userDAO.GetUser(loginUser.Email);

            // If we found a user and the password hash matches
            if (user != null && _passwordHasher.VerifyHashMatch(user.PasswordHash, loginUser.Password, user.Salt))
            {
                // Store user session
                UserContext userContext = new UserContext()
                {
                    Email = user.Email,
                    Name = user.Name,
                    CreatedAt = user.CreatedAt,
                    UserId = user.UserId
                };

                _globalUser.User = userContext;
                _globalUser.Workouts = _workoutsDAO.GetWorkoutsUserID(_globalUser.User.UserId);
                _globalUser.WorkoutDetails = _workoutDetailsDAO.GetWorkoutDetailsByUserID(_globalUser.User.UserId);
                _globalUser.LoggedIn = true;

                // Redirect to the Home page after login
                return RedirectToAction("Index", "Home");
            }

            // If login fails, show the login page with an error message
            ViewBag.Error = "Invalid email or password.";

            return RedirectToAction("Login", "Home");
        }
    }
}
