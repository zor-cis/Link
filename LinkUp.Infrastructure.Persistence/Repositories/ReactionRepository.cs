using LinkUp.Core.Domain.Entities;
using LinkUp.Core.Domain.Interfaces;
using LinkUp.Infrastructure.Persistence.Contexts;

namespace LinkUp.Infrastructure.Persistence.Repositories
{
    public class ReactionRepository : GenericRepository<Reaction>, IReactionRepository
    {
        public ReactionRepository(LinkUpContext context) : base(context)
        {
        }
    }
}
