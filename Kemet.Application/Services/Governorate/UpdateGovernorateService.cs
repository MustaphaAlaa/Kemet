using Application.Exceptions;
using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using IRepository.Generic;
using IServices.IGovernorateServices;
using Kemet.Application.Interfaces.Validations;
using Microsoft.Extensions.Logging;

namespace Application.GovernorateServices;

public class UpdateGovernorateService : IUpdateGovernorate
{
    private readonly IUpdateGovernorateValidation _updateGovernorateValidation;
    private readonly IUpdateAsync<Governorate> _updateGovernorate;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateGovernorateService> _logger;

    public UpdateGovernorateService(IUpdateGovernorateValidation updateGovernorateValidation,
        IUpdateAsync<Governorate> updateGovernorate,
        IMapper mapper,
        ILogger<UpdateGovernorateService> logger)
    {
        _updateGovernorateValidation = updateGovernorateValidation;
        _updateGovernorate = updateGovernorate;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<GovernorateReadDTO> UpdateAsync(GovernorateUpdateDTO entity)
    {
        try
        {
            var existingGovernorate = await _updateGovernorateValidation.Validate(entity);

            existingGovernorate.NameEn = entity.NameEn;
            existingGovernorate.NameAr = entity.NameAr;
            existingGovernorate.IsAvailableToDeliver = entity.IsAvailableToDeliver;

            var governorate = _mapper.Map<Governorate>(existingGovernorate);

            var updatedGovernorate = await _updateGovernorate.UpdateAsync(governorate);

            existingGovernorate = _mapper.Map<GovernorateReadDTO>(updatedGovernorate);
            return existingGovernorate;
        }
        catch (Exception ex)
        {
            var msg = $"An error occurred while updating the governorate. \n{ex.Message}";
            _logger.LogError(msg);
            throw new FailedToUpdateException(msg);
            throw;
        }
    }
}
