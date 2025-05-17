using System.Linq.Expressions;
using Application.Exceptions;
using Application.Services;
using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using Entities.Models.Interfaces.Helpers;
using Entities.Models.Interfaces.Validations;
using FluentValidation;
using IRepository.Generic;
using IServices;
using Kemet.Application.Interfaces;
using Kemet.Application.Services;
using Microsoft.Extensions.Logging;

namespace Application;

public class AddressService : GenericService<Address, AddressReadDTO, AddressService>, IAddressService
{
    private readonly IBaseRepository<Address> _repository;
    private readonly IAddressValidation _AddressValidation;
    private readonly IRepositoryRetrieverHelper<Order> _orderRepositoryHelper;

    public AddressService(
        IServiceFacade_DependenceInjection<Address, AddressService> facade,
        IAddressValidation addressValidation,
        IRepositoryRetrieverHelper<Order> orderRepositoryHelper
    )
        : base(facade, "Address")
    {
        _repository = _unitOfWork.GetRepository<Address>();

        _AddressValidation = addressValidation;
        _orderRepositoryHelper = orderRepositoryHelper;
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
        catch (ValidationException ex)
        {
            _logger.LogInformation(ex, $"Validation exception thrown while creating the {TName}.");
            throw;
        }
        catch (DoesNotExistException ex)
        {
            _logger.LogInformation(
                ex,
                $"The user is doesn't exist so {TName} cannot be created fo this user id ."
            );
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An unexpected error occurred while creating the {TName}.");
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
        catch (ValidationException ex)
        {
            _logger.LogInformation(ex, "Validation exception thrown while deleting the customer.");
            throw;
        }
        catch (Exception ex)
        {
            string msg = "An error thrown while deleting the customer.";
            _logger.LogError(ex, msg);
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
        catch (ValidationException ex)
        {
            _logger.LogInformation(
                ex,
                $"Validating Exception is thrown while updating the {TName}."
            );
            throw;
        }
        catch (DoesNotExistException ex)
        {
            _logger.LogInformation(ex, $"{TName} doesn't exist.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error thrown while validating the updating of the {TName}.");
            throw;
        }
    }

    public async Task<AddressReadDTO> GetById(int key)
    {
        return await this.RetrieveByAsync(entity => entity.AddressId == key);
    }

    private async Task<bool> IsAddressUsedInOrders(int addressId)
    {
        var order = await _orderRepositoryHelper.RetrieveByAsync<OrderReadDTO>(o =>
            o.AddressId == addressId
        );
        return order != null;
    }

    public async Task<AddressReadDTO> GetActiveAddressByCustomerId(int customerId)
    {
        var address = await this._repositoryHelper.RetrieveByAsync<AddressReadDTO>(address =>
            address.CustomerId == customerId && address.IsActive == true
        );

        return address;
    }
}
