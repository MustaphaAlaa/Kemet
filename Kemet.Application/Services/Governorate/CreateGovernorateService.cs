using Application.Exceptions;
using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using IRepository.Generic;
using IServices.IGovernorateServices;
using Kemet.Application.Interfaces.Validations;
using Microsoft.Extensions.Logging;

namespace Application.GovernorateServices;


public class CreateGovernorateService : IGovernorateService
{
    private readonly ICreateGovernorateValidation _createGovernorateValidation;
    private readonly ICreateAsync<Governorate> _createGovernorate;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateGovernorateService> _logger;

    public CreateGovernorateService(ICreateGovernorateValidation createGovernorateValidation,
        ICreateAsync<Governorate> createGovernorate,
        IMapper mapper,
        ILogger<CreateGovernorateService> logger)
    {
        _createGovernorateValidation = createGovernorateValidation;
        _createGovernorate = createGovernorate;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<GovernorateReadDTO> CreateAsync(GovernorateCreateDTO entity)
    {
        try
        {
            await _createGovernorateValidation.Validate(entity);

            var newGovernorate = await _createGovernorate.CreateAsync(_mapper.Map<Governorate>(entity));

            return _mapper.Map<GovernorateReadDTO>(newGovernorate);
        }
        catch (Exception ex)
        {
            string msg = $"An error occurred while creating the governorate. \n{ex.Message}";
            _logger.LogError(msg);
            throw new FailedToCreateException(msg);
            throw;
        }
    }
}
