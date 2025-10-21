using LinkUp.Core.Domain.Entities;
using LinkUp.Core.Domain.Interfaces;
using LinkUp.Infrastructure.Persistence.Contexts;

namespace LinkUp.Infrastructure.Persistence.Repositories
{
    public class PostCommenRepository : GenericRepository<PostCommen>, IPostCommenRepository
    {
        public PostCommenRepository(LinkUpContext context) : base(context)
        {
        }
    }
}
