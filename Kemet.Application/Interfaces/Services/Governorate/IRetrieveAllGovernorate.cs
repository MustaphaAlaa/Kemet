using Entities.Models;
using Entities.Models.DTOs;
using Domain.IServices;

namespace IServices.IGovernorateServices;

public interface IRetrieveAllGovernorate : IRetrieveAllServiceAsync<Governorate, GovernorateReadDTO>
{
}
