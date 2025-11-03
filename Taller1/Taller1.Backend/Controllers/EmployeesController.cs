using DocuSign.eSign.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taller.Shared.DTOs;
using Taller1.Backend.UnitsOfWork.Interfaces;
using Taller1.Shared.Entities;

namespace Taller1.Backend.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
public class EmployeesController : GenericController<Employee>
{
    private readonly IEmployeesUnitOfWork _employeesUnitOfWork;

    public EmployeesController(IGenericUnitOfWork<Employee> unitOfWork, IEmployeesUnitOfWork employeesUnitOfWork) : base(unitOfWork)
    {
        _employeesUnitOfWork = employeesUnitOfWork;
    }

    [HttpGet("totalRecords")]
    public override async Task<IActionResult> GetTotalRecordsAsync([FromQuery] PaginationDTO pagination)
    {
        var action = await _employeesUnitOfWork.GetTotalRecordsAsync(pagination);
        if (action.WasSuccess)
        {
            return Ok(action.Result);
        }
        return BadRequest();
    }

    [HttpGet("paginated")]
    public override async Task<IActionResult> GetAsync(PaginationDTO pagination)
    {
        var response = await _employeesUnitOfWork.GetAsync(pagination);
        if (response.WasSuccess)
        {
            return Ok(response.Result);
        }
        return BadRequest();
    }

    [HttpGet("search")]
    public override async Task<IActionResult> GetAsync([FromQuery] string query)
    {
        var response = await _employeesUnitOfWork.GetAsync(query);

        if (!response.WasSuccess)
        {
            return NotFound(response.Message);
        }

        return Ok(response.Result);
    }
}