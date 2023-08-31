namespace Medsoft.Models
{
    public class Sex
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Patient> Pacients { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
