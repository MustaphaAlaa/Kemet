using Entities.Models.DTOs;
using IServices.IColorServices;
using Kemet.Application.Interfaces.Validations;
using Kemet.Application.Utilities;
using Microsoft.Extensions.Logging;

namespace Kemet.Application.Validations;

public class DeleteColorValidation : IDeleteColorValidation
{
    private readonly IRetrieveColor _getColor;
    private readonly ILogger<DeleteColorValidation> _logger;

    public DeleteColorValidation(IRetrieveColor getColor, ILogger<DeleteColorValidation> logger)
    {
        _getColor = getColor;
        _logger = logger;
    }

    public async Task Validate(ColorDeleteDTO entity)
    {

        try
        {
            Utility.IsNull(entity);
            Utility.IdBoundry(entity.ColorId);
        }
        catch (Exception ex)
        {
            string msg = $"An error throwed while validating the creation of the color. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }
}