using Medsoft.Data;
using Medsoft.Interfaces;
using Medsoft.Models;
using Microsoft.EntityFrameworkCore;

namespace Medsoft.Repository
{
    public class PatientTypeRepository : IPatientTypeRepository
    {
        private readonly DataContext _context;

        public PatientTypeRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<PatientType> GetAll()
        {
            return _context.PatientType.OrderBy(pt => pt.Id).ToList();
        }

        public PatientType GetById(int id)
        {
            return _context.PatientType.Where(pt => pt.Id == id).FirstOrDefault();
        }

        public bool PatientTypeExists(int id)
        {
            return _context.PatientType.Any(pt => pt.Id == id);
        }

        public bool CreatePatientType(PatientType patientType)
        {
            _context.Add(patientType);
            return Save();
        }

        public bool UpdatePatientType(PatientType patientType)
        {
            _context.Update(patientType);
            return Save();
        }

        public bool DeletePatientType(PatientType patientType)
        {
            _context.Remove(patientType);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
