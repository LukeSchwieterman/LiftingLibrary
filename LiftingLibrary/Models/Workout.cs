namespace LiftingLibrary.Models
{
    public class Workout
    {
        public int WorkoutId { get; set; }
        public int UserId { get; set; }
        public DateTime Date {  get; set; }
        public string? Notes { get; set; }
    }

    public class CreatedWorkout
    {
        public DateTime Date { get; set; }
        public string? Notes { get; set;}
    }
}
