using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LinkUp.Core.Applicacion.ViewModel.User
{
    public class RegisterUserViewModel
    {

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


        [Required(ErrorMessage = "Ingrese la contraseña")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }


        [Required(ErrorMessage = "Ingrese la contraseña")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden. Debe ingresra una contraseña que coincida. ")]
        public required string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "Ingrese el numero de telefono")]
        [DataType(DataType.PhoneNumber)]
        public required string PhoneNumber { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? ProfileImage { get; set; }
    }
}
