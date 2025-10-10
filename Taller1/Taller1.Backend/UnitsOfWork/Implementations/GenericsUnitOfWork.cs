using Taller.Shared.DTOs;
using Taller1.Backend.Repositories.Interfaces;
using Taller1.Backend.UnitsOfWork.Interfaces;
using Taller1.Shared.Entities;
using Taller1.Shared.Responses;

namespace Taller1.Backend.UnitsOfWork.Implementations;

public class GenericsUnitOfWork<T> : IGenericUnitOfWork<T> where T : class
{
    private readonly IGenericRepository<T> _repository;

    public GenericsUnitOfWork(IGenericRepository<T> repository)
    {
        _repository = repository;
    }

    public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync(PaginationDTO pagination) => await _repository.GetAsync(pagination);

    public virtual async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination) => await _repository.GetTotalRecordsAsync(pagination);

    public virtual async Task<ActionResponse<T>> AddAsync(T entity) => await _repository.AddAsync(entity);

    public virtual async Task<ActionResponse<T>> DeleteAsync(int id) => await _repository.DeleteAsync(id);

    public virtual async Task<ActionResponse<T>> GetAsync(int id) => await _repository.GetAsync(id);

    public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync() => await _repository.GetAsync();

    public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync(string query) => await _repository.GetAsync(query);

    public virtual async Task<ActionResponse<T>> UpdateAsync(T entity) => await _repository.UpdateAsync(entity);
}