using Taller.Shared.DTOs;
using Taller1.Shared.Entities;
using Taller1.Shared.Responses;

namespace Taller1.Backend.Repositories.Interfaces;

public interface IStatesRepository
{
    Task<ActionResponse<IEnumerable<State>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination);

    Task<ActionResponse<State>> GetAsync(int id);

    Task<ActionResponse<IEnumerable<State>>> GetAsync();
}