namespace LiftingLibrary.Models
{
    public class WorkoutDetail
    {
        public int DetailId { get; set; }
        public int WorkoutId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public int Weight { get; set; }
    }

    public class CreatedWorkoutDetail
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public int SetNumber { get; set; }
        public int RepNumber { get; set; }
        public int Weight { get; set; }
    }
}
