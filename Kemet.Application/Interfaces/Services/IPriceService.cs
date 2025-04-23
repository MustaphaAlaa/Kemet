using Domain.IServices;
using Entities.Models;
using Entities.Models.DTOs;

namespace IServices;

public interface IPriceService
    : IServiceAsync<Price, int, PriceCreateDTO, PriceDeleteDTO, PriceUpdateDTO, PriceReadDTO>
{ }
