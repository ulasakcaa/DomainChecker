using Shared.Common;

namespace Data.Common
{
    public interface IRepository<T> where T : BaseContextEntity, new()
    {
        IQueryable<T> GetList();    
        IQueryable<T> GetListNoTracking();    
        Task<T> InsertT(T entity);
        Task Insert(T entity);
        Task Insert(List<T> entity);
        Task Update(T entity);    
        Task Update(List<T> entity);
        Task Delete(T entity);
        Task DeleteByIdAsync(object id);
    }
}
