﻿namespace LinkUp.Core.Applicacion.ViewModel.User
{
    public class UserViewModel
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string ProfileImage { get; set; }
        public required bool IsActive { get; set; }
    }
}
