using Entities.Models;
using Entities.Models.DTOs;
using Interfaces.IServices;


namespace Services.IProductVariantServices;
public interface IRetrieveAllProductVariants : IRetrieveAllServiceAsync<ProductVariant, ProductVariantReadDTO>
{
}

