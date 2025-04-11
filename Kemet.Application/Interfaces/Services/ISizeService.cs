using Domain.IServices;
using Entities.Models;
using Entities.Models.DTOs;

namespace IServices;

public interface ISizeService
    : IServiceAsync<Size, SizeCreateDTO, SizeDeleteDTO, SizeUpdateDTO, SizeReadDTO> { }
