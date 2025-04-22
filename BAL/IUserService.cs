using Advocate_Invoceing.Utilities;
using Microsoft.AspNetCore.Identity.Data;

namespace Advocate_Invoceing.BAL
{
    public interface IUserService
    {

        LoginResponse LoginCheck(LoginRequest request);
    }
}
