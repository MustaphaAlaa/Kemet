using Domain.IServices;
using Entities.Models;
using Entities.Models.DTOs;

namespace IServices;

public interface IColorService
    : IServiceAsync<Color, ColorCreateDTO, ColorDeleteDTO, ColorUpdateDTO, ColorReadDTO> { }
