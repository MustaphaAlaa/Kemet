using Entities.Models.DTOs;
using Domain.IServices;

namespace IServices.IGovernorateServices;
public interface IUpdateGovernorate : IUpdateServiceAsync<GovernorateUpdateDTO, GovernorateReadDTO>
{
}
