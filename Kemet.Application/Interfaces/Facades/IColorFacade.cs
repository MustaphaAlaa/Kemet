using System.Linq.Expressions;
using Entities.Models;
using Entities.Models.DTOs;

namespace Kemet.Application.Interfaces.Facades;

public interface CRUDFacade<Model, ReadDTO, CreateDTO, UpdateDTO, DeleteDTO>
{
    public Task<ReadDTO> Create(CreateDTO dto);
    public Task<ReadDTO> Update(UpdateDTO dto);
    public Task<ReadDTO> Retrieve(Expression<Func<Model, bool>> predicate);
    public Task<List<ReadDTO>> RetrieveAll();
    public Task<IQueryable<ReadDTO>> RetrieveAll(Expression<Func<Model, bool>> predicate);

    public Task<bool> Delete(DeleteDTO dto);
}

public interface IColorFacade
    : CRUDFacade<Color, ColorReadDTO, ColorCreateDTO, ColorUpdateDTO, ColorDeleteDTO> { }

public interface IOrderItemFacade
    : CRUDFacade<
        OrderItem,
        OrderItemReadDTO,
        OrderItemCreateDTO,
        OrderItemUpdateDTO,
        OrderItemDeleteDTO
    > { }

public interface IGovernorateFacade
    : CRUDFacade<
        Governorate,
        GovernorateReadDTO,
        GovernorateCreateDTO,
        GovernorateUpdateDTO,
        GovernorateDeleteDTO
    > { }

public interface IProductFacade
    : CRUDFacade<Product, ProductReadDTO, ProductCreateDTO, ProductUpdateDTO, ProductDeleteDTO> { }

public interface ISizeFacade
    : CRUDFacade<Size, SizeReadDTO, SizeCreateDTO, SizeUpdateDTO, SizeDeleteDTO> { }

public interface IProductVariantFacade
    : CRUDFacade<
        ProductVariant,
        ProductVariantReadDTO,
        ProductVariantCreateDTO,
        ProductVariantUpdateDTO,
        ProductVariantDeleteDTO
    > { }
