using Entities.Models.DTOs;

namespace IServices.Orchestrator;

public interface IProductVariantStockOrchestratorService
{
    Task<bool> AddProductWithSpecific(ProductWithVariantsCreateDTO createRequest);
    Task<ProductVariantUpdateStockDTO> UpdateStock(ProductVariantUpdateStockDTO productVariantUpdateStockDTO);
}
