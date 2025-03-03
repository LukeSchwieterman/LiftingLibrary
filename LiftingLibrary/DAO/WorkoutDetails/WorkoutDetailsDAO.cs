using LiftingLibrary.Models;
using MySqlConnector;

namespace LiftingLibrary.DAO.WorkoutDetails
{
    public class WorkoutDetailsDAO : IWorkoutDetailsDAO
    {
        private readonly string _connectionString;
        public WorkoutDetailsDAO (string connectionString)
        {
            _connectionString = connectionString;
        }
        public WorkoutDetail CreateWorkoutDetail(WorkoutDetail workoutDetail)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    MySqlCommand command = new MySqlCommand("INSERT INTO workoutdetails(DetailID, UserID, WorkoutID, Name, Category, SetNumber, Reps, Weight) VALUES(@detailID, @userID, @workoutID, @name, @category, @setNumber, @reps, @weight)", connection);
                    command.Parameters.AddWithValue("@detailID", workoutDetail.DetailId);
                    command.Parameters.AddWithValue("@userID", workoutDetail.UserId);
                    command.Parameters.AddWithValue("@workoutID", workoutDetail.WorkoutId);
                    command.Parameters.AddWithValue("@name", workoutDetail.Name);
                    command.Parameters.AddWithValue("@category", workoutDetail.Category);
                    command.Parameters.AddWithValue("@setNumber", workoutDetail.Sets);
                    command.Parameters.AddWithValue("@reps", workoutDetail.Reps);
                    command.Parameters.AddWithValue("@weight", workoutDetail.Weight);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (MySqlException)
            {

                throw;
            }
            return GetWorkoutDetail(workoutDetail.DetailId, workoutDetail.WorkoutId, workoutDetail.UserId);
        }

        public void DeleteWorkoutDetail(int id)
        {
            throw new NotImplementedException();
        }

        public WorkoutDetail GetWorkoutDetail(int workoutDetailID, int workoutID, int userID)
        {
            WorkoutDetail workoutDetail = null;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    MySqlCommand command = new MySqlCommand("SELECT * FROM workoutdetails WHERE DetailID = @detailID AND UserID = @userID AND workoutID = @workoutID", connection);
                    command.Parameters.AddWithValue("@detailID", workoutDetailID);
                    command.Parameters.AddWithValue("@userID", userID);
                    command.Parameters.AddWithValue("@workoutID", workoutID);
                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        workoutDetail = GetWorkoutDetailFromReader(reader);
                    }
                }
            }
            catch (MySqlException)
            {

                throw;
            }
            return workoutDetail;
        }

        public List<WorkoutDetail> GetWorkoutDetailsByExercise(int id)
        {
            throw new NotImplementedException();
        }

        public List<WorkoutDetail> GetWorkoutDetailsByUserID(int userId)
        {
            List<WorkoutDetail> workoutDetails = new List<WorkoutDetail>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    MySqlCommand command = new MySqlCommand("SELECT * FROM workoutdetails WHERE UserID = @userID", connection);
                    command.Parameters.AddWithValue("@userID", userId);
                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            workoutDetails.Add(GetWorkoutDetailFromReader(reader));
                        }

                    }
                    connection.Close();
                }
            }
            catch (MySqlException)
            {

                throw;
            }
            return workoutDetails;
        }

        public List<WorkoutDetail> GetWorkoutDetailsByWorkout(int id)
        {
            throw new NotImplementedException();
        }

        public WorkoutDetail UpdateWorkoutDetail(WorkoutDetail workoutDetail)
        {
            throw new NotImplementedException();
        }

        private WorkoutDetail GetWorkoutDetailFromReader(MySqlDataReader reader)
        {
            WorkoutDetail workout = new WorkoutDetail()
            {
                DetailId = reader.GetInt32("DetailID"),
                UserId = Convert.ToInt32(reader["UserID"]),
                WorkoutId = Convert.ToInt32(reader["WorkoutID"]),
                Name = Convert.ToString(reader["Name"]),
                Category = Convert.ToString(reader["Category"]),
                Sets = reader.GetInt32("SetNumber"),
                Reps = Convert.ToInt32(reader["Reps"]),
                Weight = Convert.ToInt32(reader["Weight"])
            };

            return workout;
        }
    }
}
