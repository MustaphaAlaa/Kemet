using Entities.Models;
using Entities.Models.DTOs;
using Interfaces.IServices;

namespace IServices.IProductServices;
public interface IRetrieveAllProducts : IRetrieveAllServiceAsync<Product, ProductReadDTO>
{
}

