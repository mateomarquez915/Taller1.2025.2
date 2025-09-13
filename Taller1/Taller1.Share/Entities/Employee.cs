using System.ComponentModel.DataAnnotations;

namespace Taller1.Shared.Entities;

public class Employee
{
    public int Id { get; set; }

    [Display(Name = "Primer Nombre")]
    [MaxLength(30, ErrorMessage = "El Campo {0} no puede tener más de {1} carácteres.")]
    [Required(ErrorMessage = "El campo {0} es obligatorio")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Apellido")]
    [MaxLength(30, ErrorMessage = "El Campo {0} no puede tener más de {1} carácteres.")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public string LastName { get; set; } = null!;

    [Display(Name = "¿Esta activo?")]
    public bool IsActive { get; set; }

    [Display(Name = "Fecha")]
    public DateTime HireDate { get; set; }

    [Display(Name = "Salario")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [Range(1000000, double.MaxValue, ErrorMessage = "El campo {0} no puede ser menor a 1.000.000")]
    public decimal Salary { get; set; }
}