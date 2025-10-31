using DocuSign.eSign.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taller1.Shared.Interfaces;

namespace Taller1.Shared.Entities;

public class City : IEntityWithName
{
    public int Id { get; set; }

    [Display(Name = "Ciudad")]
    [MaxLength(100, ErrorMessage = "EL campo {0} no puede tener mas de {1} caracteres.")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public string Name { get; set; } = null!;

    public int StateId { get; set; }

    public State? State { get; set; }
}