using Application.Exceptions;
using AutoMapper;
using Entities.Models.DTOs;
using IRepository.Generic;
using IServices;
using Microsoft.Extensions.Logging;

namespace Kemet.Application.Services.Orchestrators;

public interface IProductPriceOrchestratorService
{
    Task<ProductPriceOrchestratorDTO> CreateProductPrice(
        ProductPriceOrchestratorCreateDTO createRequest
    );

    Task<PriceReadDTO> CreatePriceRange(PriceCreateDTO createRequest);
    Task<ProductQuantityPriceReadDTO> CreateProductQuantityPrice(
        ProductQuantityPriceCreateDTO createRequest
    );
}

public class ProductPriceOrchestratorService : IProductPriceOrchestratorService
{
    readonly IPriceService _priceService;
    readonly IProductQuantityPriceService _productQuantityPriceService;
    readonly IUnitOfWork _unitOfWork;
    readonly IMapper _mapper;
    readonly ILogger<ProductPriceOrchestratorService> _logger;

    public ProductPriceOrchestratorService(
        IPriceService priceService,
        IProductQuantityPriceService productQuantityPriceService,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<ProductPriceOrchestratorService> logger
    )
    {
        _priceService = priceService;
        _productQuantityPriceService = productQuantityPriceService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    private async Task<PriceReadDTO> CreatePrice(ProductPriceOrchestratorCreateDTO createRequest)
    {
        var priceDto = _mapper.Map<PriceCreateDTO>(createRequest);
        var price = await _priceService.CreateAsync(priceDto);
        return price;
    }

    private async Task<List<ProductQuantityPriceReadDTO>> CreateProductQuantityPrice(
        Dictionary<int, ProductPriceOrchestratorRecord> QuantitiesPrices,
        int productId
    )
    {
        List<ProductQuantityPriceCreateDTO> productQuantityPrices = new();
        foreach (var productQuantityPrice in QuantitiesPrices)
        {
            productQuantityPrices.Add(
                new()
                {
                    ProductId = productId,
                    Quantity = productQuantityPrice.Key,
                    UnitPrice = productQuantityPrice.Value.QuantityPrice,
                    Note = productQuantityPrice.Value.Note,
                }
            );
        }
        var PQPs = await _productQuantityPriceService.AddRange(productQuantityPrices);
        return PQPs;
    }

    //De
    // [Obsolete]
    public async Task<ProductPriceOrchestratorDTO> CreateProductPrice(
        ProductPriceOrchestratorCreateDTO createRequest
    )
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            /*
                * get the price
                * compare the ranges with the offer range.
                * if ok create it if not rollback
        
               * Q? Should Accept more the one offer or one per Request?
               * one per request => less headache on the code, the code is straight forward.
               * many offers per request? => headache between compare prices ...
               * go with one offer per request.
            */

            var price = await this.CreatePrice(createRequest);

            var PQPs = await this.CreateProductQuantityPrice(
                createRequest.QuantitiesPrices,
                createRequest.ProductId
            );

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();

            Dictionary<int, ProductPriceOrchestratorRecord> quantitiesPrices = new();

            foreach (var Pqp in PQPs)
            {
                quantitiesPrices.Add(Pqp.Quantity, new(Pqp.UnitPrice, Pqp.Note, Pqp.CreatedAt));
            }

            var productPriceOrchestratorDTOs = new ProductPriceOrchestratorDTO()
            {
                PriceId = price.PriceId,
                MaximumPrice = price.MaximumPrice,
                MinimumPrice = price.MinimumPrice,
                QuantitiesPrices = quantitiesPrices,
            };

            return productPriceOrchestratorDTOs;
        }
        catch (FailedToCreateException ex)
        {
            await _unitOfWork.RollbackAsync();
            // Log the original exception correctly
            _logger.LogError(
                ex,
                "A known creation failure occurred while creating the Product's price with."
            );
            // Re-throw the original exception to preserve the stack trace and type
            throw;
        }
        catch (Exception ex) // Catch any other unexpected exceptions
        {
            await _unitOfWork.RollbackAsync();
            string errorMsg = "An unexpected error occurred while creating the Product's price.";
            // Log the original exception correctly
            _logger.LogError(ex, errorMsg);
            throw;
        }
    }

    public async Task<ProductQuantityPriceReadDTO> CreateProductQuantityPrice(
        ProductQuantityPriceCreateDTO createRequest
    )
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            /*
                * get the price
                * compare the ranges with the offer range.
                * if ok create it if not rollback
        
               * Q? Should Accept more the one offer or one per Request?
               * one per request => less headache on the code, the code is straight forward.
               * many offers per request? => headache between compare prices ...
               * go with one offer per request.
            */



            var price = await _priceService.ProductActivePrice(createRequest.ProductId);

            if (price is null)
                throw new DoesNotExistException("There's no price for that product");

            if (
                price.MaximumPrice < createRequest.UnitPrice
                && price.MinimumPrice > createRequest.UnitPrice
            )
                throw new ArgumentOutOfRangeException("UnitPrice should be in range");

            var ProductQuantity = await _productQuantityPriceService.RetrieveByAsync(pv =>
                pv.Quantity == createRequest.Quantity
                && pv.ProductId == createRequest.ProductId
                && pv.IsActive
            );

            if (ProductQuantity != null)
            {
                var mo = await _productQuantityPriceService.Deactivate(
                    ProductQuantity.ProductId,
                    ProductQuantity.ProductQuantityPriceId
                );
            }

            var PQP = await _productQuantityPriceService.CreateAsync(createRequest);

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();

            return PQP;
        }
        catch (Exception ex) // Catch any other unexpected exceptions
        {
            await _unitOfWork.RollbackAsync();
            string errorMsg = "An unexpected error occurred while creating the Product's price.";
            _logger.LogError(ex, errorMsg);
            throw;
        }
    }

    public async Task<PriceReadDTO> CreatePriceRange(PriceCreateDTO createRequest)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var activePrice = await _priceService.RetrieveByAsync(p =>
                p.ProductId == createRequest.ProductId && p.IsActive
            );

            if (
                activePrice is not null
                && (
                    activePrice?.MinimumPrice != createRequest.MinimumPrice
                    || activePrice?.MaximumPrice != createRequest.MaximumPrice
                )
            )
            {
                var deactivatePrice = await _priceService.DeactivatePrice(activePrice);
                await _unitOfWork.SaveChangesAsync();
            }

            var price = await _priceService.CreateAsync(createRequest);

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();

            return price;
        }
        catch (FailedToCreateException ex)
        {
            await _unitOfWork.RollbackAsync();
            // Log the original exception correctly
            _logger.LogError(
                ex,
                "A known creation failure occurred while creating the Product's price with."
            );
            // Re-throw the original exception to preserve the stack trace and type
            throw;
        }
        catch (Exception ex) // Catch any other unexpected exceptions
        {
            await _unitOfWork.RollbackAsync();
            string errorMsg = "An unexpected error occurred while creating the Product's price.";
            // Log the original exception correctly
            _logger.LogError(ex, errorMsg);
            throw;
        }
    }

    public async Task<ProductPriceOrchestratorDTO> GetActiveProductPrices(int ProductId)
    {
        try
        {
            var price = await _priceService.ProductActivePrice(ProductId);

            var PQPs = await _productQuantityPriceService.ActiveQuantityPriceFor(ProductId);

            Dictionary<int, ProductPriceOrchestratorRecord> quantitiesPrices = new();

            foreach (var Pqp in PQPs)
            {
                quantitiesPrices.Add(Pqp.Quantity, new(Pqp.UnitPrice, Pqp.Note, Pqp.CreatedAt));
            }

            var productPriceOrchestratorDTOs = new ProductPriceOrchestratorDTO()
            {
                PriceId = price.PriceId,
                MaximumPrice = price.MaximumPrice,
                MinimumPrice = price.MinimumPrice,
                QuantitiesPrices = quantitiesPrices,
            };

            return productPriceOrchestratorDTOs;
        }
        catch (Exception ex)
        {
            string errorMsg = "An unexpected error occurred while fetching the Product's prices.";
            // Log the original exception correctly
            _logger.LogError(ex, errorMsg);
            throw;
        }
    }
}

public class ProductPriceOrchestratorDTO
{
    public int PriceId { get; set; }
    public decimal MinimumPrice { get; set; }
    public decimal MaximumPrice { get; set; }

    public required Dictionary<int, ProductPriceOrchestratorRecord> QuantitiesPrices { get; set; }

    public int ProductId { get; set; }
}

public class ProductPriceOrchestratorCreateDTO
{
    public decimal MinimumPrice { get; set; }
    public decimal MaximumPrice { get; set; }
    public DateTime? StartFrom { get; set; }
    public DateTime? EndsAt { get; set; }
    public string? Note { get; set; }

    public required Dictionary<int, ProductPriceOrchestratorRecord> QuantitiesPrices { get; set; }

    public int ProductId { get; set; }
}

public record ProductPriceOrchestratorRecord(
    decimal QuantityPrice,
    string? Note,
    DateTime? CreatedAt
);
