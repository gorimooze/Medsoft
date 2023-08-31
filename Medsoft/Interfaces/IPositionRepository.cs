using Medsoft.Models;

namespace Medsoft.Interfaces
{
    public interface IPositionRepository
    {
        ICollection<Position> GetAll();
        Position GetById(int id);
        bool PositionExists(int id);
        bool CreatePosition(Position position);
        bool UpdatePosition(Position position);
        bool DeletePosition(Position position);
        bool Save();
    }
}
