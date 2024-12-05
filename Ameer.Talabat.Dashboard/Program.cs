using Ameer.Talabat.Core.Application.Abstraction.Services;
using Ameer.Talabat.Core.Application.Mapping.ProductsMapping;
using Ameer.Talabat.Core.Domain.Contracts;
using Ameer.Talabat.Core.Domain.Entities.Identity;
using Ameer.Talabat.Dashboard.Mapping;
using Ameer.Talabat.Infrastructure.Helpers;
using Ameer.Talabat.Infrastructure.Persistance._Identity;
using Ameer.Talabat.Infrastructure.Persistance.Data;
using Ameer.Talabat.Infrastructure.Persistance.UnitOfWork;
using Ameer.Talabat.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace Ameer.Talabat.Dashboard
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			builder.Services.AddDbContext<StoreContext>((options) =>
			{
				options
				.UseSqlServer(builder.Configuration.GetConnectionString("StoreContext"));
			});

			builder.Services.AddDbContext<StoreIdentityDbContext>((options) =>
			{
				options
				.UseLazyLoadingProxies()
				.UseSqlServer(builder.Configuration.GetConnectionString("StoreIdentityContext"));
			});


			builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
			{
				options.User.RequireUniqueEmail = true;
			})
			.AddEntityFrameworkStores<StoreIdentityDbContext>();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Auth/Login";
				options.AccessDeniedPath = "/Auth/AccesDenied";
                options.LogoutPath = "/Auth/Login";
            });
			builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
			builder.Services.AddAutoMapper(typeof(MappingProduct),typeof(ProductMVCMapping));
			builder.Services.AddScoped<IPhotoService, PhotoService>();
            builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
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

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Users}/{action=Index}");

			app.Run();
		}
	}
}
