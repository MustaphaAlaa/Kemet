using Domain.IServices;
using Entities.Models;
using Entities.Models.DTOs;

namespace IServices;

public interface IProductVariantService
    : IServiceAsync<
        ProductVariant,
        ProductVariantDeleteDTO,
        ProductVariantUpdateDTO,
        ProductVariantReadDTO
    > { }
