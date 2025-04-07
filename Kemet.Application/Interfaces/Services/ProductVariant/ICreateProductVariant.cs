using Entities.Models;
using Entities.Models.DTOs;
using Domain.IServices;

namespace Services.IProductVariantServices;
public interface ICreateProductVariant : ICreateServiceAsync<ProductVariantCreateDTO, ProductVariantReadDTO>
{
}

