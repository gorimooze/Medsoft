using Medsoft.Data;
using Medsoft.Interfaces;
using Medsoft.Models;
using System.Diagnostics.Metrics;

namespace Medsoft.Repository
{
    public class SexRepository : ISexRepository
    {
        private readonly DataContext _context;
        public SexRepository(DataContext context)
        {
            _context = context;
        }
        

        public ICollection<Sex> GetAll()
        {
            return _context.Sex.OrderBy(s => s.Id).ToList();
        }

        public Sex GetById(int id)
        {
            return _context.Sex.Where(s => s.Id == id).FirstOrDefault();
        }

        public bool SexExists(int id)
        {
            return _context.Sex.Any(s => s.Id == id);
        }

        public bool CreateSex(Sex sex)
        {
            _context.Add(sex);
            return Save();
        }

        public bool UpdateSex(Sex sex)
        {
            _context.Update(sex);
            return Save();
        }

        public bool DeleteSex(Sex sex)
        {
            _context.Remove(sex);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
