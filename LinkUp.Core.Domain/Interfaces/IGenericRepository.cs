namespace LinkUp.Core.Domain.Interfaces
{
    public interface IGenericRepository<Entity> where Entity : class
    {
        Task<Entity?> AddAsync(Entity entity);
        Task DeleteAsync(int Id);
        Task<List<Entity>?> GetAllList();
        Task<List<Entity>?> GetAllListIncluide(List<string> properties);
        Task<Entity?> GetById(int Id);
        IQueryable<Entity> GetQuery();
        IQueryable<Entity> GetQueryWithIncluide(List<string> properties);
        Task<Entity?> UpdateAsync(int Id, Entity entity);
    }
}