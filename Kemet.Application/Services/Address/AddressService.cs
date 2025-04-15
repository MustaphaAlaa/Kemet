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

namespace Application;

public class AddressService : IAddressService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBaseRepository<Address> _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<AddressService> _logger;
    private readonly IAddressValidation _AddressValidation;
    private readonly IRepositoryRetrieverHelper<Address> _repositoryHelper;
    private readonly IRepositoryRetrieverHelper<Order> _orderRepositoryHelper;

    public AddressService(
        IUnitOfWork unitOfWork,
        IBaseRepository<Address> repository,
        IMapper mapper,
        ILogger<AddressService> logger,
        IAddressValidation addressValidation,
        IRepositoryRetrieverHelper<Address> repositoryHelper,
        IRepositoryRetrieverHelper<Order> orderRepositoryHelper
    )
    {
        _unitOfWork = unitOfWork;
        _repository = _unitOfWork.GetRepository<Address>();
        _mapper = mapper;
        _logger = logger;
        _AddressValidation = addressValidation;
        _repositoryHelper = repositoryHelper;
        _orderRepositoryHelper = orderRepositoryHelper;
    }

    public async Task<AddressReadDTO> CreateInternalAsync(AddressCreateDTO entity)
    {
        try
        {
            var AddressDto = await this.CreateAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return AddressDto;
        }
        catch (Exception ex)
        {
            string msg = $"An error occurred while creating the Address. \n{ex.Message}";
            _logger.LogError(msg);
            throw new FailedToCreateException(msg);
            throw;
        }
    }

    public async Task<AddressReadDTO> CreateAsync(AddressCreateDTO entity)
    {
        try
        {
            await _AddressValidation.ValidateCreate(entity);

            var address = _mapper.Map<Address>(entity);

            address.CreatedAt = DateTime.Now;

            address = await _repository.CreateAsync(address);

            return _mapper.Map<AddressReadDTO>(address);
        }
        catch (Exception ex)
        {
            string msg = $"An error occurred while creating the Address. \n{ex.Message}";
            _logger.LogError(msg);
            throw new FailedToCreateException(msg);
            throw;
        }
    }

    /// <summary>
    /// this method will checks if address is used in order, if it used, the record will be soft deleted,
    ///if not used in any order it'll be deleted.
    /// this method don't save changes into the database use it if you're using Unit of work patten
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>void</returns>
    /// <exception cref="FailedToDeleteException"></exception>
    public async Task DeleteAsync(AddressDeleteDTO entity)
    {
        try
        {
            await _AddressValidation.ValidateDelete(entity);

            var isAddressUsedInOrders = await IsAddressUsedInOrders(entity.AddressId);

            if (isAddressUsedInOrders)
            {
                var address = await _repository.RetrieveAsync(address =>
                    address.AddressId == entity.AddressId
                );
                address.IsActive = false;
                _repository.Update(address);
            }
            else
                await _repository.DeleteAsync(g => g.AddressId == entity.AddressId);
        }
        catch (Exception ex)
        {
            var msg = $"An error occurred while deleting the Address.  {ex.Message}";
            _logger.LogError(msg);
            throw new FailedToDeleteException(msg);
            throw;
        }
    }

    /// <summary>
    /// this method will checks if address is used in order, if it used, the record will be soft deleted,
    ///if not used in any order it'll be deleted.
    /// this method save changes into the database directly.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>void</returns>
    /// <exception cref="FailedToDeleteException"></exception>
    public async Task<bool> DeleteInternalAsync(AddressDeleteDTO entity)
    {
        try
        {
            await DeleteAsync(entity);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }
        catch (Exception ex)
        {
            var msg = $"An error occurred while deleting the Address.  {ex.Message}";
            _logger.LogError(msg);
            throw new FailedToDeleteException(msg);
            throw;
        }
    }

    /// <summary>
    /// this method will checks if address is used in order, if it used, the record will be marked as inactive,
    /// if not used in any order it'll be updated.
    /// This method save changes into the database directly.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>void</returns>
    /// <exception cref="FailedToDeleteException"></exception> 
    public async Task<AddressReadDTO> UpdateInternalAsync(AddressUpdateDTO updateRequest)
    {
        try
        {
            var address = await this.Update(updateRequest);

            await _unitOfWork.SaveChangesAsync();

            return address;
        }
        catch (Exception ex)
        {
            var msg = $"An error occurred while updating the Address. \n{ex.Message}";
            _logger.LogError(msg);
            throw new FailedToUpdateException(msg);
            throw;
        }
    }

    /// <summary>
    /// this method will checks if address is used in order, if it used, the record will be marked as inactive,
    /// if not used in any order it'll be updated.
    /// this method don't save changes into the database use it if you're using Unit of work patten
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>void</returns>
    /// <exception cref="FailedToDeleteException"></exception>
    public async Task<AddressReadDTO> Update(AddressUpdateDTO updateRequest)
    {
        try
        {
            await _AddressValidation.ValidateUpdate(updateRequest);

            var address = await _repository.RetrieveAsync(address =>
                address.AddressId == updateRequest.AddressId
            );

            bool IsAddressUsedInOrders = await this.IsAddressUsedInOrders(updateRequest.AddressId);

            if (IsAddressUsedInOrders)
            {
                address.IsActive = false;
                _repository.Update(address);
                var createAddressDTO = _mapper.Map<AddressCreateDTO>(updateRequest);
                return await this.CreateAsync(createAddressDTO);
            }
            else
            {
                _mapper.Map(updateRequest, address);
                _repository.Update(address);
                return _mapper.Map<AddressReadDTO>(address);
            }
        }
        catch (Exception ex)
        {
            var msg = $"An error occurred while updating the Address. \n{ex.Message}";
            _logger.LogError(msg);
            throw new FailedToUpdateException(msg);
            throw;
        }
    }

     

    private async Task<bool> IsAddressUsedInOrders(int addressId)
    {
        var order = await _orderRepositoryHelper.RetrieveByAsync<OrderReadDTO>(o =>
            o.AddressId == addressId
        );
        return order != null;
    }

    public async Task<List<AddressReadDTO>> RetrieveAllAsync()
    {
        return await _repositoryHelper.RetrieveAllAsync<AddressReadDTO>();
    }

    public async Task<IEnumerable<AddressReadDTO>> RetrieveAllAsync(
        Expression<Func<Address, bool>> predicate
    )
    {
        return await _repositoryHelper.RetrieveAllAsync<AddressReadDTO>(predicate);
    }

    public async Task<AddressReadDTO> RetrieveByAsync(Expression<Func<Address, bool>> predicate)
    {
        return await _repositoryHelper.RetrieveByAsync<AddressReadDTO>(predicate);
    }
}
