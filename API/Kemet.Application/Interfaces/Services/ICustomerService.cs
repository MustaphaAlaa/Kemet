using Domain.IServices;
using Entities.Models;
using Entities.Models.DTOs;

namespace IServices;

public interface ICustomerService
    : IServiceAsync<
        Customer,
        Guid,
        CustomerCreateDTO,
        CustomerDeleteDTO,
        CustomerUpdateDTO,
        CustomerReadDTO
    >
{
    Task<CustomerReadDTO> FindCustomerByPhoneNumberAsync(string phoneNumber);
    Task<bool> IsCustomerExist(string phoneNumber);
    Task<Customer> CreateWithTrackingAsync(CustomerCreateDTO entity);
}
