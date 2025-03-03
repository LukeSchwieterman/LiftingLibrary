namespace LiftingLibrary.Models
{
    public class GlobalUser
    {
        public UserContext User { get; set; }
        public List<Progress> Progresses { get; set; }
        public List<Workout> Workouts { get; set; }
        public List<WorkoutDetail> WorkoutDetails { get; set; }
        public bool LoggedIn { get; set; }

        public void Logout()
        {
            User = new UserContext();
            Progresses = new List<Progress>();
            Workouts = new List<Workout>();
            WorkoutDetails = new List<WorkoutDetail>();
            LoggedIn = false;
        }
    }
}
