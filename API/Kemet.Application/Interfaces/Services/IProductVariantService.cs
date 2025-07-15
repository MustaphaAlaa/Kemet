using Domain.IServices;
using Entities.Models;
using Entities.Models.DTOs;

namespace IServices;

public interface IProductVariantService
    : IServiceAsync<
        ProductVariant,
        int,
        ProductVariantCreateDTO,
        ProductVariantDeleteDTO,
        ProductVariantUpdateDTO,
        ProductVariantReadDTO
    >
{
    Task<List<ProductVariantReadDTO>> AddRange(IEnumerable<ProductVariantCreateDTO> productVariantCreateDTOs);
    Task<bool> CheckProductVariantAvailability(int productVariantId);
    Task<bool> CheckProductVariantAvailability(int productVariantId, int Quantity);
    Task<ProductVariantReadDTO> UpdateStock(int productVariantId, int stock);
}
