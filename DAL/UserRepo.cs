using Advocate_Invoceing.Models.Entity;
using Advocate_Invoceing.Utilities;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace Advocate_Invoceing.DAL
{
    public class UserRepo : IUserRepo
    {
        private readonly MyDbContext _context;
        private readonly IConfiguration _config;

        public UserRepo(MyDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }



        public LoginResponse LoginCheck(LoginRequest request)
        {
            LoginResponse lr = new LoginResponse();
            try
            {

                var u = _context.userEntity.Where(a => a.IsDeleted == false && a.IsActive == true && a.Email == request.Email).FirstOrDefault();

                if (u == null)
                {
                    lr.statusCode = 0;
                    lr.Message = "Please enter valid email";
                    return lr;
                }
                else
                {
                    var p = EncryptModel.Decrypt(u.PasswordHash);
                    if (request.Password == EncryptModel.Decrypt(u.PasswordHash))
                    {
                        lr.statusCode = 1;
                        lr.Message = "success";
                        lr.userTypeName = _context.userTypeEntites.Where(a => a.UserTypeId == u.UserTypeId).Select(b => b.UserTypeName).FirstOrDefault();
                        lr.userName = u.FullName;
                        lr.userId = u.UserId;
                       // lr.profileUrl = u.ProfilePicture == null ? "dummy.png" : u.ProfilePicture;

                        // entry in lastlogin detials

                        lr.statusCode = 1;
                        lr.Message = "Login success";
                        return lr;
                    }
                    else
                    {
                        lr.statusCode = 0;
                        lr.Message = "Password incorrect";
                        return lr;
                    }
                }

            }
            catch (Exception ex)
            {


            }

            return lr;
        }


    }
}
