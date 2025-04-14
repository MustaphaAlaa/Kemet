using System.Security.Cryptography.X509Certificates;
using Application.Exceptions;
using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using IRepository.Generic;
using IServices;
using Entities.Models.Validations;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class ProductOrchestratorService : IProductOrchestratorService
{
    private readonly IProductService _productService;
    private readonly IProductVariantService _productVariantService;
    private readonly IPriceService _PriceService;

    private readonly IProductQuantityPriceService _productQuantityPriceService;

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<ProductOrchestratorService> _logger;

    public async Task<ProductReadDTO> AddProduct(ProductCreateDTO productCreateDTO)
    {
        return await _productService.CreateInternalAsync(productCreateDTO);
    }

    public async Task<bool> AddProductWithSpecific(
        ProductWithItSpecificationCreateDTO createRequest
    )
    {
        try
        {
            foreach (var Quantity in createRequest.ProductQuantityPriceCreateDTOs)
            {
                if (
                    Quantity.UnitPrice < createRequest.MinimumPrice
                    || Quantity.UnitPrice > createRequest.MaximumPrice
                )
                {
                    throw new Exception("Price is not in the range of minimum and maximum price");
                }
            }

            var productDto = _mapper.Map<ProductCreateDTO>(createRequest);
            var product = await _productService.CreateAsync(productDto);

            List<ProductVariantCreateDTO> productVariantList = new();

            if (createRequest.AllColorsHasSameSizes && createRequest.IsStockQuantityUnified)
            {
                foreach (var colorId in createRequest.ColorsIds)
                {
                    foreach (var sizeId in createRequest.SizesIds)
                    {
                        await _productVariantService.CreateAsync(
                            new ProductVariantCreateDTO
                            {
                                ColorId = colorId,
                                SizeId = sizeId,
                                ProductId = product.ProductId,
                                StockQuantity = createRequest.UnifiedStock,
                            }
                        );
                    }
                }
            }
            else
            {
                foreach (var kvp in createRequest.ColorsWithItSizesAndStock)
                {
                    var colorId = kvp.Key;

                    foreach (var ProductVariantItem in kvp.Value)
                    {
                        productVariantList.Add(
                            new ProductVariantCreateDTO
                            {
                                ColorId = colorId,
                                SizeId = ProductVariantItem.SizeId,
                                StockQuantity = ProductVariantItem.StockQuantity,
                                ProductId = product.ProductId,
                            }
                        );
                    }
                }
            }
            await _productVariantService.AddRange(productVariantList);

            // ProductVariantCreateDTO productVariantDto;
            // foreach (var productVariant in createRequest.)
            // {
            //     productVariantDto = _mapper.Map<ProductVariantCreateDTO>(productVariant);
            //     await _productVariantService.CreateAsync(productVariantDto);
            // }

            // var priceDto = _mapper.Map<PriceCreateDTO>(createRequest);
            // await _PriceService.CreateAsync(priceDto);

            // var done = await _unitOfWork.SaveChangesAsync() > 0;
            // return done;
        }
        catch (Exception ex)
        {
            string msg = $"An error occurred while creating the Product. \n{ex.Message}";
            _logger.LogError(msg);
            throw new FailedToCreateException(msg);
        }
    }
}
