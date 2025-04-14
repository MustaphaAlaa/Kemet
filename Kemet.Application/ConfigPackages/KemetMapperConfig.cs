using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.ConfKemetMapperConfigPackages;

public class KemetMapperConfig : Profile
{
    public KemetMapperConfig()
    {
        // Order mappings
        CreateMap<Order, OrderReadDTO>().ReverseMap();
        CreateMap<OrderCreateDTO, Order>().ReverseMap();
        CreateMap<OrderUpdateDTO, Order>().ReverseMap();
        //CreateMap<OrderDeleteDTO, Order>().ReverseMap();

        CreateMap<OrderItem, OrderItemReadDTO>().ReverseMap();
        CreateMap<OrderItemCreateDTO, OrderItem>().ReverseMap();
        CreateMap<OrderItemUpdateDTO, OrderItem>().ReverseMap();

        // Return mappings
        CreateMap<Return, ReturnReadDTO>().ReverseMap();
        CreateMap<ReturnCreateDTO, Return>().ReverseMap();
        CreateMap<ReturnUpdateDTO, Return>().ReverseMap();

        // Product mappings
        CreateMap<Product, ProductReadDTO>().ReverseMap();
        CreateMap<ProductCreateDTO, Product>().ReverseMap();
        CreateMap<ProductUpdateDTO, Product>().ReverseMap();

        // ProductVariant mappings
        CreateMap<ProductVariant, ProductVariantReadDTO>().ReverseMap();
        CreateMap<ProductVariantCreateDTO, ProductVariant>().ReverseMap();
        CreateMap<ProductVariantUpdateDTO, ProductVariant>().ReverseMap();

        // Size mappings
        CreateMap<Size, SizeReadDTO>().ReverseMap();
        CreateMap<SizeCreateDTO, Size>().ReverseMap();
        CreateMap<SizeUpdateDTO, Size>().ReverseMap();

        // Color mappings
        CreateMap<Color, ColorReadDTO>().ReverseMap();
        CreateMap<ColorCreateDTO, Color>().ReverseMap();
        CreateMap<ColorUpdateDTO, Color>().ReverseMap();

        // Category mappings
        CreateMap<Category, CategoryReadDTO>().ReverseMap();
        CreateMap<CategoryCreateDTO, Category>().ReverseMap();
        CreateMap<CategoryUpdateDTO, Category>().ReverseMap();

        // Address mappings
        CreateMap<Address, AddressReadDTO>().ReverseMap();
        CreateMap<AddressCreateDTO, Address>().ReverseMap();
        CreateMap<AddressUpdateDTO, Address>().ReverseMap();

        // Governorate mappings
        CreateMap<Governorate, GovernorateReadDTO>().ReverseMap();
        CreateMap<GovernorateCreateDTO, Governorate>().ReverseMap();
        CreateMap<GovernorateUpdateDTO, Governorate>().ReverseMap();

        // Customer mappings
        CreateMap<Customer, CustomerReadDTO>().ReverseMap();
        CreateMap<CustomerCreateDTO, Customer>().ReverseMap();
        CreateMap<CustomerUpdateDTO, Customer>().ReverseMap();
    }
}
