namespace Application.Contracts
{
    public interface IRepository<TEntity, TID>
    {
        public Task<TEntity> CreateAsync(TEntity entity);
        public Task<TEntity> UpdateAsync(TEntity entity);
        public Task<TEntity> DeleteAsync(TEntity entity);
        public Task<TEntity> GetByIdAsync(TID id);
        public Task<IQueryable<TEntity>> GetAllAsync();
        public Task<int> SaveChangesAsync();
    }
}
