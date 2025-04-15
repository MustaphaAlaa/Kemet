using Domain.IServices;
using Entities.Models;
using Entities.Models.DTOs;

namespace IServices;

public interface ICustomerService
    : IServiceAsync<
        Customer,
        CustomerCreateDTO,
        CustomerDeleteDTO,
        CustomerUpdateDTO,
        CustomerReadDTO
    > { }
