using Entities.Models.DTOs;

namespace Application.Services.Orchestrator;

public interface IProductVariantStockOrchestratorService
{
    Task<bool> AddProductWithSpecific(ProductWithVariantsCreateDTO createRequest);
    Task<ProductVariantUpdateStockDTO> UpdateStock(ProductVariantUpdateStockDTO productVariantUpdateStockDTO);
}