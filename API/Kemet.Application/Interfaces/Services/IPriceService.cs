using Domain.IServices;
using Entities.Models;
using Entities.Models.DTOs;

namespace IServices;

public interface IPriceService
    : IServiceAsync<Price, int, PriceCreateDTO, PriceDeleteDTO, PriceUpdateDTO, PriceReadDTO>
{
    Task<PriceReadDTO> ProductActivePrice(int ProductId);
    Task<PriceReadDTO> DeactivatePrice(PriceReadDTO priceReadDTO);
    bool AreRangesEquals(int MaximumPrice, int MinimumPrice);
    Task<PriceReadDTO> UpdateNote(PriceUpdateDTO updateRequest);
}
