using LinkUp.Core.Applicacion.Dtos.Email;
using LinkUp.Core.Applicacion.Dtos.User;
using LinkUp.Core.Applicacion.Interfaces;
using LinkUp.Infrastructure.Shared.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.Text;

namespace LinkUp.Infrastructure.Identity.Services
{
    public class AccountServiceForWebApp : IAccountServiceForWebApp
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;

        public AccountServiceForWebApp(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        public async Task<LoginResponseDto> AuthenticaAsync(LoginDto Login)
        {
            LoginResponseDto response = new()
            {
                Id = "",
                Name = "",
                LastName = "",
                UserName = "",
                Email = "",
                PhoneNumber = "",
                ProfileImage = "",
                IsActive = false,
                HasError = false
            };

            var user = await _userManager.FindByNameAsync(Login.UserName);
            if (user == null)
            {
                response.HasError = true;
                response.MessageError = $"No existe una cuenta con el nombre de usuario: {Login.UserName}";
                return response;
            }

            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.MessageError = $"La cuenta {Login.UserName} no ha sido activada. Por favor revisar su correo para realizar la activacion.";
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(Login.UserName, Login.Password, false, true);

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.MessageError = $"Credenciales incorrecta del usuario: {Login.UserName}";
                return response;
            }

            response.Id = user.Id;
            response.Name = user.Name;
            response.LastName = user.LastName;
            response.UserName = user.UserName ?? "";
            response.Email = user.Email ?? "";
            response.PhoneNumber = user.PhoneNumber ?? "";
            response.ProfileImage = user.ProfileImage;
            response.IsActive = user.EmailConfirmed;


            return response;
        }

        public async Task SingOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<RegisterResponseDto> RegisterUserAsync(SaveUserDto register, string origin)
        {
            RegisterResponseDto response = new()
            {
                Id = "",
                Name = "",
                LastName = "",
                UserName = "",
                Email = "",
                PhoneNumber = "",
                ProfileImage = "",
                IsActive = false,
                HasError = false
            };

            var userWithSameUserName = await _userManager.FindByNameAsync(register.UserName);

            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.MessageError = $"El nombre de usuario {register.UserName} ya esta registrado.";
                return response;
            }


