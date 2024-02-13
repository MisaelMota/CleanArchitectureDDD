using CleanArchitecture.Domain.Common;
using System.Linq.Expressions;

namespace CleanArchitecture.Application.Contracts.Persistence
{
    public interface IAsyncRepository<T> where T : BaseDomainModel
    {
        //Obtener todos los registros de una entidad
        Task<IReadOnlyList<T>> GetAllAsync();
        
        //Obtener datos bajo ciertas condicionales 
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T,bool>> predicate);

        //Obtener datos bajo ciertas condicionales con ordenamiento
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
               Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
            string includeString = null,
            bool disableTraking = true
            );

        //Obtener datos bajo ciertas condicionales con ordenamiento includendo otras entidades
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           List<Expression<Func<T,object>>> includes = null,
           bool disableTraking = true
           );

        //Obtener datos por el id
        Task<T> GetIdAsync(int id);

        //Agregar data a cualquier entidad
        Task<T> AddAsync(T entity);

        //Actualizar data de cualquier entidad
        Task<T> UpdateAsync(T entity);

        //Borrar data de cualquier entidad
        Task DeleteAsync(T entity);
    }
}
