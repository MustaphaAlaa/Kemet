using Domain.IServices;
using Entities.Models;
using Entities.Models.DTOs;

namespace IServices;

public interface IProductVariantService
    : IServiceAsync<
        ProductVariant,
        ProductVariantCreateDTO,
        ProductVariantDeleteDTO,
        ProductVariantUpdateDTO,
        ProductVariantReadDTO
    >
{
    Task<List<ProductVariantReadDTO>> AddRange(IEnumerable<ProductVariantCreateDTO> productVariantCreateDTOs);
}
