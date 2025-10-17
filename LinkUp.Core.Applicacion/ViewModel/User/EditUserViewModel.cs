using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LinkUp.Core.Applicacion.ViewModel.User
{
    public class EditUserViewModel
    {
        public required string Id { get; set; }

        [Required(ErrorMessage = "Ingrese el nombre")]
        [DataType(DataType.Text)]
        public required string Name { get; set; }


        [Required(ErrorMessage = "Ingrese el apellido")]
        [DataType(DataType.Text)]
        public required string LastName { get; set; }


        [Required(ErrorMessage = "Ingrese el nombre de usuario")]
        [DataType(DataType.Text)]
        public required string UserName { get; set; }


        [Required(ErrorMessage = "Ingrese el correo electronico")]
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }


        [DataType(DataType.Password)]
        public string? Password { get; set; }


        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }


        [Required(ErrorMessage = "Ingrese el numero de telefono")]
        [DataType(DataType.PhoneNumber)]
        public required string PhoneNumber { get; set; }


        [DataType(DataType.Upload)]
        public IFormFile? ProfileImage { get; set; }
    }
}
