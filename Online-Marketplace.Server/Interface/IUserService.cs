using OnlineMarketplace.Server.Models;

namespace OnlineMarketplace.Server.Interface
{
    public interface IUserService
    {
        Task<ServiceResult> RegisterAsync(UserRegisterDto dto);
        Task<string> AuthenticateAsync(UserLoginDto dto);
    }
}
