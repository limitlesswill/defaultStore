using Application.Contracts;
using Application.Context;
using Microsoft.EntityFrameworkCore;
using Application.Models;

namespace Infrastructure
{
    public class Repository<TEntity, TID> : IRepository<TEntity, TID> where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<TEntity> dbset;
        public Repository(ApplicationDbContext _context)
        {
            this.context = _context;
            this.dbset = context.Set<TEntity>();
        }
        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            return (await dbset.AddAsync(entity)).Entity;
        }

        public Task<TEntity> DeleteAsync(TEntity entity)
        {
            return Task.FromResult(dbset.Remove(entity).Entity);
        }

        public Task<IQueryable<TEntity>> GetAllAsync()
        {
            return Task.FromResult(dbset.Select(x => x));
        }

        public async Task<TEntity> GetByIdAsync(TID id)
        {
            return await dbset.FindAsync(id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            return await Task.FromResult(dbset.Update(entity).Entity);
        }
    }
}
