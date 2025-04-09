using Entities.Models.DTOs;
using IServices.ISizeServices;
using Kemet.Application.Interfaces.Validations;
using Kemet.Application.Utilities;
using Microsoft.Extensions.Logging;



namespace Kemet.Application.Validations;

public class UpdateSizeValidation : IUpdateSizeValidation
{
    private readonly IRetrieveSize _getSize;
    private readonly ILogger<UpdateSizeValidation> _logger;

    public UpdateSizeValidation(IRetrieveSize getSize, ILogger<UpdateSizeValidation> logger)
    {
        _getSize = getSize;
        _logger = logger;
    }

    public async Task<SizeReadDTO> Validate(SizeUpdateDTO entity)
    {
        try
        {
            Utility.IsNull(entity);
            Utility.IdBoundry(entity.SizeId);
            Utility.IsNullOrEmpty(entity.Name, "Size");

            entity.Name = entity.Name?.Trim().ToLower();

            var Size = await _getSize.GetByAsync(c => c.SizeId == entity.SizeId);

            Utility.DoesExist(Size);

            return Size;
        }
        catch (Exception ex)
        {
            string msg = $"An error throwed while validating the updation of the size. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }
}
