using DocuSign.eSign.Model;
using Taller.Shared.DTOs;
using Taller1.Shared.Entities;
using Taller1.Shared.Responses;

namespace Taller1.Backend.UnitsOfWork.Interfaces;

public interface IEmployeesUnitOfWork
{
    Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination);

    Task<ActionResponse<IEnumerable<Employee>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<IEnumerable<Employee>>> GetAsync(string query);
}