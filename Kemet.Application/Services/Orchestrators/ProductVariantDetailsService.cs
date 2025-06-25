using Application.Exceptions;
using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using IRepository.Generic;
using IServices;
using Microsoft.Extensions.Logging;

namespace Application.Services.Orchestrator;

public class ProductVariantDetailsService : IProductVariantDetailsService
{
    private readonly IProductService _productService;
    private readonly IProductVariantService _productVariantService;
    private readonly IPriceService _priceService;
    private readonly IColorService _colorService;
    private readonly ISizeService _sizeService;

    private readonly IProductQuantityPriceService _productQuantityPriceService;

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<ProductVariantDetailsService> _logger;

    public ProductVariantDetailsService(IProductService productService,
        IProductVariantService productVariantService, IPriceService priceService, IColorService colorService, ISizeService sizeService, IProductQuantityPriceService productQuantityPriceService, IUnitOfWork unitOfWork, IMapper mapper, ILogger<ProductVariantDetailsService> logger)
    {
        _productService = productService;
        _productVariantService = productVariantService;
        _priceService = priceService;
        _colorService = colorService;
        _sizeService = sizeService;
        _productQuantityPriceService = productQuantityPriceService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }





    public async Task<List<ProductVariantReadWithDetailsDTO>> RetrieveProductVarientColorsSizes(
        int productId, int colorId
    )
    {


        try
        {


            var productVariantDetailsLst = new List<ProductVariantReadWithDetailsDTO>();
            var productVarients = await _productVariantService.RetrieveAllAsync(p => p.ProductId == productId && p.ColorId == colorId);

            foreach (var productVar in productVarients)
            {

                var size = await _sizeService.RetrieveByAsync(s => s.SizeId == productVar.SizeId);
                productVariantDetailsLst.Add(new()
                {
                    ProductVariantId = productVar.ProductId,
                    ProductId = productId,
                    Size = size,
                });
            }






            return productVariantDetailsLst;
        }

        catch (Exception ex) // Catch any other unexpected exceptions
        {

            string errorMsg =
                "An unexpected error occurred while fetching the ProductVariant with its details.";
            // Log the original exception correctly
            _logger.LogError(ex, errorMsg);
            // Wrap the original exception in your custom type
            //throw new FailedToCreateException(errorMsg, ex);
            throw;
        }
    }

    public async Task<List<ColorReadDTO>> RetrieveProductVarientColors(int productId)
    {


        try
        {
            var productVarients = await _productVariantService.RetrieveAllAsync(p => p.ProductId == productId);
            var colorsDictionary = new Dictionary<int, ColorReadDTO>();

            foreach (var productVar in productVarients)
            {
                var color = await _colorService.RetrieveByAsync(c => c.ColorId == productVar.ColorId);

                if (!colorsDictionary.ContainsKey(color.ColorId))
                    colorsDictionary.Add(color.ColorId, color);
            }


            return colorsDictionary.Values.ToList(); ;
        }

        catch (Exception ex) // Catch any other unexpected exceptions
        {
            string errorMsg =
              "An unexpected error occurred while fetching the ProductVariant with its details.";
            // Log the original exception correctly
            _logger.LogError(ex, errorMsg);
            // Wrap the original exception in your custom type
            //throw new FailedToCreateException(errorMsg, ex);
            throw;
        }
    }

    public async Task<ProductVariantReadWithDetailsDTO?> RetrieveProductVarientStock(int productId, int colorId, int sizeId)
    {
        try
        {

            var productVariantDetailsLst = new List<ProductVariantReadWithDetailsDTO>();

            var productVarient = await _productVariantService.RetrieveByAsync(p => p.ProductId == productId &&
                                                                                     p.ColorId == colorId &&
                                                                                     p.SizeId == sizeId);

            ProductVariantReadWithDetailsDTO pv = null;

            if (productVarient is not null)
                pv = new()
                {
                    ProductVariantId = productVarient.ProductVariantId,
                    ProductId = productId,
                    StockQuantity = productVarient.StockQuantity
                };


            return pv;
        }

        catch (Exception ex) // Catch any other unexpected exceptions
        {
            //await _unitOfWork.RollbackAsync();
            string errorMsg =
                "An unexpected error occurred while fetching the ProductVariant with its details.";
            // Log the original exception correctly
            _logger.LogError(ex, errorMsg);
            // Wrap the original exception in your custom type
            //throw new FailedToCreateException(errorMsg, ex);
            throw;
        }
    }
}
