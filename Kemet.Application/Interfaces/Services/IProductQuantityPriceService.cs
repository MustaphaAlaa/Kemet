using Domain.IServices;
using Entities.Models;
using Entities.Models.DTOs;

namespace IServices;

public interface IProductQuantityPriceService
    : IServiceAsync<
        ProductQuantityPrice,
        int,
        ProductQuantityPriceCreateDTO,
        ProductQuantityPriceDeleteDTO,
        ProductQuantityPriceUpdateDTO,
        ProductQuantityPriceReadDTO
    >
{
    Task AddRange(IEnumerable<ProductQuantityPriceCreateDTO> productQuantityPriceCreateDTOs);
    Task<IEnumerable<ProductQuantityPriceReadDTO>> ActiveQunatityPriceForPrdouctWithId(int ProductId);
    Task<ProductQuantityPriceReadDTO> ActiveProductPriceForQunatityWithId(int ProductId, int Quantity);
}
