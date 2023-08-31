using Medsoft.Models;

namespace Medsoft.Interfaces
{
    public interface IPatientRepository
    {
        ICollection<Patient> GetAll();
        Patient GetPatientById(long id);
        Patient GetPatientByIDNP(decimal idnp);
        bool PatientExists(long id);
        bool CreatePatient(Patient patient);
        bool UpdatePatient(Patient patient);
        bool DeletePatient(Patient patient);
        bool Save();
    }
}
