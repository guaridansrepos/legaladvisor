using Advocate_Invoceing.Utilities;
using Microsoft.AspNetCore.Identity.Data;

namespace Advocate_Invoceing.DAL
{
    public interface IUserRepo
    {
        LoginResponse LoginCheck(LoginRequest request);
    }
}
