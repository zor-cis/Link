using System.ComponentModel.DataAnnotations;

namespace LinkUp.Core.Applicacion.ViewModel.User
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Ingrese el nombre de usuario")]
        [DataType(DataType.Text)]
        public required string UserName { get; set; }


        [Required(ErrorMessage = "Ingrese la contraseña")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
