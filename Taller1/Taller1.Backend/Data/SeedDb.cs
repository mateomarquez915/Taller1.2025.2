using Microsoft.EntityFrameworkCore;
using Taller1.Backend.UnitsOfWork.Interfaces;
using Taller1.Shared.Entities;
using Taller1.Shared.Enums;

namespace Taller1.Backend.Data;

public class SeedDb
{
    private readonly DataContext _context;
    private readonly IUsersUnitOfWork _usersUnitOfWork;

    public SeedDb(DataContext context, IUsersUnitOfWork usersUnitOfWork)
    {
        _context = context;
        _usersUnitOfWork = usersUnitOfWork;
    }

    public async Task SeedAsync()
    {
        await _context.Database.EnsureCreatedAsync();
        await CheckEmployeesAsync();
        await CheckCountriesFullAsync();
        await CheckCountriesAsync();
        await CheckRolesAsync();
        await CheckUserAsync("1010", "Mateo", "Marquez", "MateoMarquez@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", UserType.Admin);
    }

    private async Task CheckRolesAsync()
    {
        await _usersUnitOfWork.CheckRoleAsync(UserType.Admin.ToString());
        await _usersUnitOfWork.CheckRoleAsync(UserType.User.ToString());
    }

    private async Task<User> CheckUserAsync(string document, string firstName, string lastName, string email, string phone, string address, UserType userType)
    {
        var user = await _usersUnitOfWork.GetUserAsync(email);
        if (user == null)
        {
            user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                UserName = email,
                PhoneNumber = phone,
                Address = address,
                Document = document,
                City = _context.Cities.FirstOrDefault(),
                UserType = userType,
            };
            await _usersUnitOfWork.AddUserAsync(user, "123456");
            await _usersUnitOfWork.AddUserToRoleAsync(user, userType.ToString());
        }

        return user;
    }

    private async Task CheckCountriesFullAsync()
    {
        if (!_context.Countries.Any())
        {
            var countriesSQLScript = File.ReadAllText("Data\\CountriesStatesCities.sql");
            await _context.Database.ExecuteSqlRawAsync(countriesSQLScript);
        }
    }

    private async Task CheckCountriesAsync()
    {
        if (!_context.Countries.Any())
        {
            _context.Countries.Add(new Country
            {
                Name = "Colombia",
                States = [
                    new State()
                    {
                        Name = "Antioquia",
                        Cities = [
                            new City() { Name = "Medellín" },
                            new City() { Name = "Itagüí" },
                            new City() { Name = "Envigado" },
                            new City() { Name = "Bello" },
                            new City() { Name = "Rionegro" },
                        ]
                    },
                    new State()
                    {
                        Name = "Bogotá",
                        Cities = [
                            new City() { Name = "Usaquen" },
                            new City() { Name = "Champinero" },
                            new City() { Name = "Santa fe" },
                            new City() { Name = "Useme" },
                            new City() { Name = "Bosa" },
                        ]
                    },
                ]
            });
            _context.Countries.Add(new Country
            {
                Name = "Estados Unidos",
                States = [
                    new State()
                {
                    Name = "Florida",
                    Cities = [
                        new City() { Name = "Orlando" },
                        new City() { Name = "Miami" },
                        new City() { Name = "Tampa" },
                        new City() { Name = "Fort Lauderdale" },
                        new City() { Name = "Key West" },
                    ]
                },
                new State()
                    {
                        Name = "Texas",
                        Cities = [
                            new City() { Name = "Houston" },
                            new City() { Name = "San Antonio" },
                            new City() { Name = "Dallas" },
                            new City() { Name = "Austin" },
                            new City() { Name = "El Paso" },
                        ]
                    },
                ]
            });
        }
        await _context.SaveChangesAsync();
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
            _context.Employees.Add(new Employee
            {
                FirstName = "Juan",
                LastName = "Lopez",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 2200000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Andres",
                LastName = "Gomez",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 2500000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Camila",
                LastName = "Ramirez",
                IsActive = false,
                HireDate = DateTime.UtcNow,
                Salary = 3000000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Daniela",
                LastName = "Torres",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 2800000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Felipe",
                LastName = "Castro",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 3100000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Maria",
                LastName = "Rojas",
                IsActive = false,
                HireDate = DateTime.UtcNow,
                Salary = 1800000
            });
            _context.Employees.Add(new Employee
            {
                FirstName = "Julian",
                LastName = "Herrera",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 4000000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Laura",
                LastName = "Vargas",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 3500000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Santiago",
                LastName = "Mendoza",
                IsActive = false,
                HireDate = DateTime.UtcNow,
                Salary = 2200000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Valentina",
                LastName = "Guerrero",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 3300000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Sebastian",
                LastName = "Paredes",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 2800000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Natalia",
                LastName = "Diaz",
                IsActive = false,
                HireDate = DateTime.UtcNow,
                Salary = 2100000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Jorge",
                LastName = "Cardenas",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 4500000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Paula",
                LastName = "Morales",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 2600000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Ricardo",
                LastName = "Alvarez",
                IsActive = false,
                HireDate = DateTime.UtcNow,
                Salary = 1900000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Diana",
                LastName = "Camacho",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 3200000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Angela",
                LastName = "Perdomo",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 2700000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Oscar",
                LastName = "Peña",
                IsActive = false,
                HireDate = DateTime.UtcNow,
                Salary = 2300000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Carolina",
                LastName = "Zuluaga",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 3400000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Esteban",
                LastName = "Cruz",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 2900000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Tatiana",
                LastName = "Salazar",
                IsActive = false,
                HireDate = DateTime.UtcNow,
                Salary = 2500000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Mauricio",
                LastName = "Forero",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 3700000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Catalina",
                LastName = "Restrepo",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 3300000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "David",
                LastName = "Loaiza",
                IsActive = false,
                HireDate = DateTime.UtcNow,
                Salary = 2800000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Isabela",
                LastName = "Correa",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 3100000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Fernando",
                LastName = "Arango",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 4100000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Alejandra",
                LastName = "Hoyos",
                IsActive = false,
                HireDate = DateTime.UtcNow,
                Salary = 2400000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Camilo",
                LastName = "Giraldo",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 3600000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Luisa",
                LastName = "Beltran",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 2700000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Hernan",
                LastName = "Nieto",
                IsActive = false,
                HireDate = DateTime.UtcNow,
                Salary = 2200000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Gloria",
                LastName = "Bermudez",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 3200000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Anderson",
                LastName = "Rincon",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 3000000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Marcela",
                LastName = "Mora",
                IsActive = false,
                HireDate = DateTime.UtcNow,
                Salary = 2100000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Diego",
                LastName = "Valencia",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 3800000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Lorena",
                LastName = "Castellanos",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 2600000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Ivan",
                LastName = "Bonilla",
                IsActive = false,
                HireDate = DateTime.UtcNow,
                Salary = 2300000
            });
            _context.Employees.Add(new Employee
            {
                FirstName = "Sandra",
                LastName = "Trujillo",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 3500000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Rafael",
                LastName = "Montoya",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 4000000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Yesica",
                LastName = "Puentes",
                IsActive = false,
                HireDate = DateTime.UtcNow,
                Salary = 2000000
            });

            await _context.SaveChangesAsync();
        }
    }
}