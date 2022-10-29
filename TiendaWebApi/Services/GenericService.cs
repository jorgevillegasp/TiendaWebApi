using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TiendaWebApi.Interfaces;
using TiendaWebApi.Models;



namespace Infrastructure.Repositories;

public class GenericService<T> : GenericInterface<T> where T : BaseEntity
{
    protected readonly TiendaWebApiContext _context;

    public GenericService(TiendaWebApiContext context)
    {
        _context = context;
    }

    public virtual void Add(T entity)
    {
        throw new NotImplementedException();
    }

    public virtual void AddRange(IEnumerable<T> entities)
    {
        throw new NotImplementedException();
    }

    public virtual IEnumerable<T> Find(Expression<Func<T, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public virtual Task<T> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public virtual void Remove(T entity)
    {
        throw new NotImplementedException();
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        throw new NotImplementedException();
    }

    public virtual void Update(T entity)
    {
        throw new NotImplementedException();
    }
}
