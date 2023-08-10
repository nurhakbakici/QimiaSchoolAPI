using Microsoft.EntityFrameworkCore;
using QimiaSchool.DataAccess.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;



namespace QimiaSchool.DataAccess.Repositories.Abstractions;

public abstract class RepositoryBase<T> where T : class
{
    protected QimiaSchoolDbContext DbContext { get; set; }

    private readonly DbSet<T> DbSet;

    protected RepositoryBase(QimiaSchoolDbContext dbContext)
    {
        DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        DbSet = dbContext.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<T>().AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> expression,
        CancellationToken cancellationToken = default)
    {
        return await DbSet.Where(expression).ToListAsync(cancellationToken);
    }

    public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await DbSet.FindAsync(
            id,
            cancellationToken) ??
            throw new EntityNotFoundException<T>(id);

        /*
        we will give 2 parameters to this  method. First one is the id of the entity we want. 
        second one is the cancellation token it will allow us to cancel the operation if its needed.
        FindAsync() method will look at the data from database and by using await we make it to wait asycnhronusly for operation to finish.
        ?? operator checks if the outcome is null or not, and if its null it will throw the exception that we create.
         */

    }

    public async Task DeleteByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);

        DbContext.Remove(entity);
        await DbContext.SaveChangesAsync(cancellationToken);

        // after we get the entity with getbyId method, we will remove the entity with DbContext.Remove. then we will save the changes.
    }

    public async Task CreateAsync( 
       T entity,
       CancellationToken cancellationToken)
    {
        await DbSet.AddAsync(
            entity,
            cancellationToken);

        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(
    T entity,
    CancellationToken cancellationToken)
    {
        DbSet.Update(entity);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(
        T entity,
        CancellationToken cancellationToken)
    {
        DbSet.Remove(entity);
        await DbContext.SaveChangesAsync(cancellationToken);

        //DbSet.Remove will delete the given entity when savechanges applied.
    }
}



