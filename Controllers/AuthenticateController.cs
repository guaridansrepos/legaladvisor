using Advocate_Invoceing.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Advocate_Invoceing.BAL;
using Advocate_Invoceing.Models.Entity;

namespace Advocate_Invoceing.Controllers
{
    public class AuthenticateController : Controller
    {
        private readonly IUserService _Service;
        private readonly MyDbContext _context;
        

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        private readonly IConfiguration _config;


        public AuthenticateController(IUserService Service, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment, MyDbContext context, IConfiguration config)
        {
            _Service = Service;
            _hostingEnvironment = hostingEnvironment;
            _context = context;
            _config = config;
            
        }


        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (ModelState.IsValid)
            {



                var result = _Service.LoginCheck(request);

                // If the login attempt is unsuccessful, show error
                if (result.statusCode == 0)
                {
                    ViewBag.ErrorMessage = result.Message;
                    return View(request);
                }

                // Assign claims based on the login result
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, result.userId.ToString()), // User ID claim
            new Claim(ClaimTypes.Name, result.userName), // User name claim
            new Claim("UserType", result.userTypeName) // Custom claim for user type
        };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties { IsPersistent = true };

                // Sign the user in with the claims
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                              new ClaimsPrincipal(claimsIdentity), authProperties);

                // Store user information in session
                HttpContext.Session.SetString("UserId", result.userId.ToString());
                HttpContext.Session.SetString("UserName", result.userName);
                HttpContext.Session.SetObjectAsJson("loggedUser", result);

                // Redirect based on the user type
                return result.userTypeName switch
                {
                    "SuperAdmin" => RedirectToAction("AdminDash", "Home"),
                    "Admin" => RedirectToAction("AdminDash", "Home"),
                    "Employee" => RedirectToAction("Index", "Home"),
                    "HR" => RedirectToAction("Index", "Home"),
                    _ => RedirectToAction("Index", "Home") // Default redirect
                };
            }

            // If validation fails, return to login view
            return View(request);
        }


        public IActionResult Logout()
        {
            HttpContext.Session.SetObjectAsJson("loggedUser", null);
            return RedirectToAction("Login", "Authenticate");
        }



    }
}
