namespace Application.Exceptions;

public class NotAvailableException : System.Exception
{
    public NotAvailableException()
    {
    }

    public NotAvailableException(string? message) : base(message)
    {
    }

    public NotAvailableException(string? message, System.Exception? innerException) : base(message, innerException)
    {
    }
}