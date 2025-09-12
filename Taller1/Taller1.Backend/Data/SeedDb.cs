using Taller1.Shared.Entities;

namespace Taller1.Backend.Data;

public class SeedDb
{
    private readonly DataContext _context;

    public SeedDb(DataContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        await _context.Database.EnsureCreatedAsync();
        await CheckEmployeesAsync();
    }

    private async Task CheckEmployeesAsync()
    {
        if (!_context.Employees.Any())
        {
            _context.Employees.Add(new Employee
            {
                FirstName = "Mateo",
                LastName = "Marquez",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 15000000
            });
            _context.Employees.Add(new Employee
            {
                FirstName = "Carlos",
                LastName = "Martinez",
                IsActive = false,
                HireDate = DateTime.UtcNow,
                Salary = 1700000
            });
            _context.Employees.Add(new Employee
            {
                FirstName = "Karina",
                LastName = "Suarez",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 2000000
            });
            _context.Employees.Add(new Employee
            {
                FirstName = "Juan",
                LastName = "Perez",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 1500000
            });
            _context.Employees.Add(new Employee
            {
                FirstName = "Sebastian",
                LastName = "Vasquez",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 1900000
            });
            _context.Employees.Add(new Employee
            {
                FirstName = "Juan Camilo",
                LastName = "Salgado",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 2500000
            });
            _context.Employees.Add(new Employee
            {
                FirstName = "Daniel Esteban",
                LastName = "Jaramillo",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 1900000
            });
            _context.Employees.Add(new Employee
            {
                FirstName = "Elkin",
                LastName = "Perez",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 3500000
            });
            _context.Employees.Add(new Employee
            {
                FirstName = "Diana Carolina",
                LastName = "Jaramillo",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 4500000
            });
            _context.Employees.Add(new Employee
            {
                FirstName = "Luisa",
                LastName = "Restrepo",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 1500000
            });
            _context.Employees.Add(new Employee
            {
                FirstName = "Fabiola",
                LastName = "Patiño",
                IsActive = false,
                HireDate = DateTime.UtcNow,
                Salary = 1950000
            });
            await _context.SaveChangesAsync();
        }
    }
}