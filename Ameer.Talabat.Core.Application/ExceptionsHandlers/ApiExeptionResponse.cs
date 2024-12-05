using Microsoft.AspNetCore.Mvc;

namespace Ameer.Talabat.Core.Application.ExceptionsHandlers
{
	public class ApiExeptionResponse
	{
		public ApiExeptionResponse(int? statusCode = 0, string? message = null, string? stackTrace = null)
		{
			StatusCode = statusCode;
			Message = message;
		}

		public int? StatusCode { get; set; }
		public string? Message { get; set; }
	}
}
