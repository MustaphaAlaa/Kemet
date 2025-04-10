using Entities.Models.DTOs;
using Domain.IServices;
using Entities.Models;

namespace IServices.IOrderServices;

public interface ICreateOrder : IServiceAsync<OrderCreateDTO, OrderReadDTO>
{

}

public interface IUpdateOrder : IUpdateServiceAsync<OrderUpdateDTO, OrderReadDTO>
{
}

public interface IRetrieveOrder : IRetrieveServiceAsync<Order?, OrderReadDTO?>
{

}

public interface IRetrieveAllOrder : IRetrieveAllServiceAsync<Order, OrderReadDTO>
{
}


public interface IDeleteOrder : IDeleteServiceAsync<OrderDeleteDTO>
{

}
