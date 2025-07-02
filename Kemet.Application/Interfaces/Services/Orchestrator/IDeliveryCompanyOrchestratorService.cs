using Entities.Models.DTOs;

namespace IServices.Orchestrator;

public interface IDeliveryCompanyOrchestratorService
{
    /// <summary>
    /// this method for create Delivery Company and its governorate with default value for the cost 
    /// </summary>
    /// <param name="createRequest"></param>
    /// <returns></returns>
    public Task CreateDeliveryCompany(DeliveryCompanyCreateDTO createRequest);
}