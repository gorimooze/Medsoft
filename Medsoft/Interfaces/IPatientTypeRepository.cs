using Medsoft.Models;

namespace Medsoft.Interfaces
{
    public interface IPatientTypeRepository
    {
        ICollection<PatientType> GetAll();
        PatientType GetById(int id);
        bool PatientTypeExists(int id);
        bool CreatePatientType(PatientType patientType);
        bool UpdatePatientType(PatientType patientType);
        bool DeletePatientType(PatientType patientType);
        bool Save();
    }
}
