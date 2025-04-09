using Entities.Models.DTOs;
using IServices.ISizeServices;
using Kemet.Application.Interfaces.Validations;
using Kemet.Application.Utilities;
using Microsoft.Extensions.Logging;



namespace Kemet.Application.Validations;

public class DeleteSizeValidation : IDeleteSizeValidation
{
    private readonly IRetrieveSize _getSize;
    private readonly ILogger<DeleteSizeValidation> _logger;

    public DeleteSizeValidation(IRetrieveSize getSize, ILogger<DeleteSizeValidation> logger)
    {
        _getSize = getSize;
        _logger = logger;
    }

    public async Task Validate(SizeDeleteDTO entity)
    {
        try
        {
            Utility.IsNull(entity);
            Utility.IdBoundry(entity.SizeId);
        }
        catch (Exception ex)
        {
            string msg = $"An error throwed while validating the deleting of the size. {ex.Message}";
            _logger.LogError(msg);
            throw;

        }
    }
}
