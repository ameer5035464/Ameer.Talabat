namespace Ameer.Talabat.Core.Application
{
	public class UnauthorizedException : Exception
	{
        public UnauthorizedException(string? message):base(message)
        {
            
        }
    }
}
