using Entities.Models.DTOs;
using Domain.IServices;

namespace Application.IProductVariantServices;
public interface IUpdateProductVariant : IUpdateServiceAsync<ProductVariantUpdateDTO, ProductVariantReadDTO>
{
}