            var userWithSameEmail = await _userManager.FindByEmailAsync(register.Email);

            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.MessageError = $"El correo {register.Email} ya tiene una cuenta registrada.";
                return response;
            }

            AppUser newUser = new()
            {
                Name = register.Name,
                LastName = register.LastName,
                UserName = register.UserName,
                Email = register.Email,
                PhoneNumber = register.PhoneNumber,
                ProfileImage = register.ProfileImage,
                IsActive = false,
                EmailConfirmed = false
            };

            var result = await _userManager.CreateAsync(newUser, register.Password);

            if (result.Succeeded)
            {
                string verificationUrlEmail = await GetVerificationEmailUri(newUser, origin);

                await _emailService.SendEmailAsync(new EmailRequest()
                {
                    To = register.Email,
                    HtmlBody = $"Por favor confirmar su correo ingresando a la URL: {verificationUrlEmail}",
                    Subject = $"Confirmar registro"
                });

                response.Id = newUser.Id;
                response.Name = newUser.Name;
                response.LastName = newUser.LastName;
                response.UserName = newUser.UserName ?? "";
                response.Email = newUser.Email ?? "";
                response.PhoneNumber = newUser.PhoneNumber ?? "";
                response.ProfileImage = newUser.ProfileImage;
                response.IsActive = newUser.EmailConfirmed;

                return response;
            }
            else
            {
                response.HasError = true;
                response.MessageError = "Error al intentar crear la cuenta";
                return response;
            }
        }

        public async Task<string> ConfirmAccountAsync(string userId, string token)
        {

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return "There is no acccount registered with this user";
            }

            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return $"Account confirmed for {user.Email}. You can now use the app";
            }
            else
            {
                return $"An error occurred while confirming this email {user.Email}";
            }
        }

        public async Task<EditUserResponseDto> EditUserAsync(SaveUserDto edit, string origin, bool? isCreated = false)
        {
            bool isNotcreated = !isCreated ?? false;
            EditUserResponseDto response = new()
            {
                Id = "",
                Name = "",
                LastName = "",
                UserName = "",
                Email = "",
                PhoneNumber = "",
                ProfileImage = "",
                IsActive = false,
                HasError = false
            };

            var userWithSameUserName = await _userManager.Users.FirstOrDefaultAsync(w => w.UserName == edit.UserName && w.Id != edit.Id);
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.MessageError = $"El nombre de usuario {edit.UserName} ya esta registrado.";
                return response;
            }


            var userWithSameEmail = await _userManager.Users.FirstOrDefaultAsync(w => w.Email == edit.Email && w.Id != edit.Id);
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.MessageError = $"El correo {edit.UserName} ya tiene una cuenta registrada.";
                return response;
            }

            var user = await _userManager.FindByIdAsync(edit.Id);

            if (user == null)
            {
                response.HasError = true;
                response.MessageError = $"No se pudo encontrar el usuario";
                return response;
            }

            user.Id = edit.Id;
            user.Name = edit.Name;
            user.LastName = edit.LastName;
            user.UserName = edit.UserName;
            user.Email = edit.Email;
            user.PhoneNumber = edit.PhoneNumber;
            user.ProfileImage = string.IsNullOrWhiteSpace(edit.ProfileImage) ? user.ProfileImage : edit.ProfileImage;
            user.EmailConfirmed = user.EmailConfirmed && user.Email == edit.Email;
            


            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                if (!user.EmailConfirmed && isNotcreated)
                {
                    string verificationUrlEmail = await GetVerificationEmailUri(user, origin);

                    await _emailService.SendEmailAsync(new EmailRequest()
                    {
                        To = edit.Email,
                        HtmlBody = $"Por favor confirmar su correo ingresando a la URL: {verificationUrlEmail}",
                        Subject = $"Confirmar registro"
                    });
                }

                if (!string.IsNullOrWhiteSpace(edit.Password))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    await _userManager.ResetPasswordAsync(user, token, edit.Password);
                }

                response.Id = user.Id;
                response.Name = user.Name;
                response.LastName = user.LastName;
                response.UserName = user.UserName ?? "";
                response.Email = user.Email ?? "";
                response.PhoneNumber = user.PhoneNumber ?? "";
                response.ProfileImage = user.ProfileImage;
                response.IsActive = user.IsActive;

                return response;
            }
            else
            {
                response.HasError = true;
                response.MessageError = "Error al intentar editar la cuenta";
                return response;
            }
        }

        public async Task<DeleteResponseDto> DeleteAsync(string Id)
        {
            DeleteResponseDto response = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByIdAsync(Id);

            if (user == null)
            {
                response.HasError = true;
                response.MessageError = $"No se pudo encontrar el usuario";
                return response;
            }
            await _userManager.DeleteAsync(user);
            return response;
        }

        public async Task<List<UserDto>> GetAllUsersAsync(bool? isActive = true)
        {
            var query = _userManager.Users.AsQueryable();

            if (isActive != null)
            {
                query = query.Where(u => u.EmailConfirmed == isActive.Value && u.IsActive == isActive.Value);
            }

            var users = await query.Select(s => new UserDto
            {
                Id = s.Id,
                Name = s.Name,
                LastName = s.LastName ?? "",
                UserName = s.UserName ?? "",
                Email = s.Email ?? "",
                PhoneNumber = s.PhoneNumber ?? "",
                ProfileImage = s.ProfileImage ?? "",
                IsActive = s.EmailConfirmed
            }).ToListAsync();

            return users;
        }

        public async Task<UserDto?> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return null;
            }

            var result = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName ?? "",
                UserName = user.UserName ?? "",
                Email = user.Email ?? "",
                PhoneNumber = user.PhoneNumber ?? "",
                ProfileImage = user.ProfileImage ?? "",
                IsActive = user.EmailConfirmed
            };

            return result;
        }

        public async Task<UserDto?> GetUserByUserName(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return null;
            }

            var result = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName ?? "",
                UserName = user.UserName ?? "",
                Email = user.Email ?? "",
                PhoneNumber = user.PhoneNumber ?? "",
                ProfileImage = user.ProfileImage ?? "",
                IsActive = user.EmailConfirmed
            };

            return result;
        }

        public async Task<ForgotPasswordResponseDto> ForgotPasswordAsync(ForgotPasswordRequestDto forgotPassword)
        {
            ForgotPasswordResponseDto response = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByNameAsync(forgotPassword.UserName);

            if (user == null)
            {
                response.HasError = true;
                response.MessageError = $"No se pudo encontrar el usuario: {forgotPassword.UserName}";
                return response;
            }

            var resetUrl = GetResetPasswordlUrl(user, forgotPassword.Origin);

            user.EmailConfirmed = false;
            await _userManager.UpdateAsync(user);

            await _emailService.SendEmailAsync(new EmailRequest()
            {
                To = user.Email,
                HtmlBody = $"Ingrese a la url para cambiar la contraseña: {resetUrl}",
                Subject = $"Resetear contraseña"
            });

            return response;
        }

        public async Task<ForgotPasswordResponseDto> ResetPasswordAsync(ResetPasswordRequestDto resetPassword)
        {
            ForgotPasswordResponseDto response = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByIdAsync(resetPassword.Id);

            if (user == null)
            {
                response.HasError = true;
                response.MessageError = "No se pudo encontrar el usuario";
                return response;
            }

            var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(resetPassword.Token));
            var result = await _userManager.ResetPasswordAsync(user, token, resetPassword.Password);

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.MessageError = "No se pudo resetear la contraseña";
                return response;
            }

            user.EmailConfirmed = true;
            await _userManager.UpdateAsync(user);

            return response;
        }



        #region Private Mathods 

        private async Task<string> GetVerificationEmailUri(AppUser user, string origin)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var route = "Login/ConfirmEmail";
            var completeUrl = new Uri(string.Concat(origin, "/", route));// origin = https://localhost:58296 route=Login/ConfirmEmail
            var verificationUri = QueryHelpers.AddQueryString(completeUrl.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri.ToString(), "token", token);

            return verificationUri;
        }

        private async Task<string> GetResetPasswordlUrl(AppUser user, string origin)
        {

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var route = "Login/ResetPassword";

            var completeUrl = new Uri(string.Concat($"{origin}/", route));

            var resetUrl = QueryHelpers.AddQueryString(completeUrl.ToString(), "userId", user.Id);
            resetUrl = QueryHelpers.AddQueryString(resetUrl.ToString(), "token", token);
            return resetUrl;
        }

        #endregion
    }
}
