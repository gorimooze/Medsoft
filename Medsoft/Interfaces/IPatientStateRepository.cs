using Medsoft.Models;

namespace Medsoft.Interfaces
{
    public interface IPatientStateRepository
    {
        ICollection<PatientState> GetAll();
        PatientState GetById(int id);
        bool PatientStateExists(int id);
        bool CreatePatientState(PatientState patientState);
        bool UpdatePatientState(PatientState patientState);
        bool DeletePatientState(PatientState patientState);
        bool Save();
    }
}
