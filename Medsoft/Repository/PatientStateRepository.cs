using Medsoft.Data;
using Medsoft.Interfaces;
using Medsoft.Models;

namespace Medsoft.Repository
{
    public class PatientStateRepository : IPatientStateRepository
    {
        private readonly DataContext _context;

        public PatientStateRepository(DataContext context)
        {
            _context = context;
        }


        public ICollection<PatientState> GetAll()
        {
            return _context.PatientState.OrderBy(pt => pt.Id).ToList();
        }

        public PatientState GetPatientState(int id)
        {
            return _context.PatientState.Where(pt => pt.Id == id).SingleOrDefault();
        }

        public bool PatientStateExists(int id)
        {
            return _context.PatientState.Any(pt => pt.Id == id);
        }

        public bool CreatePatientState(PatientState patientState)
        {
            _context.Add(patientState);
            return Save();
        }

        public bool UpdatePatientState(PatientState patientState)
        {
            _context.Update(patientState);
            return Save();
        }

        public bool DeletePatientState(PatientState patientState)
        {
            _context.Remove(patientState);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}