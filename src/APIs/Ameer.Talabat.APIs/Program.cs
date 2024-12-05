using Ameer.Talabat.APIs.Controllers;
using Ameer.Talabat.APIs.Controllers.Models;
using Ameer.Talabat.APIs.Extensions;
using Ameer.Talabat.APIs.Services;
using Ameer.Talabat.Core.Application;
using Ameer.Talabat.Core.Application.Abstraction;
using Ameer.Talabat.Core.Domain.Entities.Identity;
using Ameer.Talabat.Infrastructure;
using Ameer.Talabat.Infrastructure.Helpers;
using Ameer.Talabat.Infrastructure.Persistance;
using Ameer.Talabat.Infrastructure.Persistance._Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Ameer.Talabat.APIs
{
    public class Program
    {

        public static async Task Main(string[] args)
        {
            #region Configures
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddControllers(options =>
            {
                options.Filters.Add(new ExceptionCustomFilter());
            })
                .AddApplicationPart(typeof(AssemblyRef).Assembly);

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<StoreIdentityDbContext>();

            builder.Services.AddApplicationServices(builder.Configuration);

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Where(e => e.Value!.Errors.Count > 0)
                          .ToDictionary(
                          e => e.Key,
                          e => e.Value!.Errors.Select(err => err.ErrorMessage).ToArray()
                        );

                    var response = new ApiErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(response);
                };
            });

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddPersistenceServices(builder.Configuration);

            builder.Services.AddScoped(typeof(ILoggedInUserService), typeof(LoggedInUserService));

            builder.Services.AddInfrastructureServices(builder.Configuration);

            builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

            builder.Services.AddCors(Options =>
            {
                Options.AddPolicy("MyPolicy", options =>
                {
                    options.AllowAnyHeader();
                    options.AllowAnyMethod();
                    options.AllowAnyOrigin();
                });
            });
            #endregion

            var app = builder.Build();

            #region Databases Intialization
            await app.IntializeDB();
            #endregion

            #region Configure Kestrell

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCors("MyPolicy");
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
