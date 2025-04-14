using Entities.Models.DTOs;

namespace IServices;

public interface IProductOrchestratorService
{
    public Task<bool> AddProductWithSpecific(ProductWithItSpecificationCreateDTO createRequest);
}
