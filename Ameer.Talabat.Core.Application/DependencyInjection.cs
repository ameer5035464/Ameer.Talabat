using Ameer.Talabat.Core.Application.Abstraction.Models.Auth;
using Ameer.Talabat.Core.Application.Abstraction.Services;
using Ameer.Talabat.Core.Application.Mapping.BasketMapping;
using Ameer.Talabat.Core.Application.Mapping.ProductsMapping;
using Ameer.Talabat.Core.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Ameer.Talabat.Core.Application
{
    public static class DependencyInjection
	{


		public static IServiceCollection AddApplicationServices(this IServiceCollection services,IConfiguration	configuration)
		{

			services.AddAutoMapper(typeof(MappingProduct), typeof(MappingBasket));
			services.AddScoped(typeof(IServiceManager), typeof(ServiceManager));
			services.AddScoped(typeof(IOrderService),(typeof(OrderService)));
			services.AddScoped(typeof(IPaymentService), typeof(PaymentService));
			services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateAudience = true,
						ValidateIssuer = true,
						ValidateIssuerSigningKey = true,
						ValidateLifetime = true,

						ValidIssuer = configuration["JwtSettings:Issuer"],
						ValidAudience = configuration["JwtSettings:Audience"],
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!)),
					};



					options.Events = new JwtBearerEvents
					{
						OnChallenge = context =>
						{
							context.Response.StatusCode = 401;
							context.Response.ContentType = "application/json";
							var response = new { message = "Token is missing or invalid." };
							return context.Response.WriteAsJsonAsync(response);
						}

					};


				});	

			return services;
		}
	}
}