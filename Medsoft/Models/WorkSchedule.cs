namespace Medsoft.Models
{
    public class WorkSchedule
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public DateTime Date { get; set; }
        public Employee Employee { get; set; }
    }
}
