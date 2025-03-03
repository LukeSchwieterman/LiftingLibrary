using LiftingLibrary.Models;
using MySqlConnector;

namespace LiftingLibrary.DAO.Workouts
{
    public class WorkoutsDAO : IWorkoutsDAO
    {
        private readonly string _connectionString;
        public WorkoutsDAO(string connectionString) 
        {
            _connectionString = connectionString;
        }

        public Workout CreateWorkout(Workout workout)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("INSERT INTO workouts (WorkoutID, UserID, Date, Notes) VALUES (@workoutId, @userId, @date, @notes)", connection);
                    command.Parameters.AddWithValue("@workoutId", workout.WorkoutId);
                    command.Parameters.AddWithValue("@userId", workout.UserId);
                    command.Parameters.AddWithValue("@date", workout.Date);
                    command.Parameters.AddWithValue("@notes", workout.Notes);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (MySqlException)
            {

                throw;
            }
            return GetWorkoutWorkoutID(workout.WorkoutId, workout.UserId);
        }

        public void DeleteWorkout(int workoutId, int userId)
        {
            throw new NotImplementedException();
        }

        public List<Workout> GetWorkoutsByDate(DateTime date, int userId)
        {
            List<Workout> workouts = new List<Workout>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("SELECT * FROM workouts WHERE UserID = @userId AND Date = @date", connection);
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@date", date);
                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            workouts.Add(GetWorkoutFromReader(reader));
                        }
                    }
                    connection.Close();
                }
            }
            catch (MySqlException)
            {

                throw;
            }
            return workouts;
        }

        public List<Workout> GetWorkoutsUserID(int userId)
        {
            List<Workout> workouts = new List<Workout>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("SELECT * FROM workouts WHERE UserID = @userId", connection);
                    command.Parameters.AddWithValue("@userId", userId);
                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            workouts.Add(GetWorkoutFromReader(reader));
                        }

                    }
                    connection.Close();
                }
            }
            catch (MySqlException)
            {

                throw;
            }
            return workouts;
        }

        public Workout GetWorkoutWorkoutID(int workoutId, int userId)
        {
            Workout workout = null;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("SELECT * FROM workouts WHERE UserID = @userId AND WorkoutID = @workoutId", connection);
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@workoutId", workoutId);
                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        workout = GetWorkoutFromReader(reader);
                    }
                }
            }
            catch (MySqlException)
            {

                throw;
            }
            return workout;
        }

        public Workout UpdateWorkout(Workout workout)
        {
            throw new NotImplementedException();
        }

        private Workout GetWorkoutFromReader(MySqlDataReader reader)
        {
            Workout workout = new Workout()
            {
                WorkoutId = Convert.ToInt32(reader["WorkoutID"]),
                UserId = Convert.ToInt32(reader["UserID"]),               
                Date = Convert.ToDateTime(reader["Date"]),
                Notes = Convert.ToString(reader["Notes"])
            };

            return workout;
        }
    }
}
