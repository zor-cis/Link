using LinkUp.Core.Domain.Entities;
using LinkUp.Core.Domain.Interfaces;
using LinkUp.Infrastructure.Persistence.Contexts;

namespace LinkUp.Infrastructure.Persistence.Repositories
{
    public class CellRepository : GenericRepository<Cell>, ICellRepository
    {
        public CellRepository(LinkUpContext context) : base(context)
        {
        }
    }
}
