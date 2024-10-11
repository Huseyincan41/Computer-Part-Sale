using Data.Context;
using Data.UnitOfWorks;
using Entity.Services;
using Entity.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Service.Services;
using System.Reflection;
using Service.Extensions;
using Data.Identity;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<ComputerDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnStr")));
builder.Services.AddScoped<IComputerPartService, ComputerPartService>();
builder.Services.AddScoped<IComputerPartSaleService, ComputerPartSaleService>();
builder.Services.AddScoped<IComputerPartSaleDetailService, ComputerPartSaleDetailService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddExtensions();
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(10);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
}
		   );


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
app.UseSession();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
     name: "areas",
       pattern: "{controller=Home}/{action=Index}/{id?}/{area=Admin}");

app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );



app.MapControllerRoute(
      name: "area",
      pattern: "{controller=Home}/{action=Index}/{area=Admin}"
    );




app.Run();
