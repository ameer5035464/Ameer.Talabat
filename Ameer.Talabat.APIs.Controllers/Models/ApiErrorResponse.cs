using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ameer.Talabat.APIs.Controllers.Models
{
	public class ApiErrorResponse
	{
        public int StatusCode { get; set; }
        public string? Message { get; set; }
		public Dictionary<string, string[]>? Errors { get; set; }
		public ApiErrorResponse(int status = 400,string? message = "Bad Request")
        {
			StatusCode = status;
			Message = message;
			Errors = [];
		}
    }
}
