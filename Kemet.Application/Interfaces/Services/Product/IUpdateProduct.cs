using Entities.Models.DTOs;
using Domain.IServices;

namespace IServices.IProductServices;
public interface IUpdateProduct : IUpdateServiceAsync<ProductUpdateDTO, ProductReadDTO>
{
}

