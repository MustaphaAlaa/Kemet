using System.Linq.Expressions;
using Application.Exceptions;
using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using Entities.Models.Interfaces.Helpers;
using Entities.Models.Interfaces.Validations;
using IRepository.Generic;
using IServices;
using Kemet.Application.Services;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class PriceService : GenericService<Price, PriceReadDTO>, IPriceService
{
    private readonly IBaseRepository<Price> _repository;
    private readonly IPriceValidation _PriceValidation;


    public PriceService(IPriceValidation PriceValidation,
                        ServiceFacade_DependenceInjection<Price> facade)
        : base(facade, "Price")
    {
        _PriceValidation = PriceValidation;
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
            string msg = $"An error occurred while creating the {TName}. \n{ex.Message}";
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
            var msg = $"An error occurred while deleting the {TName}.  {ex.Message}";
            _logger.LogError(msg);
            throw new FailedToDeleteException(msg);
            throw;
        }
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
            var msg = $"An error occurred while updating the {TName}. \n{ex.Message}";
            _logger.LogError(msg);
            throw new FailedToUpdateException(msg);
            throw;
        }
    }
}
