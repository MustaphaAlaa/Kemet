using AutoMapper;
using Entities.Models.DTOs;
using Kemet.Application.Services.Orchestrators;

namespace Entities.Models.ConfKemetMapperConfigPackages;

//For Mappings
public class KemetMapperConfig : Profile
{
    public KemetMapperConfig()
    {
        // Order mappings
        CreateMap<Order, OrderReadDTO>()
            .ReverseMap();
        CreateMap<OrderCreateDTO, Order>().ReverseMap();
        CreateMap<OrderUpdateDTO, Order>().ReverseMap();
        //CreateMap<OrderDeleteDTO, Order>().ReverseMap();

        CreateMap<OrderItem, OrderItemReadDTO>()
            .ReverseMap();
        CreateMap<OrderItemCreateDTO, OrderItem>().ReverseMap();
        CreateMap<OrderItemUpdateDTO, OrderItem>().ReverseMap();

        // Return mappings
        CreateMap<Return, ReturnReadDTO>()
            .ReverseMap();
        CreateMap<ReturnCreateDTO, Return>().ReverseMap();
        CreateMap<ReturnUpdateDTO, Return>().ReverseMap();

        // Product mappings
        CreateMap<Product, ProductReadDTO>()
            .ReverseMap();
        CreateMap<ProductCreateDTO, Product>().ReverseMap();
        CreateMap<ProductUpdateDTO, Product>().ReverseMap();

        // ProductVariant mappings
        CreateMap<ProductVariant, ProductVariantReadDTO>()
            .ReverseMap();
        CreateMap<ProductVariantCreateDTO, ProductVariant>().ReverseMap();
        CreateMap<ProductVariantUpdateDTO, ProductVariant>().ReverseMap();
        CreateMap<ProductVariantUpdateDTO, ProductVariantReadDTO>().ReverseMap();
        CreateMap<ProductVariantUpdateStockDTO, ProductVariant>().ReverseMap();

        // Size mappings
        CreateMap<Size, SizeReadDTO>()
            .ReverseMap();
        CreateMap<SizeCreateDTO, Size>().ReverseMap();
        CreateMap<SizeUpdateDTO, Size>().ReverseMap();

        // Color mappings
        CreateMap<Color, ColorReadDTO>()
            .ReverseMap();
        CreateMap<ColorCreateDTO, Color>().ReverseMap();
        CreateMap<ColorUpdateDTO, Color>().ReverseMap();

        // Category mappings
        CreateMap<Category, CategoryReadDTO>()
            .ReverseMap();
        CreateMap<CategoryCreateDTO, Category>().ReverseMap();
        CreateMap<CategoryUpdateDTO, Category>().ReverseMap();

        // Address mappings
        CreateMap<Address, AddressReadDTO>()
            .ReverseMap();
        CreateMap<AddressCreateDTO, Address>().ReverseMap();
        CreateMap<AddressUpdateDTO, Address>().ReverseMap();
        CreateMap<AddressCreateDTO, AddressUpdateDTO>().ReverseMap();

        // Governorate mappings
        CreateMap<Governorate, GovernorateReadDTO>()
            .ReverseMap();
        CreateMap<GovernorateCreateDTO, Governorate>().ReverseMap();
        CreateMap<GovernorateUpdateDTO, Governorate>().ReverseMap();

        // Customer mappings
        CreateMap<Customer, CustomerReadDTO>()
            .ReverseMap();
        CreateMap<CustomerCreateDTO, Customer>().ReverseMap();
        CreateMap<CustomerUpdateDTO, Customer>().ReverseMap();

        //UnitPrice
        CreateMap<PriceCreateDTO, Price>()
            .ReverseMap();
        CreateMap<PriceReadDTO, Price>().ReverseMap();
        CreateMap<PriceUpdateDTO, Price>().ReverseMap();
        CreateMap<PriceDeleteDTO, Price>().ReverseMap();

        //Product Quantity UnitPrice
        CreateMap<ProductQuantityPriceCreateDTO, ProductQuantityPrice>()
            .ReverseMap();
        CreateMap<ProductQuantityPriceReadDTO, ProductQuantityPrice>().ReverseMap();
        CreateMap<ProductQuantityPriceCreateDTO, ProductQuantityPriceUpdateDTO>().ReverseMap();
        CreateMap<ProductQuantityPriceUpdateDTO, ProductQuantityPrice>().ReverseMap();

        // ProductPriceOrchestratorDTOs
        CreateMap<ProductQuantityPriceUpdateDTO, ProductPriceOrchestratorDTO>()
            .ReverseMap();
        CreateMap<ProductQuantityPriceReadDTO, ProductPriceOrchestratorDTO>().ReverseMap();
        CreateMap<ProductQuantityPriceDeleteDTO, ProductPriceOrchestratorDTO>().ReverseMap();
        CreateMap<ProductQuantityPriceCreateDTO, ProductPriceOrchestratorCreateDTO>().ReverseMap();

        CreateMap<PriceCreateDTO, ProductPriceOrchestratorCreateDTO>().ReverseMap();
        CreateMap<PriceReadDTO, ProductPriceOrchestratorDTO>().ReverseMap();
        CreateMap<PriceUpdateDTO, ProductPriceOrchestratorDTO>().ReverseMap();
        CreateMap<PriceDeleteDTO, ProductPriceOrchestratorDTO>().ReverseMap();

        CreateMap<ProductWithVariantsCreateDTO, ProductCreateDTO>().ReverseMap();

        //Delivery Company
        CreateMap<DeliveryCompanyCreateDTO, DeliveryCompany>()
            .ReverseMap();
        CreateMap<DeliveryCompanyUpdateDTO, DeliveryCompany>().ReverseMap();
        CreateMap<DeliveryCompanyReadDTO, DeliveryCompany>().ReverseMap();

        // GovernorateDeliveryCompany
        CreateMap<GovernorateDeliveryCompanyCreateDTO, GovernorateDeliveryCompany>()
            .ReverseMap();
        CreateMap<GovernorateDeliveryCompanyUpdateDTO, GovernorateDeliveryCompany>().ReverseMap();
        CreateMap<GovernorateDeliveryCompanyReadDTO, GovernorateDeliveryCompany>().ReverseMap();


        // GovernorateDelivery
        CreateMap<GovernorateDeliveryCreateDTO, GovernorateDelivery>()
            .ReverseMap();
        CreateMap<GovernorateDeliveryUpdateDTO, GovernorateDelivery>().ReverseMap();
        CreateMap<GovernorateDeliveryReadDTO, GovernorateDelivery>().ReverseMap();
        CreateMap<GovernorateDeliveryDTO, GovernorateDelivery>().ReverseMap();
    }
}
