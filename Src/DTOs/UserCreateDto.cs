using System.ComponentModel.DataAnnotations;
using dotnet_exam2.Src.Validators;

namespace dotnet_exam2.Src.DTOs
{
    public class UserCreateDto
    {
        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(
            20,
            MinimumLength = 3,
            ErrorMessage = "El nombre debe tener entre 3 y 20 caracteres."
        )]
        [RegularExpression(
            @"^[a-zA-Z0-9\s]*$",
            ErrorMessage = "El nombre solo puede contener caracteres alfanuméricos."
        )]
        public required string Name { get; set; }

        [Required(ErrorMessage = "El correo electrónico es requerido.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es requerida.")]
        [PastDate]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Nacimiento")]
        public required DateOnly BirthDate { get; set; }

        [Required(ErrorMessage = "El género es requerido.")]
        public required int GenderId { get; set; }
    }
}
