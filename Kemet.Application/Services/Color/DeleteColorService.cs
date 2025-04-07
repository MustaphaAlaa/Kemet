

using Application.Exceptions;
using Entities.Models.DTOs;
using IRepository.Generic;
using IServices.IColorServices;
using Kemet.Application.Interfaces.Validations;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;

namespace Application.ColorServices;

public class DeleteColorService : IDeleteColor
{

    private readonly IDeleteAsync<ColorDeleteDTO> _deleteRepository;
    private readonly ILogger<DeleteColorService> _logger;
    private readonly IDeleteColorValidation _deleteColorValidation;

    public DeleteColorService(IDeleteAsync<ColorDeleteDTO> deleteRepository,
        ILogger<DeleteColorService> logger,
        IDeleteColorValidation deleteColorValidation)
    {
        _deleteRepository = deleteRepository;
        _logger = logger;
        _deleteColorValidation = deleteColorValidation;
    }

    public async Task<bool> DeleteAsync(ColorDeleteDTO dto)
    {

        try
        {

            await _deleteColorValidation.Validate(dto);
            return await _deleteRepository.DeleteAsync(Color => Color.ColorId == dto.ColorId) > 0;

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