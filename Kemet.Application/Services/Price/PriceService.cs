using System.Linq.Expressions;
using Application.Exceptions;
using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using Entities.Models.Interfaces.Helpers;
using Entities.Models.Interfaces.Validations;
using IRepository.Generic;
using IServices;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class PriceService : SaveService, IPriceService
{
    private readonly IBaseRepository<Price> _repository;
    private readonly IPriceValidation _PriceValidation;
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
        : base(unitOfWork)
    {
        _PriceValidation = PriceValidation;
        _mapper = mapper;
        _logger = logger;
        _repositoryHelper = repoHelper;
        _repository = _unitOfWork.GetRepository<Price>();
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

    public async Task DeleteAsync(PriceDeleteDTO entity)
    {
        try
        {
            await _PriceValidation.ValidateDelete(entity);

            await _repository.DeleteAsync(p => p.PriceId == entity.PriceId);
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

    public async Task<PriceReadDTO> GetById(int key)
    {
        return await this.RetrieveByAsync(entity => entity.PriceId == key);
    }

    public async Task<PriceReadDTO> ProductActivePrice(int ProductId)
    {
        return await this.RetrieveByAsync(entity =>
            entity.ProductId == ProductId && entity.IsActive == true
        );
    }

    public async Task<PriceReadDTO> Update(PriceUpdateDTO updateRequest)
    {
        try
        {
            await _PriceValidation.ValidateUpdate(updateRequest);

            var PriceToUpdate = _mapper.Map<Price>(updateRequest);
            _repository.Update(new Price { PriceId = updateRequest.PriceId, IsActive = false });
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
