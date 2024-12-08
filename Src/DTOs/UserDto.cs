namespace dotnet_exam2.Src.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateOnly BirthDate { get; set; }
        public GenderDto Gender { get; set; } = null!;
    }
}
