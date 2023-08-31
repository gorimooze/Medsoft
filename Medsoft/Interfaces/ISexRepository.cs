using Medsoft.Models;
using System.Diagnostics.Metrics;

namespace Medsoft.Interfaces
{
    public interface ISexRepository
    {
        ICollection<Sex> GetAll();
        Sex GetById(int id);
        bool SexExists(int id);
        bool CreateSex(Sex sex);
        bool UpdateSex(Sex sex);
        bool DeleteSex(Sex sex);
        bool Save();
    }
}
