using Entities.Models.DTOs;
using Domain.IServices;

namespace Application.IProductVariantServices;
public interface IDeleteProductVariant : IDeleteServiceAsync<ProductVariantDeleteDTO>
{
}

