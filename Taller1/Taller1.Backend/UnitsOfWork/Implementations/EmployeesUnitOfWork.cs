using DocuSign.eSign.Model;
using Taller.Shared.DTOs;
using Taller1.Backend.Repositories.Interfaces;
using Taller1.Backend.UnitsOfWork.Interfaces;
using Taller1.Shared.Entities;
using Taller1.Shared.Responses;

namespace Taller1.Backend.UnitsOfWork.Implementations;

public class EmployeesUnitOfWork : GenericsUnitOfWork<Employee>, IEmployeesUnitOfWork
{
    private readonly IEmployeesRepository _employeesRepository;

    public EmployeesUnitOfWork(IGenericRepository<Employee> repository, IEmployeesRepository employeesRepository) : base(repository)
    {
        _employeesRepository = employeesRepository;
    }

    public override async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination) => await _employeesRepository.GetTotalRecordsAsync(pagination);

    public override async Task<ActionResponse<IEnumerable<Employee>>> GetAsync(PaginationDTO pagination) => await _employeesRepository.GetAsync(pagination);

    public override async Task<ActionResponse<IEnumerable<Employee>>> GetAsync(string query)
    {
        return await _employeesRepository.GetAsync(query);
    }
}