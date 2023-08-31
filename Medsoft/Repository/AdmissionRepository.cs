using Medsoft.Data;
using Medsoft.Interfaces;
using Medsoft.Models;

namespace Medsoft.Repository
{
    public class AdmissionRepository : IAdmissionRepository
    {
        private readonly DataContext _context;

        public AdmissionRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Admission> GetAll()
        {
            return _context.Admissions.OrderBy(a => a.Id).ToList();
        }

        public Admission GetById(int id)
        {
            return _context.Admissions.Where(a => a.Id == id).FirstOrDefault();
        }

        public ICollection<Admission> GetAdmissionByPatient(long patientId)
        {
            return _context.Admissions.Where(a => a.Patient.Id == patientId).ToList();
        }

        public bool AdmissionExists(int id)
        {
            return _context.Admissions.Any(a => a.Id == id);
        }

        public bool CreateAdmission(Admission admission)
        {
            _context.Add(admission);
            return Save();
        }

        public bool UpdateAdmission(Admission admission)
        {
            _context.Update(admission);
            return Save();
        }

        public bool DeleteAdmission(Admission admission)
        {
            _context.Remove(admission);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
