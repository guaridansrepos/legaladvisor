using Advocate_Invoceing.DAL;
using Advocate_Invoceing.Models.Entity;
using Advocate_Invoceing.Utilities;
using Microsoft.AspNetCore.Identity.Data;

namespace Advocate_Invoceing.BAL
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _UserRepo;
        

        public UserService(IUserRepo UserRepo)
        {
            _UserRepo = UserRepo;
            
        }


        public LoginResponse LoginCheck(LoginRequest request)
        {
            return _UserRepo.LoginCheck(request);
        }
    }
}
