namespace Medsoft.Models
{
    public class Patient
    {
        public long Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public decimal IDNP { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public decimal Telephone { get; set; }
        public Sex Sex { get; set; }
        public PatientState PatientState { get; set; }
        public ICollection<Admission> Admissions { get; set;}
    }
}
