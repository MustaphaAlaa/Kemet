using Entities.Models.DTOs;
using Interfaces.IServices;

namespace Services.IProductVariantServices;
public interface IUpdateProductVariant : IUpdateServiceAsync<ProductVariantUpdateDTO, ProductVariantReadDTO>
{
}

