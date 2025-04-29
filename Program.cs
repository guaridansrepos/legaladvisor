using Advocate_Invoceing.BAL;
using Advocate_Invoceing.BAL.IService;
using Advocate_Invoceing.BAL.Service;
using Advocate_Invoceing.DAL;
using Advocate_Invoceing.DAL.Interface;
using Advocate_Invoceing.DAL.Repo;
using Advocate_Invoceing.Models.Entity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    })
    .AddRazorRuntimeCompilation();

builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")), ServiceLifetime.Transient);

builder.Services.AddRazorPages();
builder.Services.AddCors();


double sessionTimeout = Convert.ToDouble(builder.Configuration["sessionTimeOut"]);
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(sessionTimeout);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();
builder.Services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmployeePunchRepo, EmployeePunchRepo>();
builder.Services.AddScoped<IAdminApprovalRepo, AdminApprovalRepo>();
builder.Services.AddScoped<IEmployeeLogRepo, EmployeeWorkLogRepo>();
builder.Services.AddScoped<IInvoiceRepo, InvoiceRepo>();
 
//builder.Services.AddScoped<ILeaveTypeMaster, LeaveTypeMaster>();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Authenticate/Login";
        options.AccessDeniedPath = "/Authenticate/Login";
    });



//builder.Services.AddAuthorization(options =>
//{
//    // Policy for AdminDashboard (SuperAdmin and Admin only)
//    options.AddPolicy("AdminAccess", policy =>
//        policy.RequireClaim("UserType", "SuperAdmin", "Admin", "HR"));

//    // Policy for EmployeeDashboard (SuperAdmin, Admin, Employee)
//    options.AddPolicy("EmployeeAccess", policy =>
//        policy.RequireClaim("UserType", "SuperAdmin", "Admin", "Employee", "HR"));
//    options.AddPolicy("Employee", policy =>
//        policy.RequireClaim("UserType", "Employee"));
//    options.AddPolicy("HRAccess", policy =>
//        policy.RequireClaim("UserType", "HR"));
//    options.AddPolicy("EmployeeOrHRAccess", policy =>
//       policy.RequireRole("EmployeeAccess", "HR"));
//});





builder.Services.AddControllersWithViews()
    .AddRazorOptions(options =>
    {
        options.ViewLocationFormats.Add("/Views/Shared/_Layout.cshtml");
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}


app.UseStaticFiles();
app.UseRouting();
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authenticate}/{action=Login}/{id?}");



app.Run();
