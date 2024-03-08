using System.ComponentModel.DataAnnotations;

namespace Orders2024.Shared.Entities;

public class Country
{
    public Guid Id { get; set; }

    [Display(Name = "Pais")]
    [MaxLength(100, ErrorMessage = "El capo {0} debe tener maximo {1} caracteres")]
    [Required(ErrorMessage = "El campo {0} es requerido")]
    public string? Name { get; set; }
}