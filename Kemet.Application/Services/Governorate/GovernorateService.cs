using System.Linq.Expressions;
using Application.Exceptions;
using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using IRepository.Generic;
using IServices;
using Kemet.Application.Interfaces.Validations;
using Microsoft.Extensions.Logging;

namespace Application;

public class GovernorateService : IGovernorateService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBaseRepository<Governorate> _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GovernorateService> _logger;
    private readonly IGovernorateValidation _governorateValidation;

    public GovernorateService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<GovernorateService> logger,
        IGovernorateValidation governorateValidation
    )
    {
        _unitOfWork = unitOfWork;
        _repository = _unitOfWork.GetRepository<Governorate>();

        _mapper = mapper;
        _logger = logger;
        _governorateValidation = governorateValidation;
    }

    public async Task<GovernorateReadDTO> CreateAsync(GovernorateCreateDTO entity)
    {
        try
        {
            await _governorateValidation.ValidateCreate(entity);
            var governorate = _mapper.Map<Governorate>(entity);
            governorate = await _repository.CreateAsync(governorate);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<GovernorateReadDTO>(governorate);
        }
        catch (Exception ex)
        {
            string msg = $"An error occurred while creating the governorate. \n{ex.Message}";
            _logger.LogError(msg);
            throw new FailedToCreateException(msg);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(GovernorateDeleteDTO entity)
    {
        try
        {
            await _governorateValidation.ValidateDelete(entity);
            await _repository.DeleteAsync(g => g.GovernorateId == entity.GovernorateId);
            var isDeleted = await _unitOfWork.CompleteAsync() > 0;
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

    public Task<List<GovernorateReadDTO>> RetrieveAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<GovernorateReadDTO>> RetrieveAllAsync(
        Expression<Func<Governorate, bool>> predicate
    )
    {
        throw new NotImplementedException();
    }

    public Task<GovernorateReadDTO> RetrieveByAsync(Expression<Func<Governorate, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public async Task<GovernorateReadDTO> UpdateAsync(GovernorateUpdateDTO updateRequest)
    {
        try
        {
            await _governorateValidation.ValidateUpdate(updateRequest);

            var governorate = _mapper.Map<Governorate>(updateRequest);

            var updatedGovernorate = _repository.Update(governorate);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<GovernorateReadDTO>(governorate);
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
