using LinkUp.Core.Domain.Entities;
using LinkUp.Core.Domain.Interfaces;
using LinkUp.Infrastructure.Persistence.Contexts;

namespace LinkUp.Infrastructure.Persistence.Repositories
{
    public class BattleshipGameRepository : GenericRepository<BattleshipGame>, IBattleshipGameRepository
    {
        public BattleshipGameRepository(LinkUpContext context) : base(context)
        {
        }
    }
}
