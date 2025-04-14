using Domain.IServices;
using Entities.Models;
using Entities.Models.DTOs;

namespace IServices;

public interface IProductQuantityPriceService
    : IServiceAsync<
        ProductQuantityPrice,
        ProductQuantityPriceCreateDTO,
        ProductQuantityPriceDeleteDTO,
        ProductQuantityPriceUpdateDTO,
        ProductQuantityPriceReadDTO
    >
{
    Task AddRange(IEnumerable<ProductQuantityPriceCreateDTO> productQuantityPriceCreateDTOs);
}
