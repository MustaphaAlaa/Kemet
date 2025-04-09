using Entities.Models.DTOs;
using IServices.IColorServices;
using Kemet.Application.Interfaces.Validations;
using Kemet.Application.Utilities;
using Microsoft.Extensions.Logging;

namespace Kemet.Application.Validations;

public class UpdateColorValidation : IUpdateColorValidation
{
    private readonly IRetrieveColor _getColor;
    private readonly ILogger<UpdateColorValidation> _logger;

    public UpdateColorValidation(IRetrieveColor getColor, ILogger<UpdateColorValidation> logger)
    {
        _getColor = getColor;
        _logger = logger;
    }

    public async Task<ColorReadDTO> Validate(ColorUpdateDTO entity)
    {
        try
        {
            Utility.IsNull(entity);

            Utility.IdBoundry(entity.ColorId);

            Utility.IsNullOrEmpty(entity.NameEn, "Color's English name");
            Utility.IsNullOrEmpty(entity.NameAr, "Color's Arabic name");
            Utility.IsNullOrEmpty(entity.Hexacode, "Hexacode");

            entity.NameAr = entity.NameAr?.Trim().ToLower();
            entity.NameEn = entity.NameEn?.Trim().ToLower();
            entity.Hexacode = entity.Hexacode?.Trim().ToLower();

            var Color = await _getColor.GetByAsync(c => c.ColorId == entity.ColorId);

            Utility.DoesExist(Color, "Color");

            return Color;
        }
        catch (Exception ex)
        {
            string msg = $"An error throwed while validating the updation of the color. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }
}
