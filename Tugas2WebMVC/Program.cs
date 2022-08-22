using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Tugas2WebMVC.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Pengaturan Authentikasi
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
    {
        options.AccessDeniedPath = "/denied";
        options.Events = new CookieAuthenticationEvents()
        {
            OnSigningIn = async context =>
            {
                var principal = context.Principal;
                if(principal.HasClaim(c => c.Type == ClaimTypes.NameIdentifier))
                {
                    if (principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value == "Email") 
                    {
                        var claimsIdentity = principal.Identity as ClaimsIdentity;
                        claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
                    }
                }
                await Task.CompletedTask;
            },
            OnSignedIn = async context =>
            {
                await Task.CompletedTask;
            },
            OnValidatePrincipal = async context =>
            {
                await Task.CompletedTask;
            }
        };        
    });

//Untuk Melempar Ke Halaman Login jika belum Login
builder.Services.ConfigureApplicationCookie(opt => opt.LoginPath = "/User/Login");

//Mendaftarkan Services
builder.Services.AddScoped<IStudent, StudentServices>(); 
builder.Services.AddScoped<IAdmin, AdminServices>(); 
builder.Services.AddScoped<IEnrollment, EnrollServices>(); 
builder.Services.AddScoped<ICourse, CourseServices>(); 
builder.Services.AddScoped<IUser, UserServices>();
//AddScoped ini harus berada diatas builder.Build();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = "mysession.frontend";
    options.IdleTimeout = TimeSpan.FromHours(2);
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
