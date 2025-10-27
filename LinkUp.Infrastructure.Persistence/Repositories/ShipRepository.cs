using LinkUp.Core.Domain.Entities;
using LinkUp.Core.Domain.Interfaces;
using LinkUp.Infrastructure.Persistence.Contexts;

namespace LinkUp.Infrastructure.Persistence.Repositories
{
    public class ShipRepository : GenericRepository<Ship>, IShipRepository
    {
        public ShipRepository(LinkUpContext context) : base(context)
        {
        }
    }
}
