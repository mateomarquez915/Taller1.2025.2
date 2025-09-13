﻿using Taller1.Shared.Responses;

namespace Taller1.Backend.UnitsOfWork.Interfaces;

public interface IGenericUnitOfWork<T> where T : class

{
    Task<ActionResponse<T>> GetAsync(int id);

    Task<ActionResponse<IEnumerable<T>>> GetAsync();

    Task<ActionResponse<IEnumerable<T>>> SearchAsync(string query);

    Task<ActionResponse<T>> AddAsync(T entity);

    Task<ActionResponse<T>> DeleteAsync(int id);

    Task<ActionResponse<T>> UpdateAsync(T entity);
}