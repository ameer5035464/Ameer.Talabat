using Ameer.Talabat.Core.Application.ExceptionsHandlers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ameer.Talabat.Core.Application
{
	public class ExceptionCustomFilter : IExceptionFilter
	{

		public void OnException(ExceptionContext context)
		{

			var statusCode = context.Exception switch
			{
				NotFoundException => StatusCodes.Status404NotFound,
				UnauthorizedException => StatusCodes.Status401Unauthorized,
				UnsupportedMediaTypException => StatusCodes.Status415UnsupportedMediaType,
				ConflictException => StatusCodes.Status409Conflict,
				CustomExceptionError => StatusCodes.Status400BadRequest,
				_ => StatusCodes.Status500InternalServerError
			};


			context.Result =
			new ObjectResult(new ApiExeptionResponse()
			{
				Message = context.Exception.Message,
				StatusCode = statusCode
			})
			{
				StatusCode = statusCode
			};


		}
	}
}
