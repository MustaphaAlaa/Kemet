using Domain.IServices;
using Entities.Models;
using Entities.Models.DTOs; 

namespace IServices;

public interface IPriceService
    : IServiceAsync<Price, PriceCreateDTO, PriceDeleteDTO, PriceUpdateDTO, PriceReadDTO> { }
