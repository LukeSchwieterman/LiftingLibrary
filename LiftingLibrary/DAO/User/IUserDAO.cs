
using LiftingLibrary.Models;

namespace LiftingLibrary.DAO.User
{
    public interface IUserDAO
    {
        Models.User GetUser(string email);
        Models.User AddUser(RegisterUser registerUser);
    }
}
