

using Application.Exceptions;
using Entities.Models.DTOs;
using IRepository.Generic;
using IServices.ISizeServices;
using Kemet.Application.Interfaces.Validations;
using Microsoft.Extensions.Logging;

namespace Application.SizeServices;
public class DeleteSizeService : IDeleteSize
{
    private readonly IDeleteAsync<SizeDeleteDTO> _deleteRepository;
    private readonly ILogger<DeleteSizeService> _logger;
    private readonly IDeleteSizeValidation _deleteSizeValidation;

    public DeleteSizeService(IDeleteAsync<SizeDeleteDTO> deleteRepository,
        ILogger<DeleteSizeService> logger,
        IDeleteSizeValidation deleteSizeValidation)
    {
        _deleteRepository = deleteRepository;
        _logger = logger;
        _deleteSizeValidation = deleteSizeValidation;
    }

    public async Task<bool> DeleteAsync(SizeDeleteDTO dto)
    {
        try
        {
            await _deleteSizeValidation.Validate(dto);

            return await _deleteRepository.DeleteAsync(Size => Size.SizeId == dto.SizeId) > 0;
        }
        catch (Exception ex)
        {
            string msg = $"An error throwed while deleting the size. {ex.Message}";
            _logger.LogError(msg);
            throw new FailedToDeleteException(msg);
            throw;
        }
    }
}