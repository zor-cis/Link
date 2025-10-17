using LinkUp.Core.Applicacion.Dtos.User;

namespace LinkUp.Core.Applicacion.Interfaces
{
    public interface IAccountServiceForWebApp
    {
        Task<LoginResponseDto> AuthenticaAsync(LoginDto Login);
        Task<string> ConfirmAccountAsync(string userid, string token);
        Task<DeleteResponseDto> DeleteAsync(string Id);
        Task<EditUserResponseDto> EditUserAsync(SaveUserDto edit, string origin);
        Task<ForgotPasswordResponseDto> ForgotPasswordAsync(ForgotPasswordRequestDto forgotPassword);
        Task<List<UserDto>> GetAllUsersAsync(bool? isActive = true);
        Task<UserDto?> GetUserByEmail(string email);
        Task<UserDto?> GetUserByUserName(string username);
        Task<RegisterResponseDto> RegisterUserAsync(SaveUserDto register, string origin);
        Task<ForgotPasswordResponseDto> ResetPasswordAsync(ResetPasswordRequestDto resetPassword);
        Task SingOutAsync();
    }
}