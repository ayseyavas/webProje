using Microsoft.EntityFrameworkCore;
using webProje.Models;
using webProje.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IBookTypeRepository, BookTypeRepository>();//_bookTypeRepository nesnenesi olu�turulur DI i�in gerekli
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IRentRepository, RentRepository>();

builder.Services.AddScoped<IEmailSender, EmailSender>();


builder.Services.AddDbContext<AppDBContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConenction")));//olu�turdu�um db s�n�f�n� app ba�lad���nda kullan�lmas�

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AppDBContext>().AddDefaultTokenProviders();
builder.Services.AddRazorPages();//scaffold identity yapısını kullanırken areas içerisindeki razor pageslerin kullanılabilmesi için



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",//program�n ilk �al��t�rmas�nda hangi linke y�nlendirece�ini s�yler 
    pattern: "{controller=Home}/{action=Index}/{id?}");//?=opsiyonel anlama gelir

app.Run();
