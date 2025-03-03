using LiftingLibrary.Models;

namespace LiftingLibrary.DAO.Workouts
{
    public interface IWorkoutsDAO
    {
        Workout CreateWorkout(Workout workout);
        Workout GetWorkoutWorkoutID(int workoutId, int userId);
        List<Workout> GetWorkoutsUserID(int userId);
        List<Workout> GetWorkoutsByDate(DateTime date, int userId);
        Workout UpdateWorkout(Workout workout);
        void DeleteWorkout(int workoutId, int userId);
    }
}
