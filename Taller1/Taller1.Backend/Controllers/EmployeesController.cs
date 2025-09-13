using Microsoft.AspNetCore.Mvc;
using Taller1.Backend.UnitsOfWork.Interfaces;
using Taller1.Shared.Entities;

namespace Taller1.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : GenericController<Employee>
{
    public EmployeesController(IGenericUnitOfWork<Employee> unitOfWork) : base(unitOfWork)
    {
    }
}