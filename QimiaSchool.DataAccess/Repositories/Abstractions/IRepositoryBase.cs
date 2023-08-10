using System.Linq.Expressions;

namespace QimiaSchool.DataAccess.Repositories.Abstractions;

public interface IRepositoryBase<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
    Task DeleteByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task CreateAsync(T entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity, CancellationToken cancellationToken);
    Task DeleteAsync(T entity, CancellationToken cancellationToken);

    /* we didn't use cancellationToken= defaut on delete and update methods because it is more
     likely for us to cancel these operations since they tend to take longer to operate.*/

    // we used async to increase efficiency of our program. when we used async it makes each command to work independently. 
    // we used task class because otherwise we wouldnt be able to perform our operations asynchronusly.
    // by using cancellations tokens we gave ourselves the option to cancel an operation if we see it necessary.
}
