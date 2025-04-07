using Entities.Models;
using Entities.Models.DTOs;
using Domain.IServices;


namespace Services.IProductVariantServices;
public interface IRetrieveAllProductVariants : IRetrieveAllServiceAsync<ProductVariant, ProductVariantReadDTO>
{
}

