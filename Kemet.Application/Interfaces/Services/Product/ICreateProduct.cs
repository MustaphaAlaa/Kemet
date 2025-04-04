using Entities.Models.DTOs;
using Interfaces.IServices;

namespace IServices.IProductServices;
public interface ICreateProduct : ICreateServiceAsync<ProductCreateDTO, ProductReadDTO>
{
}

