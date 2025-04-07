using Entities.Models;
using Entities.Models.DTOs;
using Domain.IServices;

namespace Application.IProductVariantServices;
public interface ICreateProductVariant : ICreateServiceAsync<ProductVariantCreateDTO, ProductVariantReadDTO>
{
}

