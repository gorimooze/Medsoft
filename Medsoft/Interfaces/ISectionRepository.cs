using Medsoft.Models;

namespace Medsoft.Interfaces
{
    public interface ISectionRepository
    {
        ICollection<Section> GetAll();
        Section GetById(int id);
        bool SectionExists(int id);
        bool CreateSection(Section section);
        bool UpdateSection(Section section);
        bool DeleteSection(Section section);
        bool Save();
    }
}
