namespace LinkUp.Core.Applicacion.Dtos.User
{
    public class RegisterResponseDto
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string ProfileImage { get; set; }
        public required bool IsActive { get; set; }

        public bool HasError {  get; set; }
        public string? MessageError { get; set; }
    }
}
