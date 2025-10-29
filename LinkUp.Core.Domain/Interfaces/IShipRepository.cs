using LinkUp.Core.Domain.Entities;

namespace LinkUp.Core.Domain.Interfaces
{
    public interface IShipRepository : IGenericRepository<Ship>
    {
        Task<List<Ship>?> AddRangeAsync(List<Ship> ships);
    }
}
