using Application.Exceptions;
using Entities.Models;
using Entities.Models.DTOs;
using IRepository.Generic;
using IServices.IGovernorateServices;
using Kemet.Application.Interfaces.Validations;
using Microsoft.Extensions.Logging;

namespace Application.GovernorateServices;

public class DeleteGovernorateService : IDeleteGovernorate
{
    private readonly IDeleteGovernorateValidation _deleteGovernorateValidation;
    private readonly IDeleteAsync<Governorate> _deleteGovernorate;
    private readonly ILogger<DeleteGovernorateService> _logger;

    public DeleteGovernorateService(IDeleteGovernorateValidation deleteGovernorateValidation,
        IDeleteAsync<Governorate> deleteGovernorate,
        ILogger<DeleteGovernorateService> logger)
    {
        _deleteGovernorateValidation = deleteGovernorateValidation;
        _deleteGovernorate = deleteGovernorate;
        _logger = logger;
    }

    public async Task<bool> DeleteAsync(GovernorateDeleteDTO entity)
    {
        try
        {
            await _deleteGovernorateValidation.Validate(entity);
            bool isDeleted =  await _deleteGovernorate.DeleteAsync(g => g.GovernorateId == entity.GovernorateId) > 0;
            return isDeleted;
        }
        catch (Exception ex)
        {
            var msg = $"An error occurred while deleting the governorate.  {ex.Message}";
            _logger.LogError(msg);
            throw new FailedToDeleteException(msg);
            throw;
        }
    }
}
