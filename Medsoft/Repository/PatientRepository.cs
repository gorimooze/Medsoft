using Medsoft.Data;
using Medsoft.Interfaces;
using Medsoft.Models;

namespace Medsoft.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly DataContext _context;

        public PatientRepository(DataContext context)
        {
            _context = context;
        }


        public ICollection<Patient> GetAll()
        {
            return _context.Patient.OrderBy(p => p.Id).ToList();
        }

        public Patient GetPatientById(long id)
        {
            return _context.Patient.Where(p => p.Id == id).FirstOrDefault();
        }

        public Patient GetPatientByIDNP(decimal idnp)
        {
            return _context.Patient.Where(p => p.IDNP == idnp).FirstOrDefault();
        }

        public bool PatientExists(long id)
        {
            return _context.Patient.Any(p => p.Id == id);
        }

        public bool CreatePatient(Patient patient)
        {
            _context.Add(patient);
            return Save();
        }

        public bool UpdatePatient(Patient patient)
        {
            _context.Update(patient);
            return Save();
        }

        public bool DeletePatient(Patient patient)
        {
            _context.Remove(patient);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
