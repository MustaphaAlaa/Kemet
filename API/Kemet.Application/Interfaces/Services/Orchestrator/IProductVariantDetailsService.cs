using Entities.Models.DTOs;

namespace IServices.Orchestrator;

public interface IProductVariantDetailsService
{
    Task<List<ColorReadDTO>> RetrieveProductVarientColors(int productId);
    Task<List<ProductVariantReadWithDetailsDTO>> RetrieveProductVarientColorsSizes(int productId, int colorId);
    Task<ProductVariantReadWithDetailsDTO> RetrieveProductVarientStock(int productId, int colorId, int sizeId);
}