using Medsoft.Models;

namespace Medsoft.Interfaces
{
    public interface IWorkScheduleRepository
    {
        ICollection<WorkSchedule> GetAll();
        WorkSchedule GetById(int id);
        bool WorkScheduleExists(int id);
        bool CreateWorkSchedule(WorkSchedule workSchedule);
        bool UpdateWorkSchedule(WorkSchedule workSchedule);
        bool DeleteWorkSchedule(WorkSchedule workSchedule);
        bool Save();
    }
}
