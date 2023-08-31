namespace Medsoft.Models
{
    public class PatientType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Admission> Admissions { get; set; }
    }
}
