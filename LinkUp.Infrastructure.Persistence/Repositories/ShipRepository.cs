using LinkUp.Core.Domain.Entities;
using LinkUp.Core.Domain.Interfaces;
using LinkUp.Infrastructure.Persistence.Contexts;

namespace LinkUp.Infrastructure.Persistence.Repositories
{
    public class ShipRepository : GenericRepository<Ship>, IShipRepository
    {
        private readonly LinkUpContext _context;

        public ShipRepository(LinkUpContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Ship>?> AddRangeAsync(List<Ship> ships)
        {
            await _context.Set<Ship>().AddRangeAsync(ships);
            await _context.SaveChangesAsync();
            return ships;
        }
    }
}
