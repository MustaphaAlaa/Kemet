namespace Application.Exceptions;

public class InvalidPriceException : System.Exception
{
    public InvalidPriceException()
    {
    }

    public InvalidPriceException(string? message) : base(message)
    {
    }

    public InvalidPriceException(string? message, System.Exception? innerException) : base(message, innerException)
    {
    }
}
