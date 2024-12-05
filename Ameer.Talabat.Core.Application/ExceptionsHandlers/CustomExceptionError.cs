namespace Ameer.Talabat.Core.Application.ExceptionsHandlers
{
    public class CustomExceptionError : Exception
    {
        public string[]? Errors { get; set; }
        public CustomExceptionError(string? message) : base(message)
        {
            Errors = [];
        }
    }
}
