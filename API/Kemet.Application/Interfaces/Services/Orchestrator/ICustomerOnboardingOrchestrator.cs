using Application.Services.Orchestrator;
using Entities.Models.DTOs.Orchestrates;

namespace IServices.Orchestrator;

public interface ICustomerOnboardingOrchestrator
{
    Task<CustomerAndAddressResult> EnsureCustomerOnboardingAsync(
        CreatingOrderForAnonymousCustomerRequest request
    );
}
