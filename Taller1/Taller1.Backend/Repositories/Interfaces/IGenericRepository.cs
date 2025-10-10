﻿using Taller.Shared.DTOs;
using Taller1.Shared.Entities;
using Taller1.Shared.Responses;

namespace Taller1.Backend.Repositories.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<ActionResponse<IEnumerable<T>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination);

    Task<ActionResponse<T>> GetAsync(int id);

    Task<ActionResponse<IEnumerable<T>>> GetAsync();

    Task<ActionResponse<IEnumerable<T>>> GetAsync(string query);

    Task<ActionResponse<T>> AddAsync(T entity);

    Task<ActionResponse<T>> DeleteAsync(int id);

    Task<ActionResponse<T>> UpdateAsync(T entity);
}