using Medsoft.Models;

namespace Medsoft.Interfaces
{
    public interface IAdmissionRepository
    {
        ICollection<Admission> GetAll();
        Admission GetById(int id);
        ICollection<Admission> GetAdmissionByPatient(long patientId);
        bool AdmissionExists(int id);
        bool CreateAdmission(Admission admission);
        bool UpdateAdmission(Admission admission);
        bool DeleteAdmission(Admission admission);
        bool Save();
    }
}
