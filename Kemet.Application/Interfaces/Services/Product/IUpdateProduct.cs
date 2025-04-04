using Entities.Models.DTOs;
using Interfaces.IServices;

namespace IServices.IProductServices;
public interface IUpdateProduct : IUpdateServiceAsync<ProductUpdateDTO, ProductReadDTO>
{
}

