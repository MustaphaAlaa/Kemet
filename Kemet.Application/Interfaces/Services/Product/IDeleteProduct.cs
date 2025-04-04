using Entities.Models.DTOs;
using Interfaces.IServices;

namespace IServices.IProductServices;
public interface IDeleteProduct : IDeleteServiceAsync<ProductDeleteDTO>
{
}

