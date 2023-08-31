using Medsoft.Data;
using Medsoft.Interfaces;
using Medsoft.Models;

namespace Medsoft.Repository
{
    public class WorkScheduleRepository : IWorkScheduleRepository
    {
        private readonly DataContext _context;

        public WorkScheduleRepository(DataContext context)
        {
            _context = context;
        }


        public ICollection<WorkSchedule> GetAll()
        {
            return _context.WorkSchedule.OrderBy(ws => ws.Id).ToList();
        }

        public WorkSchedule GetById(int id)
        {
            return _context.WorkSchedule.Where(ws => ws.Id == id).SingleOrDefault();
        }

        public bool WorkScheduleExists(int id)
        {
            return _context.WorkSchedule.Any(ws => ws.Id == id);
        }

        public bool CreateWorkSchedule(WorkSchedule workSchedule)
        {
            _context.Add(workSchedule);
            return Save();
        }

        public bool UpdateWorkSchedule(WorkSchedule workSchedule)
        {
            _context.Update(workSchedule);
            return Save();
        }

        public bool DeleteWorkSchedule(WorkSchedule workSchedule)
        {
            _context.Remove(workSchedule);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
