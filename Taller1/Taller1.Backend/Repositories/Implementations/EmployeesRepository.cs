using DocuSign.eSign.Model;
using Microsoft.EntityFrameworkCore;
using Taller.Backend.Helpers;
using Taller.Shared.DTOs;
using Taller1.Backend.Data;
using Taller1.Backend.Repositories.Interfaces;
using Taller1.Shared.Entities;
using Taller1.Shared.Responses;

namespace Taller1.Backend.Repositories.Implementations;

public class EmployeesRepository : GenericRepository<Employee>, IEmployeesRepository
{
    private readonly DataContext _context;

    public EmployeesRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<ActionResponse<IEnumerable<Employee>>> GetAsync(PaginationDTO pagination)
    {
        var queryable = _context.Employees
                 .AsQueryable();

        if (!string.IsNullOrWhiteSpace(pagination.Filter))
        {
            var filter = pagination.Filter.ToLower();
            queryable = queryable.Where(x =>
                x.FirstName.ToLower().Contains(filter) ||
                x.LastName.ToLower().Contains(filter));
        }

        return new ActionResponse<IEnumerable<Employee>>
        {
            WasSuccess = true,
            Result = await queryable
            .OrderBy(e => e.FirstName)
            .ThenBy(e => e.LastName)
            .Paginate(pagination)
            .ToListAsync()
        };
    }

    public override async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination)
    {
        var queryable = _context.Employees.AsQueryable();

        if (!string.IsNullOrWhiteSpace(pagination.Filter))
        {
            var filter = pagination.Filter.ToLower();
            queryable = queryable.Where(x =>
                x.FirstName.ToLower().Contains(filter) ||
                x.LastName.ToLower().Contains(filter));
        }

        double count = await queryable.CountAsync();
        return new ActionResponse<int>
        {
            WasSuccess = true,
            Result = (int)count
        };
    }

    public override async Task<ActionResponse<IEnumerable<Employee>>> GetAsync(string query)
    {
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return new ActionResponse<IEnumerable<Employee>>
                {
                    Message = "Debe ingresar un texto de búsqueda."
                };
            }

            var entities = await _context.Set<Employee>()
                .Where(x => EF.Property<string>(x, "FirstName").Contains(query)
                         || EF.Property<string>(x, "LastName").Contains(query))
                .ToListAsync();

            if (!entities.Any())
            {
                return new ActionResponse<IEnumerable<Employee>>
                {
                    Message = "No se encontraron registros con ese criterio."
                };
            }

            return new ActionResponse<IEnumerable<Employee>>
            {
                WasSuccess = true,
                Result = entities
            };
        }
    }

    public override async Task<ActionResponse<Employee>> AddAsync(Employee entity)
    {
        if (entity.HireDate == default)
        {
            entity.HireDate = DateTime.Now;
        }

        _context.Employees.Add(entity);
        await _context.SaveChangesAsync();

        return new ActionResponse<Employee>
        {
            WasSuccess = true,
            Result = entity
        };
    }
}