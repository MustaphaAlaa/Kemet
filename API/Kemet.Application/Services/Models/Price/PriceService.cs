using Application.Exceptions;
using Entities.Models;
using Entities.Models.DTOs;
using Entities.Models.Interfaces.Validations;
using IRepository.Generic;
using IServices;
using Kemet.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class PriceService : GenericService<Price, PriceReadDTO, PriceService>, IPriceService
{
    private readonly IBaseRepository<Price> _repository;
    private readonly IPriceValidation _PriceValidation;

    public PriceService(
        IServiceFacade_DependenceInjection<Price, PriceService> facade,
        IPriceValidation PriceValidation
    )
        : base(facade, "UnitPrice")
    {
        _PriceValidation = PriceValidation;
        _repository = _unitOfWork.GetRepository<Price>();
    }

    public async Task<PriceReadDTO> DeactivatePrice(PriceReadDTO priceToDeactivate)
    {
        try
        {
            var price = _mapper.Map<Price>(priceToDeactivate);
            price.IsActive = false;
            price.ProductId = priceToDeactivate.ProductId;
            var UpdatedPrice = _repository.Update(price);

            var pr = _mapper.Map<PriceReadDTO>(UpdatedPrice);
            return await Task.FromResult(pr);
        }
        catch (Exception ex)
        {
            string msg = $"An error occurred while creating the {TName}. \n{ex.Message}";
            _logger.LogError(msg);
            // throw new FailedToCreateException(msg);
            throw;
        }
    }

    public async Task<PriceReadDTO> CreateAsync(PriceCreateDTO entity)
    {
        try
        {
            await _PriceValidation.ValidateCreate(entity);

            var price = _mapper.Map<Price>(entity);

            price.CreatedAt = DateTime.UtcNow;
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

            //await _repository.DeleteAsync(p => p.PriceId == entity.PriceId);

            //var price =  await _repository.RetrieveAsync(price => price.PriceId == entity.PriceId);

            //price.IsActive = false;
            //var priceDelete = _mapper.Map<UnitPrice>(price);
            _repository.Update(new Price { PriceId = entity.PriceId, IsActive = false });
        }
        catch (Exception ex)
        {
            var msg = $"An error occurred while deleting the {TName}.  {ex.Message}";
            _logger.LogError(msg);
            //throw new FailedToDeleteException(msg);
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

            //var PriceToUpdate = _mapper.Map<UnitPrice>(updateRequest);
            _repository.Update(new Price { PriceId = updateRequest.PriceId, IsActive = false });
            var newPrice = _mapper.Map<Price>(updateRequest);

            return await this.CreateAsync(_mapper.Map<PriceCreateDTO>(newPrice));
        }
        catch (Exception ex)
        {
            var msg = $"An error occurred while updating the {TName}. \n{ex.Message}";
            _logger.LogError(msg);
            //throw new FailedToUpdateException(msg);
            throw;
        }
    }

    public bool AreRangesEquals(int MaximumPrice, int MinimumPrice) => MaximumPrice == MinimumPrice;

    public async Task<PriceReadDTO> UpdateNote(PriceUpdateDTO updateRequest)
    {
        try
        {
            await _PriceValidation.ValidateUpdate(updateRequest);
            var price = await _repositoryHelper.RetrieveByAsync<PriceReadDTO>(p =>
                p.PriceId == updateRequest.PriceId
            );

            if (
                updateRequest.MinimumPrice != price.MinimumPrice
                && updateRequest.MaximumPrice != price.MaximumPrice
            )
            {
                throw new Exception(
                    "the price range doesn't equal the range recorded in the database"
                );
                return null;
            }

            var PriceToUpdate = _mapper.Map<Price>(updateRequest);

            var UpdatedPrice = _repository.Update(PriceToUpdate);
            var newPrice = _mapper.Map<PriceReadDTO>(UpdatedPrice);
            return newPrice;
            // return await this.CreateAsync(_mapper.Map<PriceCreateDTO>(newPrice));
        }
        catch (Exception ex)
        {
            var msg = $"An error occurred while updating the {TName}. \n{ex.Message}";
            _logger.LogError(msg);
            //throw new FailedToUpdateException(msg);
            throw;
        }
    }
}
