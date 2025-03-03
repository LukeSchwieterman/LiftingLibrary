using LiftingLibrary.DAO.User;
using LiftingLibrary.Models;
using LiftingLibrary.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace LiftingLibrary.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegisterController : Controller
    {
        private readonly IUserDAO _userDAO;
        private readonly GlobalUser _globalUser;

        public RegisterController(IUserDAO userDAO, GlobalUser globalUser)
        {
            _userDAO = userDAO;
            _globalUser = globalUser;
        }

        public IActionResult Register([FromForm] RegisterUser registerUser)
        {
            User existingUser = _userDAO.GetUser(registerUser.Email);
            if (existingUser != null)
            {
                ViewBag.Error = "Email already linked with an account. Please choose a different email or try and sign in.";
                return RedirectToAction("Register", "Home");
            }

            if (registerUser.Password != registerUser.ConfirmPassword)
            {
                return RedirectToAction("Register", "Home");
            }

            User user = _userDAO.AddUser(registerUser);
            if (user != null)
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
                _globalUser.LoggedIn = true;

                // Redirect to the Home page after successful register
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "An error occurred and the user was not created.";
                return RedirectToAction("Register", "Home");
            }
        }
    }
}
