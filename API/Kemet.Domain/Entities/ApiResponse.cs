using System.Net;

namespace Entities;

public class APIResponse
{
    public APIResponse()
    {
        ErrorMessages = new();
    }

    public HttpStatusCode StatusCode { get; set; }

    public bool IsSuccess { get; set; }

    public List<string> ErrorMessages { get; set; }

    public object Result { get; set; }
}
