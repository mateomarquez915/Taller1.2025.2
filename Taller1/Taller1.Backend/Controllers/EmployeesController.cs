using DocuSign.eSign.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taller1.Backend.Data;
using Taller1.Backend.UnitsOfWork.Interfaces;
using Taller1.Shared.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Taller1.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : GenericController<Employee>
{
    public EmployeesController(IGenericUnitOfWork<Employee> unitOfWork) : base(unitOfWork)
    {
    }
}