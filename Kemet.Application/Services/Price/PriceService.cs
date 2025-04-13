using System.Linq.Expressions;
using Application.Exceptions;
using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using IRepository.Generic;
using IServices;
using Kemet.Application.Interfaces.Helpers;
using Kemet.Application.Interfaces.Validations;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class PriceService : IPriceService
{
    private readonly IBaseRepository<Price> _repository;
    private readonly IPriceValidation _PriceValidation;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<PriceService> _logger;
    private readonly IRepositoryRetrieverHelper<Price> _repositoryHelper;

    public PriceService(
        IPriceValidation PriceValidation,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<PriceService> logger,
        IRepositoryRetrieverHelper<Price> repoHelper
    )
    {
        _PriceValidation = PriceValidation;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
        _repositoryHelper = repoHelper;
        _repository = _unitOfWork.GetRepository<Price>();
    }

    public async Task<PriceReadDTO> CreateInternalAsync(PriceCreateDTO entity)
    {
        try
        {
            var PriceReadDto = await this.CreateAsync(entity);

            await _unitOfWork.SaveChangesAsync();

            return PriceReadDto;
        }
        catch (Exception ex)
        {
            string msg = $"An error occurred while creating the Price. \n{ex.Message}";
            _logger.LogError(msg);
            throw new FailedToCreateException(msg);
            throw;
        }
    }

    public async Task<PriceReadDTO> CreateAsync(PriceCreateDTO entity)
    {
        try
        {
            await _PriceValidation.ValidateCreate(entity);

            var price = _mapper.Map<Price>(entity);

            price.CreatedAt = DateTime.Now;
            price.IsActive = true;

            price = await _repository.CreateAsync(price);

            var newPrice = _mapper.Map<PriceReadDTO>(price);

            return newPrice;
        }
        catch (Exception ex)
        {
            string msg = $"An error occurred while creating the Price. \n{ex.Message}";
            _logger.LogError(msg);
            throw new FailedToCreateException(msg);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(PriceDeleteDTO entity)
    {
        try
        {
            await _PriceValidation.ValidateDelete(entity);

            await _repository.DeleteAsync(p => p.PriceId == entity.PriceId);

            bool isDeleted = await _unitOfWork.SaveChangesAsync() > 0;

            return isDeleted;
        }
        catch (Exception ex)
        {
            var msg = $"An error occurred while deleting the Price.  {ex.Message}";
            _logger.LogError(msg);
            throw new FailedToDeleteException(msg);
            throw;
        }
    }

    public async Task<List<PriceReadDTO>> RetrieveAllAsync()
    {
        return await _repositoryHelper.RetrieveAllAsync<PriceReadDTO>();
    }

    public async Task<IEnumerable<PriceReadDTO>> RetrieveAllAsync(
        Expression<Func<Price, bool>> predicate
    )
    {
        return await _repositoryHelper.RetrieveAllAsync<PriceReadDTO>(predicate);
    }

    public async Task<PriceReadDTO> RetrieveByAsync(Expression<Func<Price, bool>> predicate)
    {
        return await _repositoryHelper.RetrieveByAsync<PriceReadDTO>(predicate);
    }

    public async Task<PriceReadDTO> UpdateInternalAsync(PriceUpdateDTO updateRequest)
    {
        try
        {
            var updatedDto = await this.Update(updateRequest);

            await _unitOfWork.SaveChangesAsync();

            return updatedDto;
        }
        catch (Exception ex)
        {
            var msg = $"An error occurred while updating the Price. \n{ex.Message}";
            _logger.LogError(msg);
            throw new FailedToUpdateException(msg);
            throw;
        }
    }

    public async Task<PriceReadDTO> Update(PriceUpdateDTO updateRequest)
    {
        try
        {
            await _PriceValidation.ValidateUpdate(updateRequest);

            var PriceToUpdate = _mapper.Map<Price>(updateRequest);

            var newPrice = _mapper.Map<Price>(updateRequest);

            return await this.CreateAsync(_mapper.Map<PriceCreateDTO>(newPrice));
        }
        catch (Exception ex)
        {
            var msg = $"An error occurred while updating the Price. \n{ex.Message}";
            _logger.LogError(msg);
            throw new FailedToUpdateException(msg);
            throw;
        }
    }
}
