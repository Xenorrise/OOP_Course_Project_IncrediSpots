using IncrediSpots.DataAccess.Context;
using IncrediSpots.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

public abstract class BaseRepository<TEntity> : IRepository<TEntity>
    where TEntity : BaseEntity
{
    protected readonly IncrediSpotsMainDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    protected BaseRepository(IncrediSpotsMainDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public virtual async Task<TEntity?> GetByIdAsync(int id)
        => await _dbSet.FindAsync(id);

    public virtual async Task AddAsync(TEntity entity)
        => await _dbSet.AddAsync(entity);

    public virtual void Update(TEntity entity)
        => _dbSet.Update(entity);

    public virtual void Delete(TEntity entity)
        => _dbSet.Remove(entity);
}
