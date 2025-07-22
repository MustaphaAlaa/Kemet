using Entities.Models.DTOs.Orchestrates;

namespace IServices.Orchestrator;

public interface IOrderOrchestratorService
{
    Task CreateOrder(CreatingOrderForAnonymousCustomerRequest request);
}