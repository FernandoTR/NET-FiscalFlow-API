using FiscalFlow.Domain.Interfaces;
using FiscalFlow.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace FiscalFlow.Infrastructure.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        // Inicia la consulta con el DbSet correspondiente
        IQueryable<T> query = _dbSet;        

        // Obtiene el nombre de la clave primaria
        var primaryKey = _context.Model.FindEntityType(typeof(T))?.FindPrimaryKey();
        if (primaryKey == null)
        {
            throw new InvalidOperationException($"No se encontró clave primaria para la entidad {typeof(T).Name}.");
        }

        var primaryKeyName = primaryKey.Properties.FirstOrDefault()?.Name;
        if (string.IsNullOrEmpty(primaryKeyName))
        {
            throw new InvalidOperationException($"La clave primaria para la entidad {typeof(T).Name} no tiene un nombre válido.");
        }

        // Busca la entidad utilizando la clave primaria
        return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, primaryKeyName) == id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        // Inicia la consulta con el DbSet correspondiente
        IQueryable<T> query = _dbSet;
            

        return await query.ToListAsync();
    }

    public async Task<bool> AddAsync(T entity)
    {
        try
        {
            await _dbSet.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }
        catch (Exception ex)
        {
            //_logService.ErrorLog(nameof(AddAsync), ex);
            return false;
        }
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        try
        {
            _dbSet.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }
        catch (Exception ex)
        {
            //_logService.ErrorLog(nameof(UpdateAsync), ex);
            return false;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
                return false;

            _dbSet.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }
        catch (Exception ex)
        {
            //_logService.ErrorLog(nameof(DeleteAsync), ex);
            return false;
        }
    }

}
