using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models.DTOs;

namespace Kemet.Application.Interfaces.Validations;



public interface ICreateColorValidation : IValidate<ColorCreateDTO>
{
}

public interface IUpdateColorValidation : IUpdateValidation<ColorUpdateDTO, ColorReadDTO>
{
}
public interface IDeleteColorValidation : IValidate<ColorDeleteDTO>
{
}




public interface ICreateSizeValidation : IValidate<SizeCreateDTO>
{
}

public interface IUpdateSizeValidation : IUpdateValidation<SizeUpdateDTO, SizeReadDTO>
{
}
public interface IDeleteSizeValidation : IValidate<SizeDeleteDTO>
{
}


// Add the following interfaces for the remaining models

public interface ICreateProductValidation : IValidate<ProductCreateDTO>
{
}

public interface IUpdateProductValidation : IUpdateValidation<ProductUpdateDTO, ProductReadDTO>
{
}

public interface IDeleteProductValidation : IValidate<ProductDeleteDTO>
{
}

public interface ICreateProductVariantValidation : IValidate<ProductVariantCreateDTO>
{
}

public interface IUpdateProductVariantValidation : IUpdateValidation<ProductVariantUpdateDTO, ProductVariantReadDTO>
{
}

public interface IDeleteProductVariantValidation : IValidate<ProductVariantDeleteDTO>
{
}

public interface ICreateCategoryValidation : IValidate<CategoryCreateDTO>
{
}

public interface IUpdateCategoryValidation : IUpdateValidation<CategoryUpdateDTO, CategoryReadDTO>
{
}

public interface IDeleteCategoryValidation : IValidate<CategoryDeleteDTO>
{
}


// Validation interfaces for Return
public interface ICreateReturnValidation : IValidate<ReturnCreateDTO>
{
}

public interface IUpdateReturnValidation : IUpdateValidation<ReturnUpdateDTO, ReturnReadDTO>
{
}

public interface IDeleteReturnValidation : IValidate<ReturnDeleteDTO>
{
}

// Validation interfaces for Order
public interface ICreateOrderValidation : IValidate<OrderCreateDTO>
{
}

public interface IUpdateOrderValidation : IUpdateValidation<OrderUpdateDTO, OrderReadDTO>
{
}

public interface IDeleteOrderValidation : IValidate<OrderDeleteDTO>
{
}

// Validation interfaces for OrderItem
public interface ICreateOrderItemValidation : IValidate<OrderItemCreateDTO>
{
}

public interface IUpdateOrderItemValidation : IUpdateValidation<OrderItemUpdateDTO, OrderItemReadDTO>
{
}

public interface IDeleteOrderItemValidation : IValidate<OrderItemDeleteDTO>
{
}



// Validation interfaces for Governorate
public interface ICreateGovernorateValidation : IValidate<GovernorateCreateDTO>
{
}

public interface IUpdateGovernorateValidation : IUpdateValidation<GovernorateUpdateDTO, GovernorateReadDTO>
{
}

public interface IDeleteGovernorateValidation : IValidate<GovernorateDeleteDTO>
{
}

// Validation interfaces for Customer
public interface ICreateCustomerValidation : IValidate<CustomerCreateDTO>
{
}

public interface IUpdateCustomerValidation : IUpdateValidation<CustomerUpdateDTO, CustomerReadDTO>
{
}

public interface IDeleteCustomerValidation : IValidate<CustomerDeleteDTO>
{
}

// Validation interfaces for Address
public interface ICreateAddressValidation : IValidate<AddressCreateDTO>
{
}

public interface IUpdateAddressValidation : IUpdateValidation<AddressUpdateDTO, AddressReadDTO>
{
}

public interface IDeleteAddressValidation : IValidate<AddressDeleteDTO>
{
}







