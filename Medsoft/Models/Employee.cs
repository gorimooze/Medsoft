namespace Medsoft.Models
{
    public class Employee
    {
        public long Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public decimal IDNP { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public decimal Telephone { get; set; }
        public DateTime StartWork { get; set; }
        public string Education { get; set; }
        public Sex Sex { get; set; }
        public Position Position { get; set; }
        public Section Section { get; set; }
        public ICollection<WorkSchedule> WorkSchedules { get; set; }
    }
}
