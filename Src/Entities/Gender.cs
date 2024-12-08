using System.ComponentModel.DataAnnotations;

namespace dotnet_exam2.Src.Entities
{
    public sealed class Gender
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del género es requerido.")]
        public required string Name { get; set; }

        public required IList<User> Users { get; set; }
    }
}
