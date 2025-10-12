using Microsoft.EntityFrameworkCore;
using Taller.Backend.Helpers;
using Taller.Shared.DTOs;
using Taller1.Backend.Data;
using Taller1.Backend.Repositories.Interfaces;
using Taller1.Shared.Responses;

namespace Taller1.Backend.Repositories.Implementations;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly DataContext _context;
    private readonly DbSet<T> _entity;

    public GenericRepository(DataContext context)
    {
        _context = context;
        _entity = _context.Set<T>();
    }

    public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync(PaginationDTO pagination)
    {
        var queryable = _entity.AsQueryable();

        return new ActionResponse<IEnumerable<T>>
        {
            WasSuccess = true,
            Result = await queryable
                .Paginate(pagination)
                .ToListAsync()
        };
    }

    public virtual async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination)
    {
        var queryable = _entity.AsQueryable();
        double count = await queryable.CountAsync();
        return new ActionResponse<int>
        {
            WasSuccess = true,
            Result = (int)count
        };
    }

    public virtual async Task<ActionResponse<T>> AddAsync(T entity)
    {
        _context.Add(entity);
        try
        {
            await _context.SaveChangesAsync();
            return new ActionResponse<T>
            {
                WasSuccess = true,
                Result = entity,
            };
        }
        catch (DbUpdateException)
        {
            return DbUpdateExceptionActionResponse();
        }
        catch (Exception exception)
        {
            return ExceptionActionResponse(exception);
        }
    }

    public virtual async Task<ActionResponse<T>> DeleteAsync(int id)
    {
        var row = await _entity.FindAsync(id);
        if (row == null)
        {
            return new ActionResponse<T>
            {
                Message = "El registro no fue encontrado.",
            };
        }
        _entity.Remove(row);
        await _context.SaveChangesAsync();
        return new ActionResponse<T>
        {
            WasSuccess = true,
        };
    }

    public virtual async Task<ActionResponse<T>> GetAsync(int id)
    {
        var row = await _entity.FindAsync(id);
        if (row == null)
        {
            return new ActionResponse<T>
            {
                Message = "El registro no fue encontrado.",
            };
        }
        return new ActionResponse<T>
        {
            WasSuccess = true,
            Result = row,
        };
    }

    public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync() => new ActionResponse<IEnumerable<T>>
    {
        WasSuccess = true,
        Result = await _entity.ToListAsync(),
    };

    public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return new ActionResponse<IEnumerable<T>>
            {
                Message = "Debe ingresar un texto de búsqueda."
            };
        }

        var entities = await _context.Set<T>()
            .Where(x => EF.Property<string>(x, "FirstName").Contains(query)
                     || EF.Property<string>(x, "LastName").Contains(query))
            .ToListAsync();

        if (!entities.Any())
        {
            return new ActionResponse<IEnumerable<T>>
            {
                Message = "No se encontraron registros con ese criterio."
            };
        }

        return new ActionResponse<IEnumerable<T>>
        {
            WasSuccess = true,
            Result = entities
        };
    }

    public virtual async Task<ActionResponse<T>> UpdateAsync(T entity)
    {
        _entity.Update(entity);
        try
        {
            await _context.SaveChangesAsync();
            return new ActionResponse<T>
            {
                WasSuccess = true,
                Result = entity,
            };
        }
        catch (DbUpdateException)
        {
            return DbUpdateExceptionActionResponse();
        }
        catch (Exception exception)
        {
            return ExceptionActionResponse(exception);
        }
    }

    private ActionResponse<T> ExceptionActionResponse(Exception exception) => new ActionResponse<T>
    {
        Message = exception.Message,
    };

    private ActionResponse<T> DbUpdateExceptionActionResponse() => new ActionResponse<T>
    {
        Message = "Ya existe el registro.",
    };
}