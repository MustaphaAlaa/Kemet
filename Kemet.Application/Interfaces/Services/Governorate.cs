using Entities.Models;
using Entities.Models.DTOs;
using Interfaces.IServices;

namespace IServices.IServServices;
public interface IUpdateGovernorate : IUpdateServiceAsync<GovernorateUpdateDTO, GovernorateReadDTO>
{
}

public interface IRetrieveGovernorate : IRetrieveServiceAsync<Governorate?, GovernorateReadDTO?>
{

}

public interface IRetrieveAllGovernorate : IRetrieveAllServiceAsync<Governorate, GovernorateReadDTO>
{
}


public interface IDeleteGovernorate : IDeleteServiceAsync<GovernorateDeleteDTO>
{

}


public interface ICreateGovernorate : ICreateServiceAsync<GovernorateCreateDTO, GovernorateReadDTO>
{

}