using Entities.Models;
using Entities.Models.DTOs;
using Domain.IServices;

namespace IServices.IGovernorateServices;

public interface IRetrieveGovernorate : IRetrieveServiceAsync<Governorate?, GovernorateReadDTO?>
{

}
