using Domain.IServices;
using Entities.Models;
using Entities.Models.DTOs;

namespace IServices;

public interface ICustomerService
    : IServiceAsync<
        Customer,
        int,
        CustomerCreateDTO,
        CustomerDeleteDTO,
        CustomerUpdateDTO,
        CustomerReadDTO
    >
{ }
