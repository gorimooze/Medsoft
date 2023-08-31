namespace Medsoft.Models
{
    public class Admission
    {
        public long Id { get; set; } 
        public DateTime DateAdmission { get; set; }
        public DateTime DateExtract { get; set; }
        public Patient Patient { get; set; }
        public PatientType PatientType { get; set; }
    }
}
