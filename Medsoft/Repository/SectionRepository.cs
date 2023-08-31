using Medsoft.Data;
using Medsoft.Interfaces;
using Medsoft.Models;

namespace Medsoft.Repository
{
    public class SectionRepository : ISectionRepository
    {
        private readonly DataContext _context;

        public SectionRepository(DataContext context)
        {
            _context = context;
        }


        public ICollection<Section> GetAll()
        {
            return _context.Section.OrderBy(s => s.Id).ToList();
        }

        public Section GetById(int id)
        {
            return _context.Section.Where(s => s.Id == id).SingleOrDefault();
        }

        public bool SectionExists(int id)
        {
            return _context.Section.Any(s => s.Id == id);
        }

        public bool CreateSection(Section section)
        {
            _context.Add(section);
            return Save();
        }

        public bool UpdateSection(Section section)
        {
            _context.Update(section);
            return Save();
        }

        public bool DeleteSection(Section section)
        {
            _context.Remove(section);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
