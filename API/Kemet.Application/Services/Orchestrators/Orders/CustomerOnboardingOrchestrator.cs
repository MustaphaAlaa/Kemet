using System.Net.Security;
using System.Runtime.CompilerServices;
using Application.Exceptions;
using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using Entities.Models.DTOs.Orchestrates;
using IRepository.Generic;
using IServices;
using IServices.Orchestrator;
using Microsoft.Extensions.Logging;

namespace Application.Services.Orchestrator;

/// <summary>
/// Orchestrates the onboarding of a customer and their address for order creation.
/// </summary>
public class CustomerOnboardingOrchestrator : ICustomerOnboardingOrchestrator
{
    private readonly ICustomerService _customerService;
    private readonly IAddressService _addressService;
    private readonly ILogger<CustomerOnboardingOrchestrator> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public CustomerOnboardingOrchestrator(
        ICustomerService customerService,
        IAddressService addressService,
        ILogger<CustomerOnboardingOrchestrator> logger,
        IUnitOfWork unitOfWork
    )
    {
        _customerService = customerService;
        _addressService = addressService;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Ensures a customer exists by phone number, or creates a new one if not found.
    /// </summary>
    private async Task<Guid> EnsureCustomerExistsAsync(
        CreatingOrderForAnonymousCustomerRequest request
    )
    {
        Guid customerId = Guid.Empty;

        var existingCustomer = await _customerService.FindCustomerByPhoneNumberAsync(
            request.PhoneNumber
        );

        if (existingCustomer != null)
        {
            customerId = existingCustomer.CustomerId;
        }
        else
        {
            var customerDTO = new CustomerCreateDTO
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
            };
            var createdCustomer = await _customerService.CreateWithTrackingAsync(customerDTO);

            await _unitOfWork.SaveChangesAsync();

            customerId = createdCustomer.CustomerId;
        }
        return customerId;
    }

    /// <summary>
    /// Creates a new address for the specified customer.
    /// </summary>
    private async Task<Address> CreateCustomerAddressAsync(
        CreatingOrderForAnonymousCustomerRequest request,
        Guid customerId
    )
    {
        var addressDTO = new AddressCreateDTO
        {
            StreetAddress = request.StreetAddress!,
            GovernorateId = request.GovernorateId,
            CustomerId = customerId,
        };
        var createdAddress = await _addressService.CreateWithTrackingAsync(addressDTO);
        await _unitOfWork.SaveChangesAsync();
        return createdAddress;
    }

    /// <summary>
    /// Ensures both customer and address exist for order creation.
    /// </summary>
    public async Task<CustomerAndAddressResult> EnsureCustomerOnboardingAsync(
        CreatingOrderForAnonymousCustomerRequest request
    )
    {
        try
        {
            _logger.LogInformation(
                "CustomerOnboardingOrchestrator => EnsureCustomerOnboardingAsync called"
            );
            // await _unitOfWork.BeginTransactionAsync();
            var customerAndAddressResult = new CustomerAndAddressResult();
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Request cannot be null");
            }

            // Ensure the customer exists or create a new one
            var customerId = await EnsureCustomerExistsAsync(request);
            customerAndAddressResult.CustomerId = customerId;

            AddressReadDTO oldAddress = null;
            if (request.SameLastAddress)
            {
                oldAddress = await _addressService.GetActiveAddressByCustomerId(customerId);
                customerAndAddressResult.AddressId = oldAddress?.AddressId ?? 0;
            }

            if (oldAddress == null && string.IsNullOrEmpty(request.StreetAddress))
            {
                throw new ArgumentException(
                    "No previous address found and no new address provided."
                );
            }
            else if (oldAddress == null && !string.IsNullOrEmpty(request.StreetAddress))
            {
                var newAddress = await CreateCustomerAddressAsync(request, customerId);
                customerAndAddressResult.AddressId = oldAddress?.AddressId ?? 0;
            }
            else
            {
                // If the old address exists, we can skip creating a new one
                _logger.LogInformation("Using existing address for customer.");
            }

            // await _unitOfWork.CommitAsync();

            return customerAndAddressResult;
        }
        catch (FailedToCreateException ex)
        {
            // await _unitOfWork.RollbackAsync();
            _logger.LogError(
                ex,
                "A known creation failure occurred while onboarding the customer or address."
            );
            throw;
        }
        catch (Exception ex)
        {
            // await _unitOfWork.RollbackAsync();
            string errorMsg =
                "An unexpected error occurred while onboarding the customer or address.";
            _logger.LogError(ex, errorMsg);
            throw;
        }
    }
}

