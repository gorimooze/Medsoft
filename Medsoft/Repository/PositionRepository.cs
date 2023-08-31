using Medsoft.Data;
using Medsoft.Interfaces;
using Medsoft.Models;

namespace Medsoft.Repository
{
    public class PositionRepository : IPositionRepository
    {
        private readonly DataContext _context;

        public PositionRepository(DataContext context)
        {
            _context = context;
        }


        public ICollection<Position> GetAll()
        {
            return _context.Position.OrderBy(p => p.Id).ToList();
        }

        public Position GetById(int id)
        {
            return _context.Position.Where(p => p.Id == id).SingleOrDefault();
        }

        public bool PositionExists(int id)
        {
            return _context.Position.Any(p => p.Id == id);
        }

        public bool CreatePosition(Position position)
        {
            _context.Add(position);
            return Save();
        }

        public bool UpdatePosition(Position position)
        {
            _context.Update(position);
            return Save();
        }

        public bool DeletePosition(Position position)
        {
            _context.Remove(position);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
