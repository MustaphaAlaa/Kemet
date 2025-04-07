using Entities.Models.DTOs;
using Domain.IServices;

namespace Services.IProductVariantServices;
public interface IDeleteProductVariant : IDeleteServiceAsync<ProductVariantDeleteDTO>
{
}

