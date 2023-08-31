namespace Medsoft.Models
{
    public class Section
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public int Floor { get; set; }
        public int DoctorCount { get; set; }
        public int NurseCount { get; set; }
        public int PlaceCount { get; set; }
        public decimal Telephone { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
