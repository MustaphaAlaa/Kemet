using Entities.Models.DTOs;

namespace IServices;

public interface IProductOrchestratorService
{
    public Task<bool> AddProductWithSpecific(ProductWithVariantsCreateDTO createRequest);
}
