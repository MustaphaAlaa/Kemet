namespace Application.Exceptions;

public class FailedToDeleteException : System.Exception
{
    public FailedToDeleteException()
    {
    }

    public FailedToDeleteException(string? message) : base(message)
    {
    }

    public FailedToDeleteException(string? message, System.Exception? innerException) : base(message, innerException)
    {
    }
}