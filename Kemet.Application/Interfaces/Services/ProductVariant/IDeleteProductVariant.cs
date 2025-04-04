using Entities.Models.DTOs;
using Interfaces.IServices;

namespace Services.IProductVariantServices;
public interface IDeleteProductVariant : IDeleteServiceAsync<ProductVariantDeleteDTO>
{
}

