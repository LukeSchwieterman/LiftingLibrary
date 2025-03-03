using LiftingLibrary.Models;
using LiftingLibrary.Security;
using LiftingLibrary.Security.Models;
using MySqlConnector;

namespace LiftingLibrary.DAO.User
{
    public class UserDAO : IUserDAO
    {
        private readonly string connectionString;

        public UserDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public Models.User AddUser(RegisterUser registerUser)
        {
            IPasswordHasher passwordHasher = new PasswordHasher();
            PasswordHash hash = passwordHasher.ComputeHash(registerUser.Password);

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand("INSERT INTO users (Email, Name, Passwordhash, Salt, CreatedAt) VALUES (@email, @name, @passwordHash, @salt, @createdAt)", conn);
                    cmd.Parameters.AddWithValue("@email", registerUser.Email);
                    cmd.Parameters.AddWithValue("@name", registerUser.Name);
                    cmd.Parameters.AddWithValue("@passwordhash", hash.Password);
                    cmd.Parameters.AddWithValue("@salt", hash.Salt);
                    cmd.Parameters.AddWithValue("@createdAt", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException)
            {
                throw;
            }

            return GetUser(registerUser.Email);
        }

        public Models.User GetUser(string email)
        {
            Models.User returnUser = null;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM users WHERE email = @email", conn);
                    cmd.Parameters.AddWithValue("@email", email);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        returnUser = GetUserFromReader(reader);
                    }
                }
            }
            catch (MySqlException)
            {
                throw;
            }

            return returnUser;
        }

        private Models.User GetUserFromReader(MySqlDataReader reader)
        {
            reader.Read();
            Models.User user = new Models.User()
            {
                UserId = Convert.ToInt32(reader["UserID"]),
                Name = Convert.ToString(reader["Name"]),
                Email = Convert.ToString(reader["Email"]),
                PasswordHash = Convert.ToString(reader["PasswordHash"]),
                Salt = Convert.ToString(reader["salt"]),
                CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
            };

            return user;
        }
    }
}
