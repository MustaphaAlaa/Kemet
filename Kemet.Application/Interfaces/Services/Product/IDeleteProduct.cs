using Entities.Models.DTOs;
using Domain.IServices;

namespace IServices.IProductServices;
public interface IDeleteProduct : IDeleteServiceAsync<ProductDeleteDTO>
{
}

