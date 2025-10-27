using LinkUp.Core.Domain.Entities;
using LinkUp.Core.Domain.Interfaces;
using LinkUp.Infrastructure.Persistence.Contexts;

namespace LinkUp.Infrastructure.Persistence.Repositories
{
    public class AttackRepository : GenericRepository<Attack>, IAttackRepository
    {
        public AttackRepository(LinkUpContext context) : base(context)
        {
        }
    }
}
