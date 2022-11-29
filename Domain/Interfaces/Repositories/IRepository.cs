using Domain.Entities;
using Domain.Interfaces.Entities;
using Domain.Interfaces.Specifications;
using System.Linq.Expressions;

namespace Domain.Interfaces.Repositories
{
    public interface IRepository<T> where T : BaseEntity, IAggregateRoot
    {
        T? GetById(Guid id);
        Task<T?> GetByIdAsync(Guid id);

        IEnumerable<T> List();
        Task<IEnumerable<T>> ListAsync();
        
        Task<IEnumerable<T>> ListAsync(ISpecification<T> spec);
        IEnumerable<T> List(ISpecification<T> spec);

        T Add(T entity);
        Task<T> AddAsync(T entity);

        T Delete(T entity);
        Task<T> DeleteAsync(T entity);

        T Update(T entity);
        Task<T> UpdateAsync(T entity);
    }
}
