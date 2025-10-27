using LinkUp.Core.Domain.Entities;
using LinkUp.Core.Domain.Interfaces;
using LinkUp.Infrastructure.Persistence.Contexts;

namespace LinkUp.Infrastructure.Persistence.Repositories
{
    public class BattleshipBoardRepository : GenericRepository<BattleshipBoard>, IBattleshipBoardRepository
    {
        private readonly LinkUpContext _context;
        public BattleshipBoardRepository(LinkUpContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<BattleshipBoard>?> AddRangeAsync(List<BattleshipBoard> boards) 
        { 
            await _context.Set<BattleshipBoard>().AddRangeAsync(boards);
            await _context.SaveChangesAsync();
            return boards;
        }
    }
}
