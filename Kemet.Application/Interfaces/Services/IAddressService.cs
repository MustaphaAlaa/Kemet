using Domain.IServices;
using Entities.Models;
using Entities.Models.DTOs;

namespace IServices;

public interface IAddressService
    : IServiceAsync<
        Address,
        AddressCreateDTO,
        AddressDeleteDTO,
        AddressUpdateDTO,
        AddressReadDTO
    > { }
