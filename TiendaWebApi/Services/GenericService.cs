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

    public void Add(T entity)
    {
        throw new NotImplementedException();
    }

    public void AddRange(IEnumerable<T> entities)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public Task<T> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public void Remove(T entity)
    {
        throw new NotImplementedException();
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        throw new NotImplementedException();
    }

    public void Update(T entity)
    {
        throw new NotImplementedException();
    }
}
