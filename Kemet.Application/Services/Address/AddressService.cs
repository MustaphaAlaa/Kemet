using System.Linq.Expressions;
using Application.Exceptions;
using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using Entities.Models.Interfaces.Helpers;
using Entities.Models.Interfaces.Validations;
using FluentValidation;
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

    #region Create
    private async Task<AddressReadDTO> CreateCore(AddressCreateDTO entity)
    {
        await _AddressValidation.ValidateCreate(entity);

        var address = _mapper.Map<Address>(entity);

        address.CreatedAt = DateTime.Now;

        address = await _repository.CreateAsync(address);

        return _mapper.Map<AddressReadDTO>(address);
    }

    public async Task<AddressReadDTO> CreateInternalAsync(AddressCreateDTO entity)
    {
        try
        {
            var address = await CreateCore(entity);
            await _unitOfWork.SaveChangesAsync();
            return address;
        }
        catch (ValidationException ex)
        {
            _logger.LogInformation(ex, "Validation exception thrown while creating the address.");
            throw;
        }
        catch (DoesNotExistException ex)
        {
            _logger.LogInformation(
                ex,
                "The user is doesn't exist so address cannot be created fo this user id ."
            );
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while creating the address.");
            throw;
        }
    }

    public async Task<AddressReadDTO> CreateAsync(AddressCreateDTO entity)
    {
        try
        {
            var address = await CreateCore(entity);
            return address;
        }
        catch (ValidationException ex)
        {
            _logger.LogInformation(ex, "Validation exception thrown while creating the address.");
            throw;
        }
        catch (DoesNotExistException ex)
        {
            _logger.LogInformation(
                ex,
                "The user is doesn't exist so address cannot be created fo this user id ."
            );
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while creating the address.");
            throw;
        }
    }

    #endregion




    #region Delete


    public async Task DeleteCore(AddressDeleteDTO entity)
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
            await DeleteCore(entity);
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
            await DeleteCore(entity);

            return await _unitOfWork.SaveChangesAsync() > 0;
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

    #endregion




    #region  Update


    public async Task<AddressReadDTO> UpdateCore(AddressUpdateDTO updateRequest)
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
            var address = await UpdateCore(updateRequest);

            await _unitOfWork.SaveChangesAsync();

            return address;
        }
        catch (ValidationException ex)
        {
            _logger.LogInformation(
                ex,
                "Validating Exception is thrown while updating the address."
            );
            throw;
        }
        catch (DoesNotExistException ex)
        {
            _logger.LogInformation(ex, "address doesn't exist.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error thrown while validating the updating of the address.");
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
            var address = await UpdateCore(updateRequest);
            return address;
        }
        catch (ValidationException ex)
        {
            _logger.LogInformation(
                ex,
                "Validating Exception is thrown while updating the address."
            );
            throw;
        }
        catch (DoesNotExistException ex)
        {
            _logger.LogInformation(ex, "address doesn't exist.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error thrown while validating the updating of the address.");
            throw;
        }
    }

    #endregion




    #region  Retrieve
    public async Task<List<AddressReadDTO>> RetrieveAllAsync()
    {
        try
        {
            return await _repositoryHelper.RetrieveAllAsync<AddressReadDTO>();
        }
        catch (Exception ex)
        {
            string msg =
                $"Unexpected exception throws while retrieving address records. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task<IEnumerable<AddressReadDTO>> RetrieveAllAsync(
        Expression<Func<Address, bool>> predicate
    )
    {
        try
        {
            return await _repositoryHelper.RetrieveAllAsync<AddressReadDTO>(predicate);
        }
        catch (Exception ex)
        {
            string msg =
                $"Unexpected exception throws while retrieving address records. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task<AddressReadDTO> RetrieveByAsync(Expression<Func<Address, bool>> predicate)
    {
        try
        {
            return await _repositoryHelper.RetrieveByAsync<AddressReadDTO>(predicate);
        }
        catch (Exception ex)
        {
            string msg =
                $"Unexpected exception throws while retrieving the address record. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task<AddressReadDTO> GetById(int key)
    {
        return await this.RetrieveByAsync(entity => entity.AddressId == key);

    }
    #endregion

    private async Task<bool> IsAddressUsedInOrders(int addressId)
    {
        var order = await _orderRepositoryHelper.RetrieveByAsync<OrderReadDTO>(o =>
            o.AddressId == addressId
        );
        return order != null;
    }


}
