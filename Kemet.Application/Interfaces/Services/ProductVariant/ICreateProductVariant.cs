using Entities.Models;
using Entities.Models.DTOs;
using Interfaces.IServices;

namespace Services.IProductVariantServices;
public interface ICreateProductVariant : ICreateServiceAsync<ProductVariantCreateDTO, ProductVariantReadDTO>
{
}

