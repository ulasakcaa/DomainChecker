using Microsoft.EntityFrameworkCore;
using Shared.Common;
using Data.Context;

namespace Data.Common
{
    public class Repository<T> : IRepository<T> where T : BaseContextEntity, new()
    {
        protected readonly IDomainCheckDbContext Context;     
        public Repository(IDomainCheckDbContext context)
        {
            Context = context;          
        }
        protected DbSet<T> Table { get => Context.Set<T>(); }     
        public IQueryable<T> GetList() => Table.AsQueryable();   
        public IQueryable<T> GetListNoTracking() => Table.AsNoTracking();
    
        public async Task<T> InsertT(T entity)
        {
            await Table.AddAsync(entity);
            await Context.SaveChangesAsync();
            return entity;
        }
        public async Task Insert(T entity)
        {
            await Table.AddAsync(entity);
            await Context.SaveChangesAsync();
        }
        public async Task Insert(List<T> entity)
        {
            await Table.AddRangeAsync(entity);
            await Context.SaveChangesAsync();
        }
        public async Task Update(T entity)
        {
            Table.Update(entity);
            await Context.SaveChangesAsync();
        }
        public async Task Update(List<T> entity)
        {
            Table.UpdateRange(entity);
            await Context.SaveChangesAsync();
        }
        public async Task Delete(T entity)
        {           
            Context.Set<T>().Remove(entity);           
            await Context.SaveChangesAsync();
        }
        public async Task DeleteByIdAsync(object id)
        {
            
            var entity = await Context.Set<T>().FindAsync(id);
        
            if (entity == null)
                throw new KeyNotFoundException($"Entity with id {id} not found.");
            
            Context.Set<T>().Remove(entity);
            await Context.SaveChangesAsync();
        }

    }
}
