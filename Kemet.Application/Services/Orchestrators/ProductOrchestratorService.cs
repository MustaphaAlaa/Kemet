using System.Security.Cryptography.X509Certificates;
using Application.Exceptions;
using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using Entities.Models.Validations;
using IRepository.Generic;
using IServices;
using Microsoft.Extensions.Logging;

namespace Application.Services.Orchestrator;

public class ProductOrchestratorService : IProductOrchestratorService
{
    private readonly IProductService _productService;
    private readonly IProductVariantService _productVariantService;
    private readonly IPriceService _PriceService;

    private readonly IProductQuantityPriceService _productQuantityPriceService;

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<ProductOrchestratorService> _logger;

    public ProductOrchestratorService(
        IProductService productService,
        IProductVariantService productVariantService,
        IPriceService priceService,
        IProductQuantityPriceService productQuantityPriceService,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<ProductOrchestratorService> logger
    )
    {
        _productService = productService;
        _productVariantService = productVariantService;
        _PriceService = priceService;
        _productQuantityPriceService = productQuantityPriceService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    private async Task<ProductReadDTO> CreateProduct(
        ProductWithItSpecificationCreateDTO createRequest
    )
    {
        var productDto = _mapper.Map<ProductCreateDTO>(createRequest);

        var product = await _productService.CreateAsync(productDto);
        return product;
    }

    private async Task<PriceReadDTO> CreatePrice(ProductWithItSpecificationCreateDTO createRequest)
    {
        var priceDto = _mapper.Map<PriceCreateDTO>(createRequest);
        var price = await _PriceService.CreateAsync(priceDto);
        return price;
    }

    private async Task CreateProductQuantityPrice(ProductWithItSpecificationCreateDTO createRequest)
    {
        await _productQuantityPriceService.AddRange(createRequest.ProductQuantityPriceCreateDTOs);
    }

    private async Task<List<ProductVariantReadDTO>> CreateProductVariants(
        ProductWithItSpecificationCreateDTO createRequest,
        ProductReadDTO product
    )
    {
        List<ProductVariantCreateDTO> productVariantList = new();

        if (createRequest.AllColorsHasSameSizes && createRequest.IsStockQuantityUnified)
        {
            foreach (var colorId in createRequest.ColorsIds)
            {
                foreach (var sizeId in createRequest.SizesIds)
                {
                    productVariantList.Add(
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

        var productVariantReadList = await _productVariantService.AddRange(productVariantList);
        return productVariantReadList;
    }

    public async Task<bool> AddProductWithSpecific(
        ProductWithItSpecificationCreateDTO createRequest
    )
    {
        try
        {
            //Product
            var product = await CreateProduct(createRequest);

            //Price
            var price = await CreatePrice(createRequest);

            //Product Quantity Price
            await CreateProductQuantityPrice(createRequest);

            // Product Variant
            var productVariants = await CreateProductVariants(createRequest, product);

            var done = await _unitOfWork.SaveChangesAsync() > 0;
            return done;
        }
        catch (FailedToCreateException ex) // Or potentially catch more specific DB exceptions first
        {
            // Log the original exception correctly
            _logger.LogError(
                ex,
                "A known creation failure occurred while creating the Product with specifics."
            );
            // Re-throw the original exception to preserve the stack trace and type
            throw;
        }
        catch (Exception ex) // Catch any other unexpected exceptions
        {
            string errorMsg =
                "An unexpected error occurred while creating the Product with specifics.";
            // Log the original exception correctly
            _logger.LogError(ex, errorMsg);
            // Wrap the original exception in your custom type
            throw new FailedToCreateException(errorMsg, ex);
            throw;
        }
    }
}
