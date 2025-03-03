using LiftingLibrary.Models;

namespace LiftingLibrary.DAO.WorkoutDetails
{
    public interface IWorkoutDetailsDAO
    {
        WorkoutDetail GetWorkoutDetail(int workoutDetailId, int workoutId, int userId);
        List<WorkoutDetail> GetWorkoutDetailsByWorkout(int id);
        List<WorkoutDetail> GetWorkoutDetailsByUserID(int userId);
        WorkoutDetail CreateWorkoutDetail(WorkoutDetail workoutDetail);
        void DeleteWorkoutDetail(int id);
        WorkoutDetail UpdateWorkoutDetail(WorkoutDetail workoutDetail);
    }
}
