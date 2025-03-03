namespace LiftingLibrary.Models
{
    public class Progress
    {
        public int ProgressId { get; set; }
        public int UserID { get; set; }
        public int ExerciseId { get; set; }
        public DateTime Date { get; set; }
        public int OneRepMax { get; set; }
    }
}
