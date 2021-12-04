using Api.Models;
using System.Threading.Tasks;

namespace Api.Services
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(LoginUserDTO userDTO);
        Task<string> CreateToken();

    }
}
