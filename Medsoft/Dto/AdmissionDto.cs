using Medsoft.Models;

namespace Medsoft.Dto
{
    public class AdmissionDto
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public long Patient { get; set; }
        public int PatientType { get; set; }
    }
}
