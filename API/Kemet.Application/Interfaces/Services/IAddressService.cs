using Domain.IServices;
using Entities.Models;
using Entities.Models.DTOs;

namespace IServices;

public interface IAddressService
    : IServiceAsync<
        Address,
        int,
        AddressCreateDTO,
        AddressDeleteDTO,
        AddressUpdateDTO,
        AddressReadDTO
    >
{
    Task<AddressReadDTO> GetActiveAddressByCustomerId(Guid customerId);
}
