using Application.Services;
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
    Task<List<ProductQuantityPriceReadDTO>> AddRange(
        IEnumerable<ProductQuantityPriceCreateDTO> productQuantityPriceCreateDTOs
    );
    Task<IEnumerable<ProductQuantityPriceReadDTO>> ActiveQuantityPriceFor(int ProductId);
    Task<ProductQuantityPriceReadDTO> ActiveProductPriceForQuantityWithId(
        int ProductId,
    int Quantity
    );

    Task<ProductQuantityPriceReadDTO> Deactivate(int ProductId, int ProductQuantityPriceId);

}
