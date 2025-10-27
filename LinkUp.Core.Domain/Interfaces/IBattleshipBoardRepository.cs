using LinkUp.Core.Domain.Entities;

namespace LinkUp.Core.Domain.Interfaces
{
    public interface IBattleshipBoardRepository : IGenericRepository<BattleshipBoard>
    {
        Task<List<BattleshipBoard>?> AddRangeAsync(List<BattleshipBoard> boards);
    }
}
