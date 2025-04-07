using Entities.Models;
using Entities.Models.DTOs;
using Domain.IServices;

namespace IServices.IReturnServices;
public interface IUpdateReturn : IUpdateServiceAsync<ReturnUpdateDTO, ReturnReadDTO>
{
}

public interface IRetrieveReturn : IRetrieveServiceAsync<Return?, ReturnReadDTO?>
{

}

public interface IRetrieveAllReturn : IRetrieveAllServiceAsync<Return, ReturnReadDTO>
{
}


public interface IDeleteReturn : IDeleteServiceAsync<ReturnDeleteDTO>
{

}


public interface ICreateReturn : ICreateServiceAsync<ReturnCreateDTO, ReturnReadDTO>
{

}