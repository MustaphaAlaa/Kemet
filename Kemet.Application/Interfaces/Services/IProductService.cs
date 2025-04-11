using Domain.IServices;
using Entities.Models;
using Entities.Models.DTOs;

namespace IServices;

public interface IProductService
    : IServiceAsync<
        Product,
        ProductCreateDTO,
        ProductDeleteDTO,
        ProductUpdateDTO,
        ProductReadDTO
    > { }
