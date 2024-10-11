using Data.Context;
using Data.Identity;
using Data.Repositories;
using Data.UnitOfWorks;
using Entity.Repositories;
using Entity.Services;
using Entity.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Service.Mapping;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Service.Extensions
{
    public static class DependencyExtensions
    {
        public static void AddExtensions(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(
                opt =>
                {
                    opt.Password.RequireNonAlphanumeric = false;
                    opt.Password.RequiredLength = 3;
                    opt.Password.RequireLowercase = false;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequireDigit = false;

                    //opt.User.RequireUniqueEmail = true;  //Aynı email adresinin girilmesini izinverilmez
                    //opt.User.AllowedUserNameCharacters = "abcçdefgğhiıjklmnoöprsştuüvyz"; //kullanıcı adı girilirken izin verilen karakterler.

                    opt.Lockout.MaxFailedAccessAttempts = 3;
                    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1); //default = 5
                }).AddEntityFrameworkStores<ComputerDbContext>();

            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = new PathString("/Account/Login");
                opt.LogoutPath = new PathString("/Account/Logout");
                //opt.AccessDeniedPath = new PathString("/Account/Accessdenied");
                opt.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                opt.SlidingExpiration = true; //10 dk dolmadan yeniden login olursa süre baştan başlar.,

                opt.Cookie = new CookieBuilder()
                {
                    Name = "Identity.App.Cookie",
                    HttpOnly = true,
                };
            });

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            services.AddScoped(typeof(IComputerPartService), typeof(ComputerPartService));
            services.AddScoped(typeof(ICategoryService), typeof(CategoryService));
            services.AddScoped(typeof(IAccountService), typeof(AccountService));
            services.AddScoped(typeof(IComputerPartSaleDetailService), typeof(ComputerPartSaleDetailService));
            services.AddScoped(typeof(IComputerPartSaleService), typeof(ComputerPartSaleService));
            services.AddScoped(typeof(ISepetDetayService), typeof(SepetDetayService));
            services.AddScoped(typeof(IFeedBackService), typeof(FeedBackService));

            services.AddAutoMapper(typeof(MappingProfile));
            
        }
    }
}
