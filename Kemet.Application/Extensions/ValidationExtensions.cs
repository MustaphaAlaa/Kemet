using Kemet.Application.Interfaces.Validations;
using Kemet.Application.Validations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemet.Application.Extensions;


public static partial class ApplicationLayerExtensions
{
    private static void AddValidations(this IServiceCollection service)
    {
        service.AddScoped<ICreateColorValidation, CreateColorValidation>();
        service.AddScoped<IUpdateColorValidation, UpdateColorValidation>();
        service.AddScoped<IDeleteColorValidation, DeleteColorValidation>();

        service.AddScoped<ICreateSizeValidation, CreateSizeValidation>();
        service.AddScoped<IUpdateSizeValidation, UpdateSizeValidation>();
        service.AddScoped<IDeleteSizeValidation, DeleteSizeValidation>();

        service.AddScoped<ICreateProductValidation, CreateProductValidation>();
        service.AddScoped<IUpdateProductValidation, UpdateProductValidation>();
        service.AddScoped<IDeleteProductValidation, DeleteProductValidation>();

        service.AddScoped<ICreateProductVariantValidation, CreateProductVariantValidation>();
        service.AddScoped<IUpdateProductVariantValidation, UpdateProductVariantValidation>();
        service.AddScoped<IDeleteProductVariantValidation, DeleteProductVariantValidation>();

        service.AddScoped<ICreateReturnValidation, CreateReturnValidation>();
        service.AddScoped<IUpdateReturnValidation, UpdateReturnValidation>();
        service.AddScoped<IDeleteReturnValidation, DeleteReturnValidation>();

        service.AddScoped<ICreateOrderValidation, CreateOrderValidation>();
        service.AddScoped<IUpdateOrderValidation, UpdateOrderValidation>();
        service.AddScoped<IDeleteOrderValidation, DeleteOrderValidation>();

        service.AddScoped<ICreateOrderItemValidation, CreateOrderItemValidation>();
        service.AddScoped<IUpdateOrderItemValidation, UpdateOrderItemValidation>();
        service.AddScoped<IDeleteOrderItemValidation, DeleteOrderItemValidation>();

        //service.AddScoped<ICreateCustomerValidation, CreateCustomerValidation>();
        //service.AddScoped<IUpdateCustomerValidation, UpdateCustomerValidation>();
        //service.AddScoped<IDeleteCustomerValidation, DeleteCustomerValidation>(); 

    }

}


