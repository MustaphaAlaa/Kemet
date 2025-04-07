using Entities.Models;
using Entities.Models.DTOs;
using Domain.IServices;

namespace IServices.IProductServices;
public interface IRetrieveAllProducts : IRetrieveAllServiceAsync<Product, ProductReadDTO>
{
}

