using LinkUp.Core.Domain.Entities;
using LinkUp.Core.Domain.Interfaces;
using LinkUp.Infrastructure.Persistence.Contexts;

namespace LinkUp.Infrastructure.Persistence.Repositories
{
    public class PublicationRepository : GenericRepository<Publication>, IPublicationRepository
    {
        public PublicationRepository(LinkUpContext context) : base(context)
        {
        }
    }
}
