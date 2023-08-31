namespace Medsoft.Models
{
    public class PatientState
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Patient> Pacients { get; set; }
    }
}
