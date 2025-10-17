namespace LinkUp.Core.Applicacion.Dtos.User
{
    public class SaveUserDto
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string PhoneNumber { get; set; }
        public required string ProfileImage { get; set; }
    }
}
