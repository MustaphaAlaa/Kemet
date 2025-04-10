using Entities.Models.DTOs;
using Domain.IServices;

namespace IServices.IGovernorateServices;

public interface ICreateGovernorate : IServiceAsync<GovernorateCreateDTO, GovernorateReadDTO>
{

}