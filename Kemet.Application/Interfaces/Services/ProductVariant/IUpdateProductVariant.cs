using Entities.Models.DTOs;
using Domain.IServices;

namespace Services.IProductVariantServices;
public interface IUpdateProductVariant : IUpdateServiceAsync<ProductVariantUpdateDTO, ProductVariantReadDTO>
{
}

